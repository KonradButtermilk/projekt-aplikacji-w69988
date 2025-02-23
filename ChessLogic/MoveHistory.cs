using System;
using System.Collections.Generic;

namespace ChessLogic
{
    public class MoveHistory
    {
        private readonly List<Board> history = new List<Board>();
        private int currentIndex;

        public Board CurrentBoard => history[currentIndex];
        public bool CanStepBackward => currentIndex > 0;
        public bool CanStepForward => currentIndex < history.Count - 1;

        public MoveHistory(Board initialBoard)
        {
            history.Add(initialBoard.DeepCopy());
            currentIndex = 0;
        }

        public void AddMove(Move move)
        {
            if (currentIndex < history.Count - 1)
            {
                history.RemoveRange(currentIndex + 1, history.Count - currentIndex - 1);
            }

            Board newBoard = history[^1].DeepCopy();
            move.Execute(newBoard);
            history.Add(newBoard);
            currentIndex++;
        }

        public void StepBackward() => currentIndex = Math.Max(0, currentIndex - 1);
        public void StepForward() => currentIndex = Math.Min(history.Count - 1, currentIndex + 1);
    }
}