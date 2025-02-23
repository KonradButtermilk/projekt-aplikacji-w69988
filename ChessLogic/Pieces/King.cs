using System.Collections.Generic;

namespace ChessLogic.Pieces
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }

        private static readonly Direction[] directions = {
            Direction.North, Direction.South, Direction.West, Direction.East,
            Direction.NorthWest, Direction.NorthEast, Direction.SouthWest, Direction.SouthEast
        };

        public King(Player color) => Color = color;

        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override Piece Clone() => Copy();

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach (Position to in MovePositions(from, board))
            {
                yield return new NormalMove(from, to);
            }
        }

        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach (Direction dir in directions)
            {
                Position to = from + dir;
                if (Board.IsInside(to) && (Board.IsEmpty(board, to) || board[to].Color != Color))
                {
                    yield return to;
                }
            }
        }
    }
}