using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace ChessLogic
{
    public class SqlGameRepository : IGameRepository
    {
        private readonly string connectionString;

        public SqlGameRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<ChessGame> GetAllGames()
        {
            List<ChessGame> games = new List<ChessGame>();

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(
                    "SELECT Id, WhitePlayer, BlackPlayer, Moves, GameDate, Result FROM ChessGames",
                    connection);

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    games.Add(new ChessGame
                    {
                        Id = reader.GetInt32(0),
                        WhitePlayer = reader.GetString(1),
                        BlackPlayer = reader.GetString(2),
                        Moves = reader.GetString(3),
                        GameDate = reader.GetDateTime(4),
                        Result = reader.GetString(5)
                    });
                }
            }
            return games;
        }
    }
}