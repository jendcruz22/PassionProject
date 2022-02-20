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
        // GET: Rental
        public ActionResult Edit(int id)
        {
            var controller = new RentalDataController();
            Rental selectedRental = controller.GetRentalById(id);
            return View(selectedRental);
        }

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