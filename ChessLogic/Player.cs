using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public enum Player
    {
        None,
        White,
        Black
    }

    public static class PlayerExtensions
    {
        public static Player Opponent(this Player player)
        {
            //This a switch statement, it is like an if statement but more compact
            return player switch
            {
                //If player is white, return black, if player is black, return white, else return none
                Player.White => Player.Black,
                //If player is black, return white, if player is white, return black, else return none
                Player.Black => Player.White,
                //If player is none, return none
                _ => Player.None,
            };
        }

    }
}
