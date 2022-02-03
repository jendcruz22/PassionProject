using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //This Controller Will access the movies table of our blockbuster database.
        /// <summary>
        /// Returns a list of Movies in the system
        /// </summary>
        /// <example>GET api/MovieData/ListMovies</example>
        /// <returns>
        /// A list of movies (movie names and genres)
        /// </returns>
        [HttpGet]
        public IEnumerable<Movie> ListMovies(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blockbuster.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from movies where lower(m_name) like lower(@key) or lower(m_genre) like lower(@key) or lower(concat(m_name, ' ', m_genre)) like lower(@key)";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Movie Names
            List<Movie> Movies = new List<Movie> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                //Access Column information by the DB column name as an index
                int M_ID = (int)ResultSet["m_id"];
                string M_Name = ResultSet["m_name"].ToString();
                string M_Genre = ResultSet["m_genre"].ToString();
                string M_Description = ResultSet["m_description"].ToString();

                Movie NewMovie = new Movie();
                NewMovie.M_ID = M_ID;
                NewMovie.M_Name = M_Name;
                NewMovie.M_Genre = M_Genre;
                NewMovie.M_Description = M_Description;

                //Add the Movie Name to the List
                Movies.Add(NewMovie);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of movie names
            return Movies;
        }

        /// <summary>
        /// Finds an movie based on the movie ID
        /// </summary>
        /// <example> GET api/moviedata/findmovie/{id}</example>
        /// <param name="id">The ID of the movie</param>
        /// <returns>the name of the movie</returns>
        [HttpGet]
        [Route("api/moviedata/findmovie/{id}")]
        public Movie FindMovie(int id)
        {
            //when we want to contact the database, use a query

            Movie NewMovie = new Movie();

            //accessing the database through connection string
            MySqlConnection Conn = Blockbuster.AccessDatabase();

            //open the connection to the db
            Conn.Open();

            //creating a new mysql command query
            MySqlCommand Cmd = Conn.CreateCommand();

            //setting the command query to the string we generated in query variable
            Cmd.CommandText = "Select * from movies where m_id = @id";
            Cmd.Parameters.AddWithValue("@id", id);
            Cmd.Prepare();

            //read through the results for our query
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            //iterating through our results -- even if there is one one
            while (ResultSet.Read())
            {
                int M_ID = (int)ResultSet["m_id"];
                string M_Name = ResultSet["m_name"].ToString();
                string M_Genre = ResultSet["m_genre"].ToString();
                string M_Description = ResultSet["m_description"].ToString();
                // DateTime M_DOR = (DateTime)ResultSet["m_dor"];
                decimal M_Cost = (decimal)ResultSet["m_cost"];

                NewMovie.M_ID = M_ID;
                NewMovie.M_Name = M_Name;
                NewMovie.M_Genre = M_Genre;
                NewMovie.M_Description = M_Description;
                // NewMovie.M_DOR = M_DOR;
                NewMovie.M_Cost = M_Cost;

            }

            return NewMovie;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example> POST : /api/MovieData/DeleteMovie/3</example>
        [HttpPost]
        public void DeleteMovie(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blockbuster.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Delete from movies where m_id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        [HttpPost]
        public void AddMovie(Movie NewMovie)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blockbuster.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "insert into movies (m_name, m_genre, m_description, m_dor, m_cost) values (@M_Name, @M_Genre, @M_Description, @M_DOR, @M_Cost)";
            cmd.Parameters.AddWithValue("@M_Name", NewMovie.M_Name);
            cmd.Parameters.AddWithValue("@M_Genre", NewMovie.M_Genre);
            cmd.Parameters.AddWithValue("@M_Description", NewMovie.M_Description);
            cmd.Parameters.AddWithValue("@M_DOR", NewMovie.M_DOR);
            cmd.Parameters.AddWithValue("@M_Cost", NewMovie.M_Cost);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }

        public void UpdateMovie(int id, [FromBody] Movie MovieInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blockbuster.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "update movies set m_name=@M_Name, m_genre=@M_Genre, m_description=@M_Description, m_dor=@M_DOR, m_cost=@M_Cost where m_id=@M_ID";
            cmd.Parameters.AddWithValue("@M_Name", MovieInfo.M_Name);
            cmd.Parameters.AddWithValue("@M_Genre", MovieInfo.M_Genre);
            cmd.Parameters.AddWithValue("@M_Description", MovieInfo.M_Description);
            cmd.Parameters.AddWithValue("@M_DOR", MovieInfo.M_DOR);
            cmd.Parameters.AddWithValue("@M_Cost", MovieInfo.M_Cost);
            cmd.Parameters.AddWithValue("@M_ID", id);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}

