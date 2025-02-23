using System.Collections.Generic;
using System.Linq;

namespace ChessLogic
{
    // Klasa reprezentująca stan gry w szachy
    public class GameState
    {
        // Właściwości przechowujące bieżącą planszę i bieżącego gracza
        public Board Board { get; private set; }
        public Player CurrentPlayer { get; private set; }

        // Konstruktor inicjalizujący stan gry z podanym graczem i planszą
        public GameState(Player player, Board board)
        {
            CurrentPlayer = player;
            Board = board;
        }

        // Metoda zwracająca legalne ruchy dla figury na podanej pozycji
        public IEnumerable<Move> LegalMovesForPieces(Position position)
        {
            // Sprawdzenie, czy pozycja jest pusta lub figura na niej nie należy do bieżącego gracza
            if (Board.IsEmpty(Board, position) || Board[position].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            // Pobranie figury z podanej pozycji
            Piece piece = Board[position];
            // Zwrócenie możliwych ruchów dla danej figury
            return piece.GetMoves(position, Board);
        }

        // Metoda wykonująca ruch i zmieniająca bieżącego gracza
        public void MakeMove(Move move)
        {
            move.Execute(Board); // Wykonanie ruchu na planszy
            CurrentPlayer = CurrentPlayer.Opponent(); // Zmiana bieżącego gracza na przeciwnika
        }
    }
}
