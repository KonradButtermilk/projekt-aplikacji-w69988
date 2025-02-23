namespace ChessLogic
{
    // Klasa reprezentująca normalny ruch w szachach
    public class NormalMove : Move
    {
       
        // Przeciążamy tę właściwość, aby zapewnić, że każdy ruch tego typu będzie miał określony typ MoveType.Normal
        public override MoveType Type => MoveType.Normal;

        
        // Przeciążamy tę właściwość, aby zapewnić, że pozycja początkowa ruchu będzie mogła być ustawiona przez konstruktor
        public override Position FromPosition { get; }

        // Przeciążamy tę właściwość, aby zapewnić, że pozycja końcowa ruchu będzie mogła być ustawiona przez konstruktor
        public override Position ToPosition { get; }

        // Konstruktor pozwala na ustawienie początkowej i końcowej pozycji ruchu w momencie tworzenia obiektu
        public NormalMove(Position fromPosition, Position toPosition)
        {
            FromPosition = fromPosition;
            ToPosition = toPosition;
        }

       
        // Ta metoda wykonuje rzeczywisty ruch na szachownicy, przenosząc figurę z pozycji początkowej na pozycję końcową
        public override void Execute(Board board)
        {
            // Pobranie figury z pozycji początkowej
            Piece piece = board[FromPosition];
            // Przeniesienie figury na pozycję końcową
            board[ToPosition] = piece;
            // Usunięcie figury z pozycji początkowej
            board[FromPosition] = null;
            // Oznaczenie, że figura wykonała ruch
            //piece.HasMoved = true;
        }
    }
}
