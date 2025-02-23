using System.Collections.Generic;

namespace ChessLogic
{
    public interface IGameRepository
    {
        IEnumerable<ChessGame> GetAllGames();
    }
}