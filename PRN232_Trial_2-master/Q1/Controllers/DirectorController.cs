using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.DAO;
using Q1.Models;
using Q1.ViewModels;
using System.Globalization;

namespace Q1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorController : Controller
    {
        [HttpGet("~/api/director/getdirectors/{nationality}/{gender}")]
        public IActionResult GetDirectors(string? nationality, string? gender)
        {
            var context = new PePrnFall22B1Context();

            var directors = context.Directors.ToList();

            if (nationality != null)
            {
                directors = directors.Where(x => x.Nationality.ToLower() == nationality.ToLower()).ToList();
            }

            if (gender != null)
            {
                if (gender.ToLower() == "male")
                {
                    directors = directors.Where(x => x.Male == true).ToList();
                }
                else
                {
                    directors = directors.Where(x => x.Male == false).ToList();
                }
            }

            List<DirectorViewModel> viewModel = new List<DirectorViewModel>();

            foreach (var director in directors)
            {
                DirectorViewModel temp = new DirectorViewModel();

                temp.id = director.Id;
                temp.fullName = director.FullName;
                temp.gender = "Female";
                if (director.Male == true)
                {
                    temp.gender = "Male";
                }
                temp.dob = director.Dob;
                DateTime date = director.Dob;
                temp.dobString = date.ToString("M/d/yyyy");
                temp.nationality = director.Nationality;
                temp.description = director.Description;

                viewModel.Add(temp);
            }

            return Ok(viewModel);
        }

        [HttpGet("~/api/director/getdirector/{id}")]
        public IActionResult GetDirector(int? id)
        {
            var context = new PePrnFall22B1Context();

            var directors = context.Directors
                .Include(x => x.Movies)
                .ToList();

            DirectorViewModel2 viewModel = new DirectorViewModel2();            

            var temp = directors.Where(x => x.Id == id).FirstOrDefault();            

            viewModel.id = temp.Id;
            viewModel.fullName = temp.FullName;
            viewModel.gender = "Female";
            if (temp.Male == true)
            {
                viewModel.gender = "Male";
            }
            viewModel.dob = temp.Dob;
            DateTime date = temp.Dob;
            viewModel.dobString = date.ToString("M/d/yyyy");
            viewModel.nationality = temp.Nationality;
            viewModel.description = temp.Description;
            
            foreach(var movie in temp.Movies)
            {
                MovieViewModel tempMovie = new MovieViewModel();

                tempMovie.id = movie.Id;
                tempMovie.title = movie.Title;
                tempMovie.releaseDate = movie.ReleaseDate;
                tempMovie.realeaseYear = Int32.Parse(movie.ReleaseDate?.ToString("yyyy", CultureInfo.InvariantCulture));
                tempMovie.description = movie.Description;
                tempMovie.language = movie.Language;
                tempMovie.producerId = movie.ProducerId;
                tempMovie.directorId = movie.DirectorId;
                tempMovie.directorName = context.Directors.FirstOrDefault(x => x.Id == movie.DirectorId)?.FullName;
                tempMovie.producerName = context.Producers.FirstOrDefault(x => x.Id == movie.ProducerId)?.Name;
                tempMovie.genres = movie.Genres;
                tempMovie.stars = movie.Stars;

                viewModel.Movies.Add(tempMovie);
            }

            return Ok(viewModel);
        }

        [HttpPost("~/api/Director/Create")]
        public IActionResult Create([FromBody] AddDirectorViewMoel director)
        {
            try
            {
                var context = new PePrnFall22B1Context();

                var temp = new Director();

                temp.FullName = director.fullName;
                temp.Male = director.male;
                temp.Dob = director.dob;
                temp.Nationality = director.nationality;
                temp.Description = director.description;

                context.Directors.Add(temp);
                context.SaveChanges();

                return Ok(1);
            } catch (Exception e)
            {
                return Conflict("There is an error while adding");
            }            
        }

    }
}
