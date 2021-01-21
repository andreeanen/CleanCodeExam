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

        //[TestMethod]
        //public void GetMovieById_OrdersIsInstantiated_ReturnsOk()
        //{
        //    var controller = new MovieController(clientFactory);
        //    var mockMovie = new Movie()
        //    {
        //        Id = "tt0060196",
        //        Title = "The Good, the Bad and the Ugly",
        //        Rated = "8,8"
        //    };
        //    string id = "tt0060196";
        //    var actual = controller.GetMovieById(id);

        //    Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
        //}


        [TestMethod]
        public void GetOrderedMovies_ReturnAListWithMoviesOrderAscendingAfterRating()
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
                    Id= "tt0097576",
                    Title= "Indiana Jones and the Last Crusade",
                    Rated ="7,6"
                },
                new Movie
                {
                     Id="tt0060196",
                     Title ="The Good, the Bad and the Ugly",
                     Rated="8,8"
                },
            };

            string howToOrderMovies = "ascending";
            var controller = new MovieController(clientFactory);
            var actual = controller.OrderMoviesByRating(mockMovies, howToOrderMovies);

            actual.Equals(expected);
            //CollectionAssert.AreEquivalent(expected, actual);

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
            var actual = controller.OrderMoviesByRating(mockMovies, howToOrderMovies);

            actual.Equals(expected);

        }

    }
}
