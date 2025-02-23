using System.Windows;
using System.Windows.Input;
using ChessLogic;

namespace ChessUI
{

    public partial class GameHistoryWindow : Window
    {
        private const string ConnectionString = @"Server=KONRADPC\SQLEXPRESS;Database=ChessDB;Integrated Security=True;TrustServerCertificate=True;Encrypt=True";
        public ChessGame SelectedGame { get; private set; }

        public GameHistoryWindow()
        {
            InitializeComponent();
            LoadGames();
        }

        private void LoadGames()
        {
            var repository = new SqlGameRepository(ConnectionString);
            gamesDataGrid.ItemsSource = repository.GetAllGames();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gamesDataGrid.SelectedItem is ChessGame selectedGame)
            {
                SelectedGame = selectedGame;
                DialogResult = true;
                Close();
            }
        }

    }
}