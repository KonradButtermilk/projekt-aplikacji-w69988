using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    // Klasa musi być abstract, bo nie reprezentuje konkretnej bierki
    public abstract class Bierka
    {
        public abstract RodzajBierek Type { get; }
        public abstract Player Color { get; }
        public bool HasMoved { get; set; } = false;

        public abstract Bierka Copy();
    }
}