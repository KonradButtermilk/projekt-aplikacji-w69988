using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace ChessLogic
{
    // Klasa reprezentująca repozytorium gier szachowych wykorzystujące bazę danych SQL
    public class SqlGameRepository : IGameRepository
    {
        // Prywatne pole przechowujące ciąg połączenia do bazy danych
        private readonly string connectionString;

        // Konstruktor inicjalizujący repozytorium z podanym ciągiem połączenia
        public SqlGameRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Metoda zwracająca wszystkie gry z bazy danych jako kolekcję obiektów ChessGame
        public IEnumerable<ChessGame> GetAllGames()
        {
            List<ChessGame> games = new List<ChessGame>();

            // Tworzenie połączenia z bazą danych przy użyciu podanego ciągu połączenia
            using (var connection = new SqlConnection(connectionString))
            {
                // Tworzenie komendy SQL do pobrania wszystkich gier z tabeli ChessGames
                var command = new SqlCommand(
                    "SELECT Id, WhitePlayer, BlackPlayer, Moves, GameDate, Result FROM ChessGames",
                    connection);

                connection.Open();  // Otwarcie połączenia z bazą danych
                var reader = command.ExecuteReader();  // Wykonanie komendy i pobranie danych

                // Odczytywanie wyników zapytania SQL
                while (reader.Read())
                {
                    // Dodawanie odczytanej gry do listy
                    games.Add(new ChessGame
                    {
                        Id = reader.GetInt32(0),  // Odczytanie Id gry
                        WhitePlayer = reader.GetString(1),  // Odczytanie nazwy białego gracza
                        BlackPlayer = reader.GetString(2),  // Odczytanie nazwy czarnego gracza
                        Moves = reader.GetString(3),  // Odczytanie ruchów wykonanych podczas gry
                        GameDate = reader.GetDateTime(4),  // Odczytanie daty gry
                        Result = reader.GetString(5)  // Odczytanie wyniku gry
                    });
                }
            }
            return games;  // Zwrócenie listy wszystkich gier
        }
    }
}
