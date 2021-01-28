using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesList.Models;
using StarWarsApiCSharp;

namespace MoviesList.Controllers
{
    public class FilmsController : Controller
    {
        private readonly IGenericRepository<FilmViewModel> filmRepository;
        private readonly IGenericRepository<Person> personRepository;
        private readonly IGenericRepository<Planet> planetRepository;
        private readonly IGenericRepository<Starship> starshipRepository;
        private readonly IGenericRepository<Vehicle> vehicleRepository;
        private readonly ApplicationDbContext dbContext;

        public FilmsController(
            IGenericRepository<FilmViewModel> filmRepository,
            IGenericRepository<Person> personRepository,
            IGenericRepository<Planet> planetRepository,
            IGenericRepository<Starship> starshipRepository,
            IGenericRepository<Vehicle> vehicleRepository,
            ApplicationDbContext dbContext)
        {
            this.filmRepository = filmRepository;
            this.personRepository = personRepository;
            this.planetRepository = planetRepository;
            this.starshipRepository = starshipRepository;
            this.vehicleRepository = vehicleRepository;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var films = filmRepository.GetAll("films");
            return View(films);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var film = filmRepository.GetById(id, "films");

            // All objects:
            //film.Characters = film.Characters?.Select(personUrl => personRepository.GetByUrl(personUrl)?.Name).ToList();
            //film.Planets = film.Planets?.Select(planetUrl => planetRepository.GetByUrl(planetUrl)?.Name).ToList();
            //film.Starships = film.Starships?.Select(starshipUrl => starshipRepository.GetByUrl(starshipUrl)?.Name).ToList();
            //film.Vehicles = film.Vehicles?.Select(vehicleUrl => vehicleRepository.GetByUrl(vehicleUrl)?.Name).ToList();

            // Returning only one of each due to high api response time. 
            film.Characters = new List<string>() { personRepository.GetByUrl(film.Characters?.First()).Name };
            film.Planets = new List<string>() { planetRepository.GetByUrl(film.Planets?.First()).Name };
            film.Starships = new List<string>() { starshipRepository.GetByUrl(film.Starships?.First()).Name };
            film.Vehicles = new List<string>() { vehicleRepository.GetByUrl(film.Vehicles?.First()).Name };

            var ratings = dbContext.Ratings?.Where(rating => rating.FilmId == id);
            if (ratings.Any())
            {
                var avgScore = ratings.Select(rating => rating.Score)?.Average();
                film.AverageScore = avgScore;
            }
            return View(film);
        }

        [HttpPost]
        public async Task<IActionResult> RateFilm(int id, int score)
        {
            if (!ModelState.IsValid)
                return null;
            if (score < 0 || score > 10)
                return null;
            var rating = new Rating()
            {
                FilmId = id,
                Score = score
            };
            dbContext.Ratings.Add(rating);
            await dbContext.SaveChangesAsync();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
