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
        public ActionResult List(string SearchKey = null)
        {
            //this class will help us gather information from the blockbuster database
            MovieDataController controller = new MovieDataController();
            IEnumerable<Movie> Movies = controller.ListMovies(SearchKey);
            return View(Movies);
        }

        // GET : /Movie/Show/{id}
        [HttpGet]
        [Route("Movie/Show/{id}")]
        public ActionResult Show(int id)
        {
            MovieDataController controller = new MovieDataController();
            Movie SelectedMovie = controller.FindMovie(id);

            return View(SelectedMovie);
        }

        // GET : /Movie/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            MovieDataController controller = new MovieDataController();
            Movie NewMovie = controller.FindMovie(id);

            return View(NewMovie);
        }

        // POST : /Movie/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            MovieDataController controller = new MovieDataController();
            controller.DeleteMovie(id);
            return RedirectToAction("List");
        }

        // GET : /Movie/New
        public ActionResult New()
        {
            return View();
        }

        // GET : /Movie/NewMovieRental/{id}
        [HttpGet]
        [Route("Movie/NewMovieRental/{id}")]
        public ActionResult NewMovieRental(int id)
        {
            MovieDataController controller = new MovieDataController();
            Movie SelectedMovie = controller.FindMovie(id);

            return View(SelectedMovie);
        }

        // POST : /Movie/Create
        [HttpPost]
        public ActionResult Create(string M_Poster, string M_Name, string M_Genre, string M_Description, DateTime M_DOR, decimal M_Cost)
        {
            Movie NewMovie = new Movie();
            NewMovie.Poster = M_Poster;
            NewMovie.Name = M_Name;
            NewMovie.Genre = M_Genre;
            NewMovie.DOR = M_DOR;
            NewMovie.Description = M_Description;
            NewMovie.Cost = M_Cost;

            MovieDataController controller = new MovieDataController();
            controller.AddMovie(NewMovie);

            return RedirectToAction("List");
        }

        // POST : /Movie/CreateRental
        [HttpPost]
        public ActionResult CreateRental(string FName, string LName, int MovieId, DateTime FDate, DateTime TDate)
        {
            Rental NewMovieRental = new Rental();
            NewMovieRental.FName = FName;
            NewMovieRental.LName = LName;
            NewMovieRental.MovieId = MovieId;
            NewMovieRental.FDate = FDate;
            NewMovieRental.TDate = TDate;

            MovieDataController controller = new MovieDataController();
            controller.AddMovieRental(NewMovieRental);

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
            Movie SelectedMovie = controller.FindMovie(id);
            return View(SelectedMovie);
        }

        /// <summary>
        /// Receive a POST request containing information about an existing movie in the system, with new values. Conveys this information to the API, and redirects to the "Movie Show" page of our updated movie.
        /// </summary>
        /// <param name="id">Id of the Movie to update</param>
        /// <param name="M_Poster">The updated link to the movie poster</param>
        /// <param name="M_Name">The updated movie name of the movie</param>
        /// <param name="M_Genre">The updated genre of the movie</param>
        /// <param name="M_Description">The updated description of the movie</param>
        /// <param name="M_DOR">The updated date of release of the movie</param>
        /// <param name="M_Cost">The updated price of renting the movie</param>
        /// <returns>A dynamic webpage which provides the current information of the movie.</returns>
        /// <example>
        // POST : /Movie/Update/{id}
        /// FORM DATTA / POST DATA / REQUEST BODY
        /// {
        /// "M_Name":"Pulp Fiction",
        /// "M_Poster":"image link",
        /// "M_Genre":"Crime, Drama",
        /// "M_Description":"The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
        /// "M_DOR":"1994-10-14",
        /// "M_Cost":"20.23"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string M_Poster, string M_Name, string M_Genre, string M_Description, DateTime M_DOR, decimal M_Cost)
        {
            Movie MovieInfo = new Movie();
            MovieInfo.Poster = M_Poster;
            MovieInfo.Name = M_Name;
            MovieInfo.Genre = M_Genre;
            MovieInfo.Description = M_Description;
            MovieInfo.DOR = M_DOR;
            MovieInfo.Cost = M_Cost;

            MovieDataController controller = new MovieDataController();
            controller.UpdateMovie(id, MovieInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}