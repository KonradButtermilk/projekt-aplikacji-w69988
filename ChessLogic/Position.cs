namespace ChessLogic
{
    public readonly struct Position
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public static Position operator +(Position pos, Direction dir)
        {
            return new Position(pos.Row + dir.DeltaRow, pos.Column + dir.DeltaColumn);
        }

        public static bool TryParse(string s, out Position position)
        {
            position = new Position(0, 0);
            if (s.Length != 2) return false;

            int col = s[0] - 'a';
            int row = 8 - (s[1] - '0');

            if (row < 0 || row >= 8 || col < 0 || col >= 8)
                return false;

            position = new Position(row, col);
            return true;
        }

        public override string ToString() => $"{(char)('a' + Column)}{8 - Row}";

        // Dodane operatory równości
        public static bool operator ==(Position a, Position b)
        {
            return a.Row == b.Row && a.Column == b.Column;
        }

        public static bool operator !=(Position a, Position b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Position pos)
            {
                return this == pos;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
    }
}