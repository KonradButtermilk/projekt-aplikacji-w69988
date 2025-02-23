namespace ChessLogic
{
    public class Queen: Piece
    {
        public override PieceType Type => PieceType.Queen;
        public override Player Color { get; }

        protected static readonly Direction[] directions = new Direction[]
        {
            Direction.North,
            Direction.East,
            Direction.South,
            Direction.West,
            Direction.NorthEast,
            Direction.NorthWest,
            Direction.SouthEast,
            Direction.SouthWest
        };

        public Queen(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Queen copy = new Queen(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override Piece Clone()
        {
            Queen clone = new Queen(Color);
            clone.HasMoved = HasMoved;
            return clone;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionInDirections(from, board, directions)
                .Select(to => new NormalMove(from, to));
        }
    }
}
