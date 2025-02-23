using System;
using System.Collections.Generic;

namespace ChessLogic
{
    // Klasa reprezentująca historię ruchów w grze w szachy
    public class MoveHistory
    {
        // Lista przechowująca historię stanu planszy po każdym ruchu
        private readonly List<Board> history = new List<Board>();
        // Indeks aktualnego stanu planszy w historii
        private int currentIndex;

        // Właściwość zwracająca bieżący stan planszy
        public Board CurrentBoard => history[currentIndex];

        // Właściwość sprawdzająca, czy można cofnąć ruch
        public bool CanStepBackward => currentIndex > 0;

        // Właściwość sprawdzająca, czy można wykonać ruch do przodu (po cofnięciu)
        public bool CanStepForward => currentIndex < history.Count - 1;

        // Konstruktor inicjalizujący historię ruchów z początkowym stanem planszy
        public MoveHistory(Board initialBoard)
        {
            history.Add(initialBoard.DeepCopy());
            currentIndex = 0;
        }

        // Metoda dodająca nowy ruch do historii
        public void AddMove(Move move)
        {
            // Jeśli bieżący indeks nie jest na końcu historii, usuń wszystkie przyszłe stany
            if (currentIndex < history.Count - 1)
            {
                history.RemoveRange(currentIndex + 1, history.Count - currentIndex - 1);
            }

            // Tworzenie kopii bieżącego stanu planszy i wykonanie na niej ruchu
            Board newBoard = history[^1].DeepCopy();
            move.Execute(newBoard);

            // Dodanie nowego stanu planszy do historii i aktualizacja bieżącego indeksu
            history.Add(newBoard);
            currentIndex++;
        }

        // Metoda cofająca ruch w historii
        public void StepBackward() => currentIndex = Math.Max(0, currentIndex - 1);

        // Metoda przesuwająca ruch do przodu w historii
        public void StepForward() => currentIndex = Math.Min(history.Count - 1, currentIndex + 1);
    }
}
