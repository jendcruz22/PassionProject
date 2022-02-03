using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace PassionProject.Models
{
    public class BlockbusterDbContext
    {
        //These are readonly "secret" properties. 
        //Only the BlockbusterDbContext class can use them.
        //Change these to match your own local blockbuster database!
        private static string User { get { return "root"; } }
        private static string Password { get { return ""; } }
        private static string Database { get { return "blockbuster"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //ConnectionString is a series of credentials used to connect to the database.
        protected static string ConnectionString
        {
            get
            {
                //convert zero datetime is a db connection setting which returns NULL if the date is 0000-00-00
                //this can allow C# to have an easier interpretation of the date (no date instead of 0 BCE)

                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }
        //This is the method we actually use to get the database!
        /// <summary>
        /// Returns a connection to the blockbuster database.
        /// </summary>
        /// <example>
        /// private BlockbusterDbContext Blockbuster = new BlockbusterDbContext();
        /// MySqlConnection Conn = Blockbuster.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase()
        {
            //We are instantiating the MySqlConnection Class to create an object
            //the object is a specific connection to our blockbuster database on port 3306 of localhost
            return new MySqlConnection(ConnectionString);
        }
    }
}