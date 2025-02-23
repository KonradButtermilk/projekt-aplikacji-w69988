using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ChessGame.cs
namespace ChessLogic
{
    public class ChessGame
    {
        public int Id { get; set; }
        public string WhitePlayer { get; set; }
        public string BlackPlayer { get; set; }
        public string Moves { get; set; }
        public DateTime GameDate { get; set; }
        public string Result { get; set; }
    }
}

