namespace ChessLogic
{
    // Struktura reprezentująca kierunek ruchu na szachownicy
    public readonly struct Direction
    {
        // Właściwości przechowujące zmiany wiersza i kolumny
        public int DeltaRow { get; }
        public int DeltaColumn { get; }

        // Konstruktor inicjalizujący kierunek ruchu
        public Direction(int deltaRow, int deltaColumn)
        {
            DeltaRow = deltaRow;
            DeltaColumn = deltaColumn;
        }

        // Statyczne pola reprezentujące podstawowe kierunki ruchu
        public static readonly Direction North = new(-1, 0);  // Ruch na północ
        public static readonly Direction South = new(1, 0);   // Ruch na południe
        public static readonly Direction East = new(0, 1);    // Ruch na wschód
        public static readonly Direction West = new(0, -1);   // Ruch na zachód
        public static readonly Direction NorthEast = North + East;   // Ruch na północny wschód
        public static readonly Direction NorthWest = North + West;   // Ruch na północny zachód
        public static readonly Direction SouthEast = South + East;   // Ruch na południowy wschód
        public static readonly Direction SouthWest = South + West;   // Ruch na południowy zachód

        // Przeciążony operator dodawania, pozwalający na łączenie dwóch kierunków
        public static Direction operator +(Direction a, Direction b)
        {
            return new Direction(
                a.DeltaRow + b.DeltaRow,
                a.DeltaColumn + b.DeltaColumn
            );
        }
    }
}
