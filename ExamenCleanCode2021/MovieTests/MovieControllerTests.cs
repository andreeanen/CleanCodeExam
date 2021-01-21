using ExamenCleanCode2021.Controllers;
using ExamenCleanCode2021.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Http;

namespace MovieTests
{
    [TestClass]
    public class MovieControllerTests
    {
        public IHttpClientFactory clientFactory;

        [TestMethod]
        public void GetOrderedMovies_ReturnAListWithMoviesOrderAscendingAfterRating()
        {
            var mockMovies = new List<Movie>()
            {
                new Movie
                {
                     Id="0",
                     Title ="The Good, the Bad and the Ugly",
                     Rated="8,8"
                },
                new Movie
                {
                    Id= "1",
                    Title= "Indiana Jones and the Last Crusade",
                    Rated ="7,6"
                }
            };

            string howToOrderMovies = "ascending";
            var controller = new MovieController(clientFactory);
            var movieList = controller.OrderMoviesByRating(mockMovies, howToOrderMovies) as List<Movie>;

            var expectedMovie = mockMovies[0];
            var actualMovie = movieList[1];

            Assert.AreEqual(expectedMovie, actualMovie);

        }

        [TestMethod]
        public void GetOrderedMovies_ReturnAListWithMoviesOrderDescendingAfterRating()
        {
            var mockMovies = new List<Movie>()
            {
                new Movie
                {
                     Id="tt0060196",
                     Title ="The Good, the Bad and the Ugly",
                     Rated="8,8"
                },
                new Movie
                {
                    Id= "tt0097576",
                    Title= "Indiana Jones and the Last Crusade",
                    Rated ="7,6"
                }
            };

            var expected = new List<Movie>()
            {
                  new Movie
                {
                     Id="tt0060196",
                     Title ="The Good, the Bad and the Ugly",
                     Rated="8,8"
                },
                new Movie
                {
                    Id= "tt0097576",
                    Title= "Indiana Jones and the Last Crusade",
                    Rated ="7,6"
                },

            };

            string howToOrderMovies = "descending";
            var controller = new MovieController(clientFactory);
            var movieList = controller.OrderMoviesByRating(mockMovies, howToOrderMovies) as List<Movie>;

            var expectedMovie = mockMovies[0];
            var actualMovie = movieList[0];

            Assert.AreEqual(expectedMovie, actualMovie);

        }

    }
}
