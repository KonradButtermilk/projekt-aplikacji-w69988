﻿using ChessLogic.Pieces;

namespace ChessLogic
{
    // Klasa reprezentująca szachownicę
    public class Board
    {
        // Tablica 2D przechowująca figury na szachownicy
        private readonly Piece[,] pieces = new Piece[8, 8];

        // Indeksator umożliwiający dostęp do figury na szachownicy za pomocą wiersza i kolumny
        public Piece this[int row, int col]
        {
            get => pieces[row, col];
            set => pieces[row, col] = value;
        }

        // Indeksator umożliwiający dostęp do figury na szachownicy za pomocą obiektu Position
        public Piece this[Position pos]
        {
            get => pieces[pos.Row, pos.Column];
            set => pieces[pos.Row, pos.Column] = value;
        }

        // Statyczna metoda sprawdzająca, czy pozycja znajduje się wewnątrz szachownicy
        public static bool IsInside(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
        }

        // Statyczna metoda sprawdzająca, czy pozycja na szachownicy jest pusta
        public static bool IsEmpty(Board board, Position pos)
        {
            return board[pos] == null;
        }

        // Metoda tworząca głęboką kopię szachownicy
        // Tworzymy nową instancję Board i kopiujemy wszystkie figury na nowe pozycje
        public Board DeepCopy()
        {
            Board copy = new Board();
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    copy.pieces[row, col] = pieces[row, col]?.Clone();
                }
            }
            return copy;
        }

        // Statyczna metoda tworząca i inicjalizująca szachownicę z początkowym ustawieniem figur
        public static Board Initial()
        {
            Board board = new Board();
            AddDefaultPieces(board);
            return board;
        }

        // Prywatna metoda dodająca domyślne figury do szachownicy
        private static void AddDefaultPieces(Board board)
        {
            // Ustawienie pionków
            for (int col = 0; col < 8; col++)
            {
                board[1, col] = new Pawn(Player.Black);
                board[6, col] = new Pawn(Player.White);
            }

            // Ustawienie wież
            board[0, 0] = new Rook(Player.Black);
            board[0, 7] = new Rook(Player.Black);
            board[7, 0] = new Rook(Player.White);
            board[7, 7] = new Rook(Player.White);

            // Ustawienie skoczków
            board[0, 1] = new Knight(Player.Black);
            board[0, 6] = new Knight(Player.Black);
            board[7, 1] = new Knight(Player.White);
            board[7, 6] = new Knight(Player.White);

            // Ustawienie gońców
            board[0, 2] = new Bishop(Player.Black);
            board[0, 5] = new Bishop(Player.Black);
            board[7, 2] = new Bishop(Player.White);
            board[7, 5] =  new Bishop(Player.White);

            // Ustawienie królowych
            board[0, 3] =  new Queen(Player.Black);
            board[7, 3] =  new Queen(Player.White);

            // Ustawienie królów
            board[0, 4] =  new King(Player.Black);
            board[7, 4] =  new King(Player.White);
        }
    }
}
