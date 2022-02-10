using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class MovieDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private BlockbusterDbContext Blockbuster = new BlockbusterDbContext();


        // This Controller Will access the movies table of our blockbuster database. | Non-Deterministic.
        /// <summary>
        /// Returns a list of movies in the blockbuster database
        /// </summary>
        /// <returns>
        /// A list of Movie Objects with fields mapped to the database column values (movie name, movie genre, date of release, description, cost of renting).
        /// </returns>
        /// <example>GET api/MovieData/ListAuthors -> {Author Object, Author Object, Author Object...}</example>
        [HttpGet]
        [Route("api/moviedata/listmovies/{SearchKey?}")]
        public IEnumerable<Movie> ListMovies(string SearchKey = null)
        {
            if (SearchKey == null)
            {
                return Blockbuster.Movies;
            }
            else
            {
                return Blockbuster.Movies.Where(m => m.Name.Contains(SearchKey));
            }
        }

        /// <summary>
        /// Finds an movie from the MySQL Database through an id. | Non-Deterministic.
        /// </summary>
        /// <param name="id">The Movie ID</param>
        /// <returns>Movie object containing information about the movie with a matching ID. Empty Movie Object if the ID does not match any movies in the system.</returns>
        /// <example>api/MovieData/FindMovie/5 -> {Author Object}</example>
        [HttpGet]
        [Route("api/moviedata/findmovie/{id}")]
        public Movie FindMovie(int id)
        {
            return Blockbuster.Movies.Include(m => m.Rentals).SingleOrDefault(m => m.Id == id);
        }

        /// <summary>
        /// Deletes a movie from the connected MySql Database if the ID of that movie exists. | Non-Deterministic.
        /// </summary>
        /// <param name="id">The ID of the movie.</param>
        /// <example> POST : /api/MovieData/DeleteMovie/5</example>
        [HttpPost]
        public void DeleteMovie(int id)
        {
            var movie = Blockbuster.Movies.SingleOrDefault(m => m.Id == id);
            if (movie != null)
            {
                Blockbuster.Movies.Remove(movie);
                Blockbuster.SaveChanges();
            }
        }

        /// <summary>
        /// Adds a Movie to the MySQL Database.
        /// </summary>
        /// <param name="NewMovie">An object with fields that map to the columns of the movie's table. | Non-Deterministic.</param>
        /// <example>
        /// POST api/MovieData/AddMovie
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        /// "M_Name":"Pulp Fiction",
        /// "M_Genre":"Crime, Drama",
        /// "M_Description":"The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
        /// "M_DOR":"1994-10-14",
        /// "M_Cost":"20.23"
        /// }
        /// </example>
        [HttpPost]
        public void AddMovie(Movie NewMovie)
        {
            Blockbuster.Movies.Add(NewMovie);
            Blockbuster.SaveChanges();
        }

        /// <summary>
        /// Adds a Movie Rental to the MySQL Database.
        /// </summary>
        /// <param name="NewMovieRental">An object with fields that map to the columns of the rental's table. | Non-Deterministic.</param>
        /// <example>
        /// POST api/MovieData/AddMovieRental
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        /// "MovieId":"5",
        /// "FName":"Jenny",
        /// "LName":"Dcruz",
        /// "FDate":"2022-01-20",
        /// "TDate":"202-01-31"
        /// 
        /// }
        /// </example>
        [HttpPost]
        public void AddMovieRental(Rental NewMovieRental)
        {
            Blockbuster.Rentals.Add(NewMovieRental);
            Blockbuster.SaveChanges();
        }

        /// <summary>
        /// Updates an Movie on the MySQL Database. | Non-Deterministic.
        /// </summary>
        /// <param name="MovieInfo">An object with fields that map to the columns of the Movie's table.</param>
        /// <example>
        /// POST api/MovieData/UpdateMovie/208 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///     "Name":"Pulp Fiction",
        ///     "Genre":"Crime, Drama",
        ///     "Description":"The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
        ///     "DOR":"1994-10-14",
        ///     "Cost":"20.23"
        /// }
        /// </example>
        public void UpdateMovie(int id, [FromBody] Movie MovieInfo)
        {
            var movie = Blockbuster.Movies.SingleOrDefault(m => m.Id == id);
            if (movie != null)
            {
                movie.Name = MovieInfo.Name;
                movie.Genre = MovieInfo.Genre;
                movie.Description = MovieInfo.Description;
                movie.DOR = MovieInfo.DOR;
                movie.Cost = MovieInfo.Cost;

                Blockbuster.SaveChanges();
            }
        }
    }
}

