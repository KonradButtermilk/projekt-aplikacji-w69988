using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    public abstract class Move
    {
        public abstract MoveType Type { get; }
        public abstract Pozycja FromPoz { get; }
        public abstract Pozycja ToPoz { get; }
        public abstract void Execute ( Board board );
    }
}
