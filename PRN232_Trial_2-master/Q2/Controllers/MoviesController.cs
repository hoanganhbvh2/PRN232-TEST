using givenAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Q2.Service;

namespace Q2.Controllers
{
    [Route("/[controller]/[action]")]
    public class MoviesController : Controller
    {
        private readonly ApiClientService _apiClient;

        public MoviesController(ApiClientService apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet]        
        public IActionResult Director_Movie(int? directorId)
        {
            var movies = _apiClient.GetAllMovies();
            var directors = _apiClient.GetAllDirectors();

            if (directorId != null)
            {
                movies = movies.Where(x => x.DirectorId == directorId).ToList();
            }

            var viewModel = new ViewModels.DirectorMovie
            {
                movies = movies.ToList(),
                directors = directors.ToList()
            };            

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Delete(int movieId)
        {
            _apiClient.DeleteMovie(movieId);
            return RedirectToAction("Director_Movie", "Movies");
        }
    }
}
