using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    // Klasa musi być abstract, bo nie reprezentuje konkretnej bierki
    public abstract class Piece
    {
        public abstract RodzajBierek Type { get; }
        public abstract Player Color { get; }
        public bool HasMoved { get; set; } = false;

        public abstract Piece Copy();

        public abstract IEnumerable<Move> GetMoves(Pozycja from, Board board);

        protected IEnumerable<Pozycja> MovePositionsInDir(Pozycja from, Board board, Direction dir) //sprawdzanie wszystkich możliwych ruchów dla figury
        {
            for (Pozycja pos = from + dir; Board.IsInside(pos); pos += dir)
            {
                if (board.IsEmpty(pos))
                {
                    yield return pos;
                    continue;
                }
                Piece piece = board[pos];
                {
                    if (piece.Color != Color)
                    {
                        yield return pos;
                    }
                    yield break;
                }
            }
        }
        protected IEnumerable<Pozycja> MovePositionsInDirs(Pozycja from, Board board, Direction[] dirs)
        {
           return dirs.SelectMany(dir => MovePositionsInDir(from, board, dir));
        }
    }
}