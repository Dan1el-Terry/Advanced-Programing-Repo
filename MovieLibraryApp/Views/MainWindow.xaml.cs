using System.Windows;
using MovieLibraryApp.Models;
using MovieLibraryApp.ViewModels;

namespace MovieLibraryApp
{
    public partial class MainWindow : Window
    {
        private MovieLibraryViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MovieLibraryViewModel();

            // Example data
            viewModel.AddMovie(new Movie { MovieID = "M001", Title = "Inception", Director = "Christopher Nolan", Genre = "Sci-Fi", ReleaseYear = 2010 });
            MoviesDataGrid.ItemsSource = viewModel.Movies;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var results = viewModel.SearchByTitle(SearchTextBox.Text);
            MoviesDataGrid.ItemsSource = results;
        }

        private void SortTitleButton_Click(object sender, RoutedEventArgs e)
        {
            // Placeholder: implement Bubble Sort
        }

        private void SortYearButton_Click(object sender, RoutedEventArgs e)
        {
            // Placeholder: implement Merge Sort
        }

        private void BorrowButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.BorrowMovie(MovieIDTextBox.Text, UserTextBox.Text);
            MoviesDataGrid.Items.Refresh();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ReturnMovie(MovieIDTextBox.Text);
            MoviesDataGrid.Items.Refresh();
        }
    }
}