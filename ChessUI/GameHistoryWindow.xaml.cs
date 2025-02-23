using System.Windows;
using System.Windows.Input;
using ChessLogic;

namespace ChessUI
{
    // Klasa reprezentująca okno historii gier w interfejsie użytkownika
    public partial class GameHistoryWindow : Window
    {
        // Stała przechowująca ciąg połączenia do bazy danych SQL
        private const string ConnectionString = @"Server=KONRADPC\SQLEXPRESS;Database=ChessDB;Integrated Security=True;TrustServerCertificate=True;Encrypt=True";

        // Właściwość przechowująca wybraną grę
        public ChessGame SelectedGame { get; private set; }

        // Konstruktor inicjalizujący okno i ładujący gry
        public GameHistoryWindow()
        {
            InitializeComponent(); // Inicjalizacja komponentów okna
            LoadGames(); // Załadowanie gier do widoku
        }

        // Metoda ładująca gry z bazy danych i ustawiająca źródło danych dla kontrolki DataGrid
        private void LoadGames()
        {
            var repository = new SqlGameRepository(ConnectionString); // Tworzenie instancji repozytorium gier SQL
            gamesDataGrid.ItemsSource = repository.GetAllGames(); // Ustawienie źródła danych dla DataGrid na wyniki zapytania do bazy danych
        }

        // Obsługa zdarzenia podwójnego kliknięcia myszy na kontrolce DataGrid
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Sprawdzenie, czy wybranym elementem jest obiekt ChessGame
            if (gamesDataGrid.SelectedItem is ChessGame selectedGame)
            {
                SelectedGame = selectedGame; // Ustawienie wybranej gry
                DialogResult = true; // Ustawienie wyniku dialogu na true
                Close(); // Zamknięcie okna
            }
        }
    }
}
