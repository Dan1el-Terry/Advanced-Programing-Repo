using System.Collections.ObjectModel;
using System.Windows;
using MovieLibraryApp.Models;

namespace MovieLibraryApp
{
    public partial class MainWindow : Window
    {
        private MovieManager manager = new MovieManager();
        private ObservableCollection<Movie> movies = new ObservableCollection<Movie>();

        public MainWindow()
        {
            InitializeComponent();
            MovieGrid.ItemsSource = movies;
        }

        private void AddMovie_Click(object sender, RoutedEventArgs e)
        {
            // Send data to backend (NO ID here anymore)
            var movie = manager.AddMovie(
                TitleBox.Text,
                DirectorBox.Text,
                GenreBox.Text,
                int.TryParse(YearBox.Text, out int y) ? y : 0
            );

            // Update UI list
            RefreshGrid();

            // Clear inputs
            TitleBox.Clear();
            DirectorBox.Clear();
            GenreBox.Clear();
            YearBox.Clear();
        }

        private void RefreshGrid()
        {
            movies.Clear();

            foreach (var m in manager.GetAllMovies())
            {
                movies.Add(m);
            }
        }
    }
}