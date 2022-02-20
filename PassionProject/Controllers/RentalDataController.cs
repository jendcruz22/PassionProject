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
