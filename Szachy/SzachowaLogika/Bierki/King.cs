using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    public class King : Bierka
    {
        public override RodzajBierek Type => RodzajBierek.King;
        public override Player Color { get; }

        public King(Player color)
        {
            Color = color;
        }

        public override Bierka Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}