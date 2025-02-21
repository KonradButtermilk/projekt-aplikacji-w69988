using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLogic;
using ChessLogic.Moves;
using ChessLogic.Pieces;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position,Move> moveCache=new Dictionary<Position, Move>();

        private GameState gameState;
        private Position selectedPosition = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPalyer);
        }

        private void InitializeBoard()
        {


            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Image image = new Image();
                    pieceImages[row, column] = image;

                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[row,column] = highlight;
                    HighlightGrid.Children.Add(highlight);
                    

                }
            }
        }

        private void DrawBoard(Board board) { 
        
            for(int row = 0;row < 8; row++)
            {
                   for(int column = 0; column < 8; column++)
                {
                    Piece piece = board[row, column];
                    ImageSource imageSource = Images.GetImage(piece);
                    pieceImages[row, column].Source = imageSource;
                }   
            }
        
        }

        private void CacheMoves(IEnumerable<Move> moves) { 
        
            moveCache.Clear();
            foreach (Move move in moves)
            {
                moveCache[move.ToPosition] = move;
            }
        }

        private void ShowHighlights()
        {
        
            Color color = Color.FromArgb(150,125,255,125);
            foreach (Position position in moveCache.Keys)
            {
                highlights[position.Row, position.Column].Fill = new SolidColorBrush(color);
            }
        }

        private void HideHighlights()
        {
            foreach (Position position in moveCache.Keys)
            {
                highlights[position.Row, position.Column].Fill = Brushes.Transparent;
            }
        }
        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;

            int row = (int)(point.Y / squareSize);
            int column = (int)(point.X / squareSize);

            return new Position(row, column);
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(BoardGrid);
            Position position = ToSquarePosition(point);

            if(selectedPosition == null)
            {
                OnFromPosotionSelected(position);
            }
            else
            {
                OnToPosotionSelected(position);

            }
        }

        private void OnFromPosotionSelected(Position position)
        {
            IEnumerable<Move> moves = gameState.LegalMovesForPieces(position);

            if (moves.Any())
            {
                selectedPosition = position;
                CacheMoves(moves);
                ShowHighlights();
            }
        }

        private void OnToPosotionSelected(Position position)
        {
            
            selectedPosition = null;
            HideHighlights();

            if (moveCache.TryGetValue(position,out Move move))
            {
                HandleMove(move);
            }
        }

        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPalyer);
        }

        private void SetCursor(Player player)
        {
            Cursor = player == Player.White ? ChessCursors.WhiteCursor : ChessCursors.BlackCursor;
        }
    }
}
