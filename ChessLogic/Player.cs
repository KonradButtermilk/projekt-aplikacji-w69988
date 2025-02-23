using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    // Enum reprezentujący gracza w szachach
    public enum Player
    {
        None,  // Brak gracza
        White, // Biały gracz
        Black  // Czarny gracz
    }

    // Klasa rozszerzeń dla typu Player
    public static class PlayerExtensions
    {
        // Metoda rozszerzająca zwracająca przeciwnika danego gracza
        public static Player Opponent(this Player player)
        {
            // Instrukcja switch, działająca jak bardziej zwięzły if
            return player switch
            {
                // Jeśli gracz jest biały, zwróć czarnego; jeśli gracz jest czarny, zwróć białego; w przeciwnym razie zwróć brak gracza
                Player.White => Player.Black,
                Player.Black => Player.White,
                _ => Player.None,
            };
        }
    }
}
