using System;
using System.Collections.Generic;
using MovieLibraryApp.Models;

namespace MovieLibraryApp.ViewModels
{
    public class MovieLibraryViewModel
    {
        public LinkedList<Movie> Movies { get; set; } = new LinkedList<Movie>();
        public Dictionary<string, Movie> MovieHashtable { get; set; } = new Dictionary<string, Movie>();
        public Dictionary<string, Queue<string>> BorrowQueues { get; set; } = new Dictionary<string, Queue<string>>();

        public void AddMovie(Movie movie)
        {
            if (!MovieHashtable.ContainsKey(movie.MovieID))
            {
                Movies.AddLast(movie);
                MovieHashtable[movie.MovieID] = movie;
                BorrowQueues[movie.MovieID] = new Queue<string>();
            }
        }

        public List<Movie> SearchByTitle(string title)
        {
            // Linear search skeleton
            List<Movie> results = new List<Movie>();
            foreach (var movie in Movies)
            {
                if (movie.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                    results.Add(movie);
            }
            return results;
        }

        public Movie SearchByID(string movieID)
        {
            // Hashtable lookup
            return MovieHashtable.ContainsKey(movieID) ? MovieHashtable[movieID] : null;
        }

        public void BorrowMovie(string movieID, string user)
        {
            if (MovieHashtable.ContainsKey(movieID))
            {
                var movie = MovieHashtable[movieID];
                if (movie.IsAvailable)
                    movie.IsAvailable = false;
                else
                    BorrowQueues[movieID].Enqueue(user);
            }
        }

        public void ReturnMovie(string movieID)
        {
            if (MovieHashtable.ContainsKey(movieID))
            {
                var movie = MovieHashtable[movieID];
                if (BorrowQueues[movieID].Count > 0)
                {
                    // Assign to next user in queue
                    var nextUser = BorrowQueues[movieID].Dequeue();
                    Console.WriteLine($"Movie assigned to {nextUser}");
                }
                else
                    movie.IsAvailable = true;
            }
        }
    }
}