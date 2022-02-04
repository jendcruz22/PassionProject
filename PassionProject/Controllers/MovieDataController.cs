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

        [HttpGet]
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
 
        [HttpGet]
        [Route("api/moviedata/findmovie/{id}")]
        public Movie FindMovie(int id)
        {
            return Blockbuster.Movies.Include(m => m.Rentals).SingleOrDefault(m => m.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example> POST : /api/MovieData/DeleteMovie/3</example>
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

        [HttpPost]
        public void AddMovie(Movie NewMovie)
        {
            Blockbuster.Movies.Add(NewMovie);
            Blockbuster.SaveChanges();
        }

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

