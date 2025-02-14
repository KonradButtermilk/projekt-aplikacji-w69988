using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    public class King : Piece
    {
        public override RodzajBierek Type => RodzajBierek.King;
        public override Player Color { get; }

        private static readonly Direction[] directions = new Direction[]
            {
                Direction.North,
                Direction.South,
                Direction.West,
                Direction.East,
                Direction.NorthWest,
                Direction.NorthEast,
                Direction.SouthWest,
                Direction.SouthEast


            };

        public King(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private IEnumerable<Pozycja> MovePozycjas(Pozycja from, Board board)
        {
            foreach (Direction direction in directions)
            {
                Pozycja to = from + direction;
                if (!Board.IsInside(to))
                {
                    continue;
                }

                if (board.IsEmpty(to) || board[to].Color != Color)
                {
                    yield return to;
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Pozycja from, Board board)
        {
            foreach (Pozycja to in MovePozycjas(from, board))
            {
                yield return new NormalMove(from, to);
            }
        }
    }
}