using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class RentalController : Controller
    {

        /// <summary>
        /// Routes a dynamically generated Rentals Update Page. Gathers information from the blockbuster database.
        /// </summary>
        /// <param name="id">Entry Id of the Rental</param>
        /// <returns>A dynamic Update Rentals webpage which provides the current information of the rental and asks the user for new information as part of a form</returns>
        // <example>GET : /Rental/Edit/5</example>
        public ActionResult Edit(int id)
        {
            var controller = new RentalDataController();
            Rental selectedRental = controller.GetRentalById(id);
            return View(selectedRental);
        }

        /// <summary>
        /// Receive a POST request containing information about an existing rental against a movie in the system, with new values. Conveys this information to the API, and redirects to the List of movies page.
        /// </summary>
        /// <param name="id">Entry id of the rental</param>
        /// <param name="fName">First name of the person renting the movie</param>
        /// <param name="lName">Last name of the person renting the movie</param>
        /// <param name="fDate">The date from when the movie is being rented</param>
        /// <param name="tDate">The date till when the movie is being rented</param>
        /// <returns>A dynamic webpage which provides the current information of the rental.</returns>
        /// <example>
        // POST : /Rental/Update/{id}
        /// FORM DATA / POST DATA / REQUEST BODY
        /// {
        /// "fname":"Chris",
        /// "lname":"Frank",
        /// "fdate":"2022-01-02",
        /// "tdate":"2022-01-12"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string fName, string lName, DateTime fDate, DateTime tDate)
        {
            Rental rentalInfo = new Rental();
            rentalInfo.FName = fName;
            rentalInfo.LName = lName;
            rentalInfo.FDate = fDate;
            rentalInfo.TDate = tDate;

            RentalDataController controller = new RentalDataController();
            controller.UpdateRental(id, rentalInfo);

            return Redirect("/Movie/List");
        }

        // GET : /Rental/DeleteConfirm/{id}
        public ActionResult DeleteRentalConfirm(int id)
        {
            RentalDataController controller = new RentalDataController();
            Rental newRental = controller.GetRentalById(id);

            return View(newRental);
        }

        // POST : /Rental/DeleteRental/{id}
        [HttpPost]
        public ActionResult DeleteRental(int id)
        {
            RentalDataController controller = new RentalDataController();
            controller.DeleteRental(id);
            return Redirect("/Movie/List");
        }
    }
}