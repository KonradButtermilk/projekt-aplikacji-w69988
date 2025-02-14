using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    public class NormalMove : Move
    {
        public override MoveType Type => MoveType.Normal;
        public override Pozycja FromPoz { get; }
        public override Pozycja ToPoz { get; }
        public NormalMove(Pozycja from, Pozycja to)
        {
            FromPoz = from;
            ToPoz = to;
        }
        public override void Execute(Board board)
        {
            Piece piece = board[FromPoz];
            board[ToPoz] = piece;
            board[FromPoz] = null;
            piece.HasMoved = true;
        }
    }
    
 }

