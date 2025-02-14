using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    public class Pawn : Piece
    {

        public override RodzajBierek Type => RodzajBierek.Pawn;
        public override Player Color { get; }
        private readonly Direction forward;

        public Pawn(Player color)
        {
            Color = color;
            if (color == Player.White)
            {
                forward = Direction.North;
            }
            else if (color == Player.Black)
            {
                forward = Direction.South;
            }
        }

        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private static bool CanMoveTo(Pozycja pozycja, Board board)
        {

            return Board.IsInside(pozycja) && board.IsEmpty(pozycja);
        }

        private bool CanCaptureAt(Pozycja pozycja, Board board)
        {
            if (!Board.IsInside(pozycja) || board.IsEmpty(pozycja))
            {
                return false;
            }
            return board[pozycja].Color != Color;
        }

        private IEnumerable<Move> ForwardMoves(Pozycja from, Board board)
        {
            Pozycja oneMovePozycja = from + forward;
            if (CanMoveTo(oneMovePozycja, board))
            {
                yield return new NormalMove(from, oneMovePozycja);
                Pozycja twoMovePozycja = oneMovePozycja + forward;

                if (!HasMoved && CanMoveTo(twoMovePozycja, board))
                {
                    yield return new NormalMove(from, twoMovePozycja);
                }
            }
        }
        private IEnumerable<Move> DiagonalMoves(Pozycja from, Board board)
        {
            foreach (Direction direction in new Direction[] { Direction.West, Direction.East })
            {
                Pozycja to = from + forward + direction;
                if (CanCaptureAt(to, board))
                {
                    yield return new NormalMove(from, to);
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Pozycja from, Board board)
        {
            return ForwardMoves(from, board).Concat(DiagonalMoves(from, board));
        }

    }
}
