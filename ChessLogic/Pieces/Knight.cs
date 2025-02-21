using ChessLogic.Moves;

namespace ChessLogic.Pieces
{
    public class Knight : Piece
    {
        public override PieceType Type => PieceType.Knight;
        public override Player Color { get; }

        public Knight(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Knight copy = new Knight(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private static IEnumerable<Position> PotencialToPositions(Position from)
        {
            foreach(Direction verticalDirection in new Direction[] {Direction.North, Direction.South})
            {
                foreach(Direction horizontalDirection in new Direction[] {Direction.West, Direction.East})
                {
                    yield return from + 2*verticalDirection + horizontalDirection;
                    yield return from +2* horizontalDirection + verticalDirection;
                }
            }
        }

        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            return PotencialToPositions(from).Where(positions => Board.IsInside(positions) && (board.IsEmpty(positions) || board[positions].Color != Color));
        }


        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositions(from, board).Select(to => new NormalMove(from, to));
        }
    }
}
