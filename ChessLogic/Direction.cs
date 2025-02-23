namespace ChessLogic
{
    public readonly struct Direction
    {
        public int DeltaRow { get; }
        public int DeltaColumn { get; }

        public Direction(int deltaRow, int deltaColumn)
        {
            DeltaRow = deltaRow;
            DeltaColumn = deltaColumn;
        }

        public static readonly Direction North = new(-1, 0);
        public static readonly Direction South = new(1, 0);
        public static readonly Direction East = new(0, 1);
        public static readonly Direction West = new(0, -1);
        public static readonly Direction NorthEast = North + East;
        public static readonly Direction NorthWest = North + West;
        public static readonly Direction SouthEast = South + East;
        public static readonly Direction SouthWest = South + West;

        public static Direction operator +(Direction a, Direction b)
        {
            return new Direction(
                a.DeltaRow + b.DeltaRow,
                a.DeltaColumn + b.DeltaColumn
            );
        }
    }
}
