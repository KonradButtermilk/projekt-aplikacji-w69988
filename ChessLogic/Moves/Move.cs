using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    // Abstrakcyjna klasa reprezentująca ruch szachowy
    public abstract class Move
    {
        // Abstrakcyjna cecha określająca typ ruchu
        public abstract MoveType Type { get; }

        // Abstrakcyjna cecha określająca pozycję początkową ruchu
        public abstract Position FromPosition { get; }

        // Abstrakcyjna cecha określająca pozycję końcową ruchu
        public abstract Position ToPosition { get; }

        // Abstrakcyjna metoda wykonująca ruch na podanej planszy
        public abstract void Execute(Board board);
    }
}
