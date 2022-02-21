using PassionProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PassionProject.Controllers
{
    public class RentalDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private BlockbusterDbContext Blockbuster = new BlockbusterDbContext();

        /// <summary>
        /// Finds a rental entry from the MySQL Database through an id. | Non-Deterministic.
        /// </summary>
        /// <param name="id">The Rental Entry ID</param>
        /// <returns>Rental object containing information about the rental with a matching Entry ID. Empty Rental Object if the Entry ID does not match any rental entries in the system.</returns>
        /// <example>api/RentalData/GetRentalById/5 -> {Rental Object}</example>
        public Rental GetRentalById(int id)
        {
            return Blockbuster.Rentals.SingleOrDefault(r => r.Id == id);
        }

        public void UpdateRental(int id, [FromBody] Rental rentalInfo)
        {
            var rental = Blockbuster.Rentals.SingleOrDefault(r => r.Id == id);
            if (rental != null)
            {
                rental.FName = rentalInfo.FName;
                rental.LName = rentalInfo.LName;
                rental.FDate = rentalInfo.FDate;
                rental.TDate = rentalInfo.TDate;

                Blockbuster.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a rental entry from the database if the entry ID of that rental exists. | Non-Deterministic.
        /// </summary>
        /// <param name="id">The ID of the movie.</param>
        /// <example> POST : /api/MovieData/DeleteMovie/5</example>
        [HttpDelete]
        public void DeleteRental(int id)
        {
            var rental = Blockbuster.Rentals.SingleOrDefault(r => r.Id == id);
            if (rental != null)
            {
                Blockbuster.Rentals.Remove(rental);
                Blockbuster.SaveChanges();
            }
        }
    }
}
