namespace ChessLogic
{
    // Klasa reprezentująca gońca w szachach
    public class Bishop : Piece
    {
        // Przeciążona cecha określająca typ figury jako Goniec
        public override PieceType Type => PieceType.Bishop;

        // Przeciążona cecha określająca kolor figury
        public override Player Color { get; }

        // Tablica statyczna określająca możliwe kierunki ruchu dla gońca
        private static readonly Direction[] directions = new Direction[]
        {
            Direction.NorthEast,
            Direction.NorthWest,
            Direction.SouthEast,
            Direction.SouthWest
        };

        // Konstruktor inicjalizujący kolor gońca
        public Bishop(Player color)
        {
            Color = color;
        }

        // Metoda kopiująca gońca
        // Tworzymy nową instancję gońca z tym samym kolorem i ustawiamy stan ruchu
        public override Piece Copy()
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        // Metoda zwracająca możliwe ruchy gońca z danej pozycji na planszy
        // Przeszukujemy możliwe kierunki i generujemy ruchy dla każdej pozycji docelowej
        public override IEnumerable<Move> GetMoves(Position fromPosition, Board board)
        {
            return MovePositionInDirections(fromPosition, board, directions).Select(toPosition => new NormalMove(fromPosition, toPosition));
        }

        // Metoda klonująca gońca
        // Tworzymy nową instancję gońca z tym samym kolorem i ustawiamy stan ruchu
        public override Piece Clone()
        {
            Bishop clone = new Bishop(Color);
            clone.HasMoved = HasMoved;
            return clone;
        }
    }
}
