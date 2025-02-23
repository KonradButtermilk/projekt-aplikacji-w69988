using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ChessGame.cs
namespace ChessLogic
{
    // Klasa reprezentująca grę w szachy
    public class ChessGame
    {
        // Identyfikator gry
        public int Id { get; set; }

        // Nazwa białego gracza
        public string WhitePlayer { get; set; }

        // Nazwa czarnego gracza
        public string BlackPlayer { get; set; }

        // Ruchy wykonane podczas gry, zapisane w formacie tekstowym
        public string Moves { get; set; }

        // Data, kiedy odbyła się gra
        public DateTime GameDate { get; set; }

        // Wynik gry (np. wygrana białych, wygrana czarnych, remis)
        public string Result { get; set; }
    }
}
