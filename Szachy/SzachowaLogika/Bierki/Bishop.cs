using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    public class Bishop : Bierka
    {
        public override RodzajBierek Type => RodzajBierek.Bishop;
        public override Player Color { get; }

        public Bishop(Player color)
        {
            Color = color;
        }
        public override Bierka Copy()
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}