using System.Collections.Generic;

namespace MovieLibraryApp.Models
{
    public class MovieManager
    {
        // REQUIRED DATA STRUCTURES
        private Dictionary<string, Movie> movieTable = new Dictionary<string, Movie>(); // Hashtable
        private LinkedList<Movie> movieList = new LinkedList<Movie>();                 // Linked List
        private Queue<string> borrowQueue = new Queue<string>();                      // Queue

        private int nextId = 1;

        // -------------------------
        // ADD MOVIE (AUTO ID)
        // -------------------------
        public Movie AddMovie(string title, string director, string genre, int year)
        {
            string id = "M" + nextId.ToString("000");
            nextId++;

            Movie movie = new Movie
            {
                MovieID = id,
                Title = title,
                Director = director,
                Genre = genre,
                ReleaseYear = year,
                IsAvailable = true
            };

            movieList.AddLast(movie);
            movieTable[id] = movie;

            return movie;
        }

        // -------------------------
        // FAST LOOKUP (HASH TABLE)
        // -------------------------
        public Movie GetById(string id)
        {
            return movieTable.ContainsKey(id) ? movieTable[id] : null;
        }

        // -------------------------
        // BORROW MOVIE
        // -------------------------
        public bool BorrowMovie(string id, string user = "")
        {
            var movie = GetById(id);

            if (movie == null)
                return false;

            if (!movie.IsAvailable)
            {
                borrowQueue.Enqueue(user); // waiting list
                return false;
            }

            movie.IsAvailable = false;
            return true;
        }

        // -------------------------
        // RETURN MOVIE
        // -------------------------
        public void ReturnMovie(string id)
        {
            var movie = GetById(id);

            if (movie == null)
                return;

            movie.IsAvailable = true;

            // If someone is waiting
            if (borrowQueue.Count > 0)
            {
                string nextUser = borrowQueue.Dequeue();
                movie.IsAvailable = false;
                // (later: show MessageBox notification)
            }
        }

        // -------------------------
        // GET ALL MOVIES (LinkedList)
        // -------------------------
        public List<Movie> GetAllMovies()
        {
            return new List<Movie>(movieList);
        }
    }
}