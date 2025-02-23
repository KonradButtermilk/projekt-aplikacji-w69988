using System.Collections.Generic;

namespace ChessLogic.Pieces
{
    public class Knight : Piece
    {
        public override PieceType Type => PieceType.Knight;
        public override Player Color { get; }

        private static readonly Direction[] directions = {
            new Direction(-2, -1),
            new Direction(-2, 1),
            new Direction(-1, -2),
            new Direction(-1, 2),
            new Direction(1, -2),
            new Direction(1, 2),
            new Direction(2, -1),
            new Direction(2, 1)
        };

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
        public override Piece Clone()
        {
            Knight clone = new Knight(Color);
            clone.HasMoved = HasMoved;
            return clone;
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
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach (Position to in MovePositions(from, board))
            {
                yield return new NormalMove(from, to);
            }
        }
    }
}