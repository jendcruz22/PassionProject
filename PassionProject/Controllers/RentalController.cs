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
            Rental SelectedRental = controller.getRentalById(id);
            return View(SelectedRental);
        }

        [HttpPost]
        public ActionResult Update(int id, string FName, string LName, DateTime FDate, DateTime TDate)
        {
            Rental RentalInfo = new Rental();
            RentalInfo.FName = FName;
            RentalInfo.LName = LName;
            RentalInfo.FDate = FDate;
            RentalInfo.TDate = TDate;

            RentalDataController controller = new RentalDataController();
            controller.UpdateRental(id, RentalInfo);

            return Redirect("/Movie/List");
        }

        // GET : /Rental/DeleteConfirm/{id}
        public ActionResult DeleteRentalConfirm(int id)
        {
            RentalDataController controller = new RentalDataController();
            Rental NewRental = controller.getRentalById(id);

            return View(NewRental);
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