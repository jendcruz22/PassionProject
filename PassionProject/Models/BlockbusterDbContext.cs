using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace PassionProject.Models
{
    public class BlockbusterDbContext: DbContext
    {

        // These are readonly "secret" properties. 
        // Only the BlockbusterDbContext class can use them.
        // Change these to match your own local database!
        public BlockbusterDbContext(): base("DefaultConnection"){ }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rental> Rentals { get; set; }
    }
}