using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Rental
    {
        // What describes the rentals table?
        public int Id { get; set; }
        public int MovieId { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public DateTime FDate { get; set; }

        public DateTime TDate { get; set; }

    }
}