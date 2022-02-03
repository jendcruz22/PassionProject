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
            //this class will help us gather information from the db
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

        // POST : /Movie/Create
        [HttpPost]
        public ActionResult Create(string M_Name, string M_Genre, string M_Description, decimal M_Cost)
        {
            //Identify this method is running 
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(M_Name);

            Movie NewMovie = new Movie();
            NewMovie.M_Name = M_Name;
            NewMovie.M_Genre = M_Genre;
            NewMovie.M_Description = M_Description;
            NewMovie.M_Cost = M_Cost;

            MovieDataController controller = new MovieDataController();
            controller.AddMovie(NewMovie);

            return RedirectToAction("List");
        }

        /// <summary>
        /// Routes a dynamically generated "Movie Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A dynamic "Update Movie" webpage which proves the current information of the movie and asks the user for new information as part of a form</returns>
        // <example>GET : /Movie/Update/{id}</example>
        public ActionResult Update(int id)
        {
            MovieDataController controller = new MovieDataController();
            Movie SelectedMovie = controller.FindMovie(id);
            return View(SelectedMovie);
        }


        /// <summary>
        /// Receive a POST request containing information about an existing movie in the system, with new values. Conveys this information to the API, and redirects to the "Movie Show" page of our updated movie.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="M_Name"></param>
        /// <param name="M_Genre"></param>
        /// <param name="M_Description"></param>
        /// <param name="M_Cost"></param>
        /// <returns>A dynamic webpage which provides the current information of the movie.</returns>
        /// <example>
        // POST : /Movie/Update/{id}
        /// FORM DATTA / POST DATA / REQUEST BODY
        /// {
        /// "M_Name":"Jenny",
        /// "M_Genre":"Dcruz",
        /// "M_Description":"N01469587",
        /// "M_Cost":"3434.23"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string M_Name, string M_Genre, string M_Description, decimal M_Cost)
        {
            Movie MovieInfo = new Movie();
            MovieInfo.M_Name = M_Name;
            MovieInfo.M_Genre = M_Genre;
            MovieInfo.M_Description = M_Description;
            MovieInfo.M_Cost = M_Cost;

            MovieDataController controller = new MovieDataController();
            controller.UpdateMovie(id, MovieInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}