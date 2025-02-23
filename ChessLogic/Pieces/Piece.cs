using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public abstract class Piece
    {
        public abstract PieceType Type { get; }
        public abstract Player Color { get; }
        public bool HasMoved { get; set; } = false;
        public abstract Piece Clone();
        public abstract Piece Copy();

        public abstract IEnumerable<Move> GetMoves( Position fromPosition, Board board);

        protected IEnumerable<Position> MovePositionInDirection(Position from, Board board, Direction direction) {


            for (Position position = from + direction; Board.IsInside(position); position += direction) {

                if (Board.IsEmpty(board, position))
                {
                    yield return position;
                    continue;
                }

                Piece piece = board[position];
                if (piece.Color != Color)
                {
                    yield return position;
                }

                yield break;
            }

        }

        protected IEnumerable<Position> MovePositionInDirections(Position from, Board board, Direction[] directions)
        {
            return directions.SelectMany(direction => MovePositionInDirection(from, board, direction));
        }
    }
}
