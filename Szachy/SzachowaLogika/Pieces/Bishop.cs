using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    public class Bishop : Piece
    {
        public override RodzajBierek Type => RodzajBierek.Bishop;
        public override Player Color { get; }

        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthWest,
            Direction.SouthEast
        };

        public Bishop(Player color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Pozycja from, Board board)
        {
            // Krok 1: Pobierz wszystkie możliwe pozycje docelowe
            IEnumerable<Pozycja> possiblePositions = MovePositionsInDirs(from, board, dirs);

            // Krok 2: Utwórz listę, która będzie przechowywać ruchy
            List<Move> moves = new List<Move>();

            // Krok 3: Przejdź przez wszystkie możliwe pozycje docelowe
            foreach (Pozycja to in possiblePositions)
            {
                // Krok 4: Utwórz nowy ruch (NormalMove) i dodaj go do listy
                Move move = new NormalMove(from, to);
                moves.Add(move);
            }

            // Krok 5: Zwróć listę ruchów
            return moves;
        }
    }
}