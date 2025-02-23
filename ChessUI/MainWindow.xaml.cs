using System;
using System.Collections.Generic;
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
        private const string ConnectionString =
        @"Data Source=KONRADPC\SQLEXPRESS;Initial Catalog=ChessDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private readonly Image[,] pieceImages = new Image[8, 8];
        private MoveHistory moveHistory;
        private GameState gameState;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
            InitializeGame();
        }

        private void InitializeBoard()
        {
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
            gameState = new GameState(Player.White, Board.Initial());
            moveHistory = new MoveHistory(gameState.Board);
            DrawBoard(gameState.Board);
            UpdateButtons();
        }

        private void DrawBoard(Board board)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Piece piece = board[row, col];
                    pieceImages[row, col].Source = Images.GetImage(piece);

                    // Aktualizuj podświetlenie aktualnej pozycji
                    var highlight = HighlightGrid.Children[row * 8 + col] as Rectangle;
                    highlight.Visibility = Visibility.Hidden;
                }
            }
        }

        private void UpdateButtons()
        {
            btnPrev.IsEnabled = moveHistory.CanStepBackward;
            btnNext.IsEnabled = moveHistory.CanStepForward;
        }

        private void LoadGame(ChessGame game)
        {
            try
            {
                var moves = ParseMoves(game.Moves);
                moveHistory = new ChessLogic.MoveHistory(Board.Initial());

                foreach (var move in moves)
                {
                    moveHistory.AddMove(move);
                }

                DrawBoard(moveHistory.CurrentBoard);
                UpdateButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd ładowania gry: {ex.Message}");
            }
        }

        private List<Move> ParseMoves(string moves)
        {
            var result = new List<Move>();
            string[] moveTokens = moves.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string token in moveTokens)
            {
                if (token.Length != 4)
                {
                    MessageBox.Show($"Nieprawidłowy format ruchu: {token}");
                    continue;
                }

                string fromStr = token.Substring(0, 2);
                string toStr = token.Substring(2, 2);

                if (Position.TryParse(fromStr, out Position from) && Position.TryParse(toStr, out Position to))
                {
                    result.Add(new NormalMove(from, to));
                }
                else
                {
                    MessageBox.Show($"Nie udało się przetworzyć ruchu: {token}");
                }
            }
            return result;
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Logika obsługi kliknięć
        }
        private void LoadGameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new GameHistoryWindow();
            if (historyWindow.ShowDialog() == true && historyWindow.SelectedGame != null)
            {
                LoadGame(historyWindow.SelectedGame);
            }
        }
        private void PrevMoveButton_Click(object sender, RoutedEventArgs e)
        {
            moveHistory.StepBackward();
            DrawBoard(moveHistory.CurrentBoard);
            UpdateButtons();
        }

        private void NextMoveButton_Click(object sender, RoutedEventArgs e)
        {
            moveHistory.StepForward();
            DrawBoard(moveHistory.CurrentBoard);
            UpdateButtons();
        }
    }
}