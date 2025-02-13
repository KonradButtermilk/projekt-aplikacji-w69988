﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    public class Rook : Bierka
    {
        public override RodzajBierek Type => RodzajBierek.Rook;
        public override Player Color { get; }

        public Rook(Player color)
        {
            Color = color;
        }

        public override Bierka Copy()
        {
            Rook copy = new Rook(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}