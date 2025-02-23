using System.Collections.Generic;

namespace ChessLogic.Pieces
{
    // Klasa reprezentująca króla w szachach
    public class King : Piece
    {
        // Przeciążona cecha określająca typ figury jako Król
        public override PieceType Type => PieceType.King;

        // Przeciążona cecha określająca kolor figury
        public override Player Color { get; }

        // Tablica statyczna określająca możliwe kierunki ruchu dla króla
        private static readonly Direction[] directions = {
            Direction.North, Direction.South, Direction.West, Direction.East,
            Direction.NorthWest, Direction.NorthEast, Direction.SouthWest, Direction.SouthEast
        };

        // Konstruktor inicjalizujący kolor króla
        public King(Player color) => Color = color;

        // Metoda kopiująca króla
        // Tworzymy nową instancję króla z tym samym kolorem i ustawiamy stan ruchu
        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        // Metoda klonująca króla
        // Przeciążamy metodę Clone() w celu użycia metody Copy()
        public override Piece Clone() => Copy();

        // Metoda zwracająca możliwe ruchy króla z danej pozycji na planszy
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach (Position to in MovePositions(from, board))
            {
                yield return new NormalMove(from, to);
            }
        }

        // Prywatna metoda zwracająca możliwe pozycje ruchu króla z danej pozycji na planszy
        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach (Direction dir in directions)
            {
                // Obliczenie pozycji docelowej na podstawie kierunku ruchu
                Position to = from + dir;
                // Sprawdzenie, czy pozycja docelowa jest wewnątrz planszy i jest pusta lub zawiera figurę przeciwnika
                if (Board.IsInside(to) && (Board.IsEmpty(board, to) || board[to].Color != Color))
                {
                    yield return to;
                }
            }
        }
    }
}
