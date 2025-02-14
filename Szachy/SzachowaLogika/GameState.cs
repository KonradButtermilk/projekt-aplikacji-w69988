using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    public class GameState
    {
        public Board Board { get; set; }
        public Player CurrentPalyer { get; private set; }

        public GameState(Player player, Board board)
        {

            CurrentPalyer = player;
            Board = board;
        }

        public IEnumerable<Move> LegalMovesForPieces(Pozycja pozycja)
        {
            if (Board.IsEmpty(pozycja) || Board[pozycja].Color != CurrentPalyer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pozycja];
            return piece.GetMoves(pozycja, Board);
        }

        public void MakeMove(Move move)
        {
            move.Execute(Board);
            CurrentPalyer = CurrentPalyer.Opponent();
        }

    }
}
