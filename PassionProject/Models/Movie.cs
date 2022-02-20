using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Movie
    {

        //What describes a movie?
        public int Id { get; set; }

        public string Poster { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }

        public DateTime DOR { get; set; }

        public decimal Cost { get; set; }

        public List<Rental> Rentals { get; set; }
    }
}