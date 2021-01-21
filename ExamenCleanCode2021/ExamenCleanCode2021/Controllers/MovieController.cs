using ExamenCleanCode2021.Models;
using ExamenCleanCode2021.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExamenCleanCode2021.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        public IHttpClientFactory clientFactory;

        public MovieController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }


        [HttpGet("detailed/order={order}")]
        public async Task<ActionResult> GetCombined(string order)
        {
            List<Movie> moviesCombined = await GetCombineMoviesList();

            var moviesCombinedOrderedByRating = OrderMoviesByRating(moviesCombined, order);

            var movieTitleVisitor = new MovieTitleVisitor();
            var listOfMovieTitlesCombined = movieTitleVisitor.GetListOfMovieTitles(moviesCombinedOrderedByRating);

            return Ok(listOfMovieTitlesCombined);
        }

        private async Task<List<Movie>> GetCombineMoviesList()
        {
            var client = clientFactory.CreateClient();
            var result = await client.GetStringAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json");
            var movies = JsonSerializer.Deserialize<List<Movie>>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var resultDetailed = await client.GetStringAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/detailedMovies.json");
            var detailedMovies = JsonSerializer.Deserialize<List<DetailedMovie>>(resultDetailed, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
                .Select(x => new Movie { Id = x.Id, Title = x.Title, Rated = x.ImdbRating.ToString() });

            var existingTitles = movies.Select(x => x.Title.ToLower());
            var filteredDetailed = detailedMovies.Where(x => !existingTitles.Contains(x.Title.ToLower())).ToList();
            var moviesCombined = movies.Concat(filteredDetailed).ToList();
            return moviesCombined;
        }

        [HttpGet("toplist/order={order}")]
        public async Task<IActionResult> GetToplist(string order)
        {
            var url = "https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json";
            var client = clientFactory.CreateClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await client.GetStringAsync(url);
                var movies = JsonSerializer.Deserialize<List<Movie>>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                var moviesOrderedByRating = OrderMoviesByRating(movies, order);

                var movieTitleVisitor = new MovieTitleVisitor();
                var listOfMovieTitles = movieTitleVisitor.GetListOfMovieTitles(moviesOrderedByRating);

                return Ok(listOfMovieTitles);

            }
            return NotFound();

        }

        public List<Movie> OrderMoviesByRating(List<Movie> movies, string howToOrderMovies)
        {
            var moviesToOrder = movies;
            if (howToOrderMovies == "ascending")
            {
                moviesToOrder = movies.OrderBy(m => m.Rated).ToList();

            }
            else if (howToOrderMovies == "descending")
            {
                moviesToOrder = moviesToOrder = movies.OrderByDescending(m => m.Rated).ToList();
            }

            return moviesToOrder;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(string id)
        {
            List<Movie> moviesCombined = await GetCombineMoviesList();
            var movieWithId = moviesCombined.Where(m => m.Id == id).FirstOrDefault();

            return Ok(movieWithId);
        }
    }
}
