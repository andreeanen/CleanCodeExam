using ExamenCleanCode2021.Models;
using System.Collections.Generic;


namespace ExamenCleanCode2021.Services
{
    public class MovieTitleVisitor
    {
        public List<string> GetListOfMovieTitles(List<Movie> movies)
        {
            List<string> titles = new List<string>();

            foreach (var movie in movies)
            {
                string title = GetMovieTitle(movie);
                titles.Add(title);
            }

            return titles;
        }

        private string GetMovieTitle(Movie movie)
        {
            return movie.Title;
        }
    }
}
