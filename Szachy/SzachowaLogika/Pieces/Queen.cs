﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    public class Queen : Piece
    {
        public override RodzajBierek Type => RodzajBierek.Queen;
        public override Player Color { get; }

        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.North,
            Direction.East,
            Direction.South,
            Direction.West,
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthWest,
            Direction.SouthEast
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
        public override IEnumerable<Move> GetMoves(Pozycja from, Board board)
        {

            IEnumerable<Pozycja> possiblePositions = MovePositionsInDirs(from, board, dirs);


            List<Move> moves = new List<Move>();


            foreach (Pozycja to in possiblePositions)
            {

                Move move = new NormalMove(from, to);
                moves.Add(move);
            }


            return moves;
        }
    }
}