using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    // Abstrakcyjna klasa reprezentująca figurę szachową
    public abstract class Piece
    {
        // Abstrakcyjna cecha określająca typ figury
        public abstract PieceType Type { get; }

        // Abstrakcyjna cecha określająca kolor figury
        public abstract Player Color { get; }

        // Właściwość określająca, czy figura wykonała ruch
        public bool HasMoved { get; set; } = false;

        // Abstrakcyjna metoda do klonowania figury
        public abstract Piece Clone();

        // Abstrakcyjna metoda do kopiowania figury
        public abstract Piece Copy();

        // Abstrakcyjna metoda zwracająca możliwe ruchy figury z danej pozycji na planszy
        public abstract IEnumerable<Move> GetMoves(Position fromPosition, Board board);

        // Chroniona metoda zwracająca możliwe pozycje ruchu figury w jednym kierunku
        // Używamy pętli do przeszukiwania pozycji w danym kierunku, aż napotkamy koniec planszy lub inną figurę
        protected IEnumerable<Position> MovePositionInDirection(Position from, Board board, Direction direction)
        {
            for (Position position = from + direction; Board.IsInside(position); position += direction)
            {
                // Jeśli pozycja jest pusta, dodajemy ją do możliwych ruchów
                if (Board.IsEmpty(board, position))
                {
                    yield return position;
                    continue;
                }

                // Jeśli na pozycji znajduje się figura przeciwnika, dodajemy ją do możliwych ruchów
                Piece piece = board[position];
                if (piece.Color != Color)
                {
                    yield return position;
                }

                // Jeśli napotkamy jakąkolwiek figurę, przerywamy przeszukiwanie w tym kierunku
                yield break;
            }
        }

        // Chroniona metoda zwracająca możliwe pozycje ruchu figury we wszystkich kierunkach
        // Używamy metody MovePositionInDirection do przeszukiwania pozycji we wszystkich podanych kierunkach
        protected IEnumerable<Position> MovePositionInDirections(Position from, Board board, Direction[] directions)
        {
            return directions.SelectMany(direction => MovePositionInDirection(from, board, direction));
        }
    }
}
