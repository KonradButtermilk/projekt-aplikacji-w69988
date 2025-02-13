using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    public class Pawn : Bierka
    {
        public override RodzajBierek Type => RodzajBierek.Pawn;
        public override Player Color { get; }

        public Pawn(Player color)
        {
            Color = color; 
        }
        public override Bierka Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}
