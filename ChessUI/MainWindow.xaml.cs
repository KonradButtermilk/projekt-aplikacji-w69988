using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChessLogic;

namespace ChessUI
{
    public partial class MainWindow : Window
    {
        // Connection string do bazy danych
        private const string ConnectionString =
            @"Data Source=KONRADPC\SQLEXPRESS;Initial Catalog=ChessDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        // Pola klasy
        private readonly Image[,] pieceImages = new Image[8, 8]; // Tablica obrazów figur
        private MoveHistory moveHistory; // Historia ruchów
        private GameState gameState; // Stan gry
        private Position? selectedPosition = null; // Wybrana pozycja (nullable)

        // ------------------- Konstruktor -------------------
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
            InitializeGame();
            this.KeyDown += MainWindow_KeyDown; // Dodanie obsługi klawiszy
            this.Focusable = true; // Okno może przyjmować fokus
            this.Focus(); // Ustawienie fokusu na oknie
        }

        // ------------------- Inicjalizacja -------------------
        private void InitializeBoard()
        {
            // Tworzenie siatki obrazów figur i podświetleń
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Image image = new Image();
                    pieceImages[row, col] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle
                    {
                        Fill = Brushes.Transparent,
                        Stroke = Brushes.Yellow,
                        StrokeThickness = 3,
                        Visibility = Visibility.Hidden
                    };
                    HighlightGrid.Children.Add(highlight);
                }
            }
        }

        private void InitializeGame()
        {
            // Ustawienie początkowego stanu gry
            gameState = new GameState(Player.White, Board.Initial());
            moveHistory = new MoveHistory(gameState.Board);
            DrawBoard(gameState.Board);
            UpdateButtons();
        }

        // ------------------- Rysowanie i aktualizacja UI -------------------
        private void DrawBoard(Board board)
        {
            // Rysowanie aktualnego stanu planszy
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Piece piece = board[row, col];
                    pieceImages[row, col].Source = Images.GetImage(piece);
                    var highlight = HighlightGrid.Children[row * 8 + col] as Rectangle;
                    highlight.Visibility = Visibility.Hidden;
                }
            }
        }

        private void UpdateButtons()
        {
            // Aktualizacja stanu przycisków przewijania
            btnPrev.IsEnabled = moveHistory.CanStepBackward;
            btnNext.IsEnabled = moveHistory.CanStepForward;
        }

        private void HighlightSquare(Position pos)
        {
            // Podświetlenie pola żółtą ramką
            int index = pos.Row * 8 + pos.Column;
            var highlight = HighlightGrid.Children[index] as Rectangle;
            highlight.Visibility = Visibility.Visible;
        }

        private void ClearHighlights()
        {
            // Usunięcie wszystkich podświetleń
            foreach (var child in HighlightGrid.Children)
            {
                if (child is Rectangle rect)
                {
                    rect.Visibility = Visibility.Hidden;
                }
            }
        }

        // ------------------- Wczytywanie gry -------------------
        private void LoadGame(ChessGame game)
        {
            // Inicjalizacja gry z bazy danych
            Board initialBoard = Board.Initial();
            gameState = new GameState(Player.White, initialBoard);
            moveHistory = new MoveHistory(initialBoard);
            List<string> moveLog = new List<string>();

            var moves = ParseAlgebraicMoves(game.Moves, moveLog);

            // Wykonanie wszystkich sparsowanych ruchów
            foreach (var move in moves)
            {
                gameState.MakeMove(move);
                moveHistory.AddMove(move);
            }

            // Synchronizacja i wyświetlenie planszy
            gameState = new GameState(gameState.CurrentPlayer, moveHistory.CurrentBoard);
            DrawBoard(moveHistory.CurrentBoard);
            UpdateButtons();
        }

        private List<Move> ParseAlgebraicMoves(string moves, List<string> moveLog)
        {
            // Parsowanie listy ruchów z notacji 
            var result = new List<Move>();
            string[] moveTokens = moves.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Board tempBoard = gameState.Board.DeepCopy();
            Player currentPlayer = Player.White;

            foreach (string token in moveTokens)
            {
                Move move = ParseSingleMove(token, tempBoard, currentPlayer)?.FirstOrDefault();
                if (move != null)
                {
                    move.Execute(tempBoard);
                    result.Add(move);
                    moveLog.Add($"{(currentPlayer == Player.White ? "White" : "Black")}: {token} -> Parsed successfully");
                    currentPlayer = currentPlayer == Player.White ? Player.Black : Player.White;
                }
                else
                {
                    moveLog.Add($"{(currentPlayer == Player.White ? "White" : "Black")}: {token} -> Failed to parse");
                    currentPlayer = currentPlayer == Player.White ? Player.Black : Player.White;
                }
            }

            return result;
        }

        private IEnumerable<Move> ParseSingleMove(string token, Board board, Player player)
        {
            // Usunięcie znaków szachu/matu
            token = token.TrimEnd('+', '#');

            // Roszady
            if (token == "O-O" || token == "O-O-O")
            {
                Position kingFrom = player == Player.White ? new Position(7, 4) : new Position(0, 4);
                Position kingTo = token == "O-O"
                    ? (player == Player.White ? new Position(7, 6) : new Position(0, 6))
                    : (player == Player.White ? new Position(7, 2) : new Position(0, 2));

                var kingMoves = board[kingFrom]?.GetMoves(kingFrom, board);
                var move = kingMoves?.FirstOrDefault(m => m.ToPosition == kingTo);
                if (move != null)
                {
                    Position rookFrom = token == "O-O"
                        ? (player == Player.White ? new Position(7, 7) : new Position(0, 7))
                        : (player == Player.White ? new Position(7, 0) : new Position(0, 0));
                    Position rookTo = token == "O-O"
                        ? (player == Player.White ? new Position(7, 5) : new Position(0, 5))
                        : (player == Player.White ? new Position(7, 3) : new Position(0, 3));
                    board[rookTo] = board[rookFrom];
                    board[rookFrom] = null;
                    return new[] { move };
                }
                return null;
            }

            // Parsowanie prostych ruchów i bicia
            string targetSquare = token;
            PieceType pieceType = PieceType.Pawn;
            bool isCapture = token.Contains("x");
            char? fromFile = null;

            if (token.Length >= 3 && char.IsUpper(token[0]))
            {
                pieceType = GetPieceTypeFromNotation(token[0]);
                targetSquare = isCapture ? token.Substring(2) : token.Substring(1);
            }
            else if (isCapture && token.Length == 4 && char.IsLower(token[0]))
            {
                fromFile = token[0];
                targetSquare = token.Substring(2);
            }

            if (Position.TryParse(targetSquare, out Position to))
            {
                for (int row = 0; row < 8; row++)
                {
                    for (int col = 0; col < 8; col++)
                    {
                        if (fromFile.HasValue && col != (fromFile.Value - 'a'))
                            continue;

                        Position from = new Position(row, col);
                        if (!Board.IsEmpty(board, from) && board[from].Color == player && board[from].Type == pieceType)
                        {
                            var legalMoves = board[from].GetMoves(from, board);
                            var move = legalMoves.FirstOrDefault(m => m.ToPosition == to);
                            if (move != null)
                            {
                                return new[] { move };
                            }
                        }
                    }
                }
            }

            return null;
        }

        private PieceType GetPieceTypeFromNotation(char pieceChar)
        {
            // Konwersja znaku notacji na typ figury
            return pieceChar switch
            {
                'N' => PieceType.Knight,
                'B' => PieceType.Bishop,
                'R' => PieceType.Rook,
                'Q' => PieceType.Queen,
                'K' => PieceType.King,
                _ => PieceType.Pawn
            };
        }

        // ------------------- Nawigacja w historii -------------------
        private void GoToStart()
        {
            // Przewinięcie do początkowej pozycji
            while (moveHistory.CanStepBackward)
            {
                moveHistory.StepBackward();
            }
            gameState = new GameState(gameState.CurrentPlayer, moveHistory.CurrentBoard);
            DrawBoard(moveHistory.CurrentBoard);
            UpdateButtons();
        }

        private void GoToEnd()
        {
            // Przewinięcie do ostatniego ruchu
            while (moveHistory.CanStepForward)
            {
                moveHistory.StepForward();
            }
            gameState = new GameState(gameState.CurrentPlayer, moveHistory.CurrentBoard);
            DrawBoard(moveHistory.CurrentBoard);
            UpdateButtons();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // Obsługa klawiszy strzałek
            switch (e.Key)
            {
                case Key.Left:
                    if (moveHistory.CanStepBackward)
                    {
                        moveHistory.StepBackward();
                        gameState = new GameState(gameState.CurrentPlayer, moveHistory.CurrentBoard);
                        DrawBoard(moveHistory.CurrentBoard);
                        UpdateButtons();
                    }
                    e.Handled = true;
                    break;

                case Key.Right:
                    if (moveHistory.CanStepForward)
                    {
                        moveHistory.StepForward();
                        gameState = new GameState(gameState.CurrentPlayer, moveHistory.CurrentBoard);
                        DrawBoard(moveHistory.CurrentBoard);
                        UpdateButtons();
                    }
                    e.Handled = true;
                    break;

                case Key.Up:
                    GoToEnd();
                    e.Handled = true;
                    break;

                case Key.Down:
                    GoToStart();
                    e.Handled = true;
                    break;
            }
        }

        // ------------------- Interakcja z planszą -------------------
        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Obsługa kliknięć na planszy
            Point clickPoint = e.GetPosition(BoardGrid);
            int col = (int)(clickPoint.X / 100);
            int row = (int)(clickPoint.Y / 100);
            if (col >= 0 && col < 8 && row >= 0 && row < 8)
            {
                Position pos = new Position(row, col);
                if (selectedPosition == null)
                {
                    Piece piece = gameState.Board[pos];
                    if (piece != null && piece.Color == gameState.CurrentPlayer)
                    {
                        selectedPosition = pos;
                        ClearHighlights();
                        HighlightSquare(pos);
                        var moves = gameState.LegalMovesForPieces(pos);
                        foreach (var move in moves)
                        {
                            HighlightSquare(move.ToPosition);
                        }
                    }
                }
                else
                {
                    if (pos == selectedPosition.Value)
                    {
                        selectedPosition = null;
                        ClearHighlights();
                    }
                    else
                    {
                        Piece piece = gameState.Board[pos];
                        if (piece != null && piece.Color == gameState.CurrentPlayer)
                        {
                            selectedPosition = pos;
                            ClearHighlights();
                            HighlightSquare(pos);
                            var moves = gameState.LegalMovesForPieces(pos);
                            foreach (var move in moves)
                            {
                                HighlightSquare(move.ToPosition);
                            }
                        }
                        else
                        {
                            Position from = selectedPosition.Value;
                            var moves = gameState.LegalMovesForPieces(from);
                            var move = moves.FirstOrDefault(m => m.ToPosition == pos);
                            if (move != null)
                            {
                                gameState.MakeMove(move);
                                moveHistory.AddMove(move);
                                DrawBoard(gameState.Board);
                                selectedPosition = null;
                                ClearHighlights();
                                UpdateButtons();
                            }
                            else
                            {
                                selectedPosition = null;
                                ClearHighlights();
                            }
                        }
                    }
                }
            }
        }

        // ------------------- Zdarzenia menu i przycisków -------------------
        private void LoadGameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Wczytywanie gry z menu
            var historyWindow = new GameHistoryWindow();
            if (historyWindow.ShowDialog() == true && historyWindow.SelectedGame != null)
            {
                LoadGame(historyWindow.SelectedGame);
            }
        }

        private void PrevMoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Cofnięcie ruchu przyciskiem
            moveHistory.StepBackward();
            gameState = new GameState(gameState.CurrentPlayer, moveHistory.CurrentBoard);
            DrawBoard(moveHistory.CurrentBoard);
            UpdateButtons();
        }

        private void NextMoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Przejście do następnego ruchu przyciskiem
            moveHistory.StepForward();
            gameState = new GameState(gameState.CurrentPlayer, moveHistory.CurrentBoard);
            DrawBoard(moveHistory.CurrentBoard);
            UpdateButtons();
        }
    }
}