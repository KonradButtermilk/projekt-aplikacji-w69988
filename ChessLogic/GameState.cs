using ChessLogic.Moves;
using ChessLogic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
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

        public IEnumerable<Move> LegalMovesForPieces(Position position)
        {
            if(Board.IsEmpty(position) || Board[position].Color != CurrentPalyer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[position];
            return piece.GetMoves(position, Board);
        }

        public void MakeMove(Move move)
        {
           move.Execute(Board);
            CurrentPalyer = CurrentPalyer.Opponent();
        }   

    }
}
