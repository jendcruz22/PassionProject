using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassionProject.Models;
using System.Diagnostics;

namespace PassionProject.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Movie/List
        [HttpGet]
        public ActionResult List(string searchKey = null)
        {
            //this class will help us gather information from the blockbuster database
            MovieDataController controller = new MovieDataController();
            IEnumerable<Movie> movies = controller.ListMovies(searchKey);
            return View(movies);
        }

        // GET : /Movie/Show/{id}
        [HttpGet]
        [Route("Movie/Show/{id}")]
        public ActionResult Show(int id)
        {
            MovieDataController controller = new MovieDataController();
            Movie selectedMovie = controller.FindMovie(id);

            return View(selectedMovie);
        }

        // GET : /Movie/New
        public ActionResult New()
        {
            return View();
        }

        // POST : /Movie/Create
        [HttpPost]
        public ActionResult Create(string poster, string name, string genre, string description, DateTime dor, decimal cost)
        {
            Movie newMovie = new Movie();
            newMovie.Poster = poster;
            newMovie.Name = name;
            newMovie.Genre = genre;
            newMovie.DOR = dor;
            newMovie.Description = description;
            newMovie.Cost = cost;

            MovieDataController controller = new MovieDataController();
            controller.AddMovie(newMovie);

            return RedirectToAction("List");
        }

        /// <summary>
        /// Routes a dynamically generated "Movie Update" Page. Gathers information from the blockbuster database.
        /// </summary>
        /// <param name="id">Id of the Movie</param>
        /// <returns>A dynamic "Update Movie" webpage which provides the current information of the movie and asks the user for new information as part of a form</returns>
        // <example>GET : /Movie/Update/5</example>
        public ActionResult Update(int id)
        {
            MovieDataController controller = new MovieDataController();
            Movie selectedMovie = controller.FindMovie(id);
            return View(selectedMovie);
        }

        /// <summary>
        /// Receive a POST request containing information about an existing movie in the system, with new values. Conveys this information to the API, and redirects to the "Movie Show" page of our updated movie.
        /// </summary>
        /// <param name="id">Id of the Movie to update</param>
        /// <param name="poster">The updated link to the movie poster</param>
        /// <param name="name">The updated movie name of the movie</param>
        /// <param name="genre">The updated genre of the movie</param>
        /// <param name="description">The updated description of the movie</param>
        /// <param name="dor">The updated date of release of the movie</param>
        /// <param name="cost">The updated price of renting the movie</param>
        /// <returns>A dynamic webpage which provides the current information of the movie.</returns>
        /// <example>
        // POST : /Movie/Update/{id}
        /// FORM DATA / POST DATA / REQUEST BODY
        /// {
        /// "name":"Pulp Fiction",
        /// "poster":"image link",
        /// "genre":"Crime, Drama",
        /// "description":"The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
        /// "dor":"1994-10-14",
        /// "cost":"20.23"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string poster, string name, string genre, string description, DateTime dor, decimal cost)
        {
            Movie movieInfo = new Movie();
            movieInfo.Poster = poster;
            movieInfo.Name = name;
            movieInfo.Genre = genre;
            movieInfo.Description = description;
            movieInfo.DOR = dor;
            movieInfo.Cost = cost;

            MovieDataController controller = new MovieDataController();
            controller.UpdateMovie(id, movieInfo);

            return RedirectToAction("Show/" + id);
        }

        // GET : /Movie/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            MovieDataController controller = new MovieDataController();
            Movie newMovie = controller.FindMovie(id);

            return View(newMovie);
        }

        // POST : /Movie/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            MovieDataController controller = new MovieDataController();
            controller.DeleteMovie(id);
            return RedirectToAction("List");
        }

        // GET : /Movie/NewMovieRental/{id}
        [HttpGet]
        [Route("Movie/NewMovieRental/{id}")]
        public ActionResult NewMovieRental(int id)
        {
            MovieDataController controller = new MovieDataController();
            Movie selectedMovie = controller.FindMovie(id);

            return View(selectedMovie);
        }

        // POST : /Movie/CreateRental
        [HttpPost]
        public ActionResult CreateRental(string FName, string LName, int MovieId, DateTime FDate, DateTime TDate)
        {
            Rental newMovieRental = new Rental();
            newMovieRental.FName = FName;
            newMovieRental.LName = LName;
            newMovieRental.MovieId = MovieId;
            newMovieRental.FDate = FDate;
            newMovieRental.TDate = TDate;

            MovieDataController controller = new MovieDataController();
            controller.AddMovieRental(newMovieRental);

            return RedirectToAction("List");
        }
    }
}