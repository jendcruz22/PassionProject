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
        public BlockbusterDbContext(): base("DefaultConnection"){ }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rental> Rentals { get; set; }
    }
}