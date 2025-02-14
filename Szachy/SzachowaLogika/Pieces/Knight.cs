using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    public class Knight : Piece
    {
        public override RodzajBierek Type => RodzajBierek.Knight;
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

        private static IEnumerable<Pozycja> PotencialToPozycjas(Pozycja from)
        {
            foreach (Direction verticalDirection in new Direction[] { Direction.North, Direction.South })
            {
                foreach (Direction horizontalDirection in new Direction[] { Direction.West, Direction.East })
                {
                    yield return from + 2 * verticalDirection + horizontalDirection;
                    yield return from + 2 * horizontalDirection + verticalDirection;
                }
            }
        }

        private IEnumerable<Pozycja> MovePozycjas(Pozycja from, Board board)
        {
            return PotencialToPozycjas(from).Where(positions => Board.IsInside(positions) && (board.IsEmpty(positions) || board[positions].Color != Color));
        }


        public override IEnumerable<Move> GetMoves(Pozycja from, Board board)
        {
            return MovePozycjas(from, board).Select(to => new NormalMove(from, to));
        }
    }
}