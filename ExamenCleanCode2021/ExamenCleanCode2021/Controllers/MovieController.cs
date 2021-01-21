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
        private readonly IHttpClientFactory clientFactory;

        public MovieController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }


        [HttpGet("detailed")]
        public async Task<ActionResult> GetDetailedList()
        {

            var client = clientFactory.CreateClient();
            var result = await client.GetStringAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/detailedMovies.json");
            var movies = JsonSerializer.Deserialize<List<Movie>>(result);

            return Ok(movies);
        }

        [HttpGet("toplist")]
        public async Task<IActionResult> Toplist()
        {
            //List<string> movies = new List<string>();
            var url = "https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json";
            var client = clientFactory.CreateClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await client.GetStringAsync(url);
                var movies = JsonSerializer.Deserialize<List<Movie>>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                string howToOrderMovies = "ascending";
                var moviesOrderedByRating = GetOrderedMovies(movies, howToOrderMovies);

                var movieTitleVisitor = new MovieTitleVisitor();
                var listOfMovieTitles = movieTitleVisitor.GetListOfMovieTitles(moviesOrderedByRating);

                return Ok(listOfMovieTitles);

            }
            return NotFound();

        }

        public List<Movie> GetOrderedMovies(List<Movie> movies, string howToOrderMovies)
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
            var url = "https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json";
            var client = clientFactory.CreateClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await client.GetStringAsync(url);
                var movieList = JsonSerializer.Deserialize<List<Movie>>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                var movieWithId = movieList.Where(m => m.Id == id).FirstOrDefault();

                return Ok(movieWithId);
            }
            return NotFound();
        }
    }
}
