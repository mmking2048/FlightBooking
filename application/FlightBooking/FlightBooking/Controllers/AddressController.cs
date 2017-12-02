using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlightBooking.Models;

namespace FlightBooking.Controllers
{
    public class AddressController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            // TODO: fetch list of addresses
            var addresses = new[] {new Address(111, "Street", "Chicago", "IL", "60600", "United States", 1)};
            return PartialView("Index", addresses);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StreetNumber,StreetName,City,ZipCode,Country")] Address address)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: database insert
                    return RedirectToAction("Index", "Account");
                }
            }
            catch (Exception /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            
            return View(address);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // TODO: database search for address
            var address = new Address(111, "Street", "Chicago", "IL", "60600", "United States", 1);

            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // TODO: database search for address
            var address = new Address(111, "Street", "Chicago", "IL", "60600", "United States", 1);
            if (TryUpdateModel(address, "", new[]{"StreetNumber", "StreetName", "City", "ZipCode", "Country"}))
            {
                try
                {
                    // TODO: save changes to database

                    return RedirectToAction("Index", "Account");
                }
                catch (Exception /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(address);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // TODO: database search for address
            var address = new Address(111, "Street", "Chicago", "IL", "60600", "United States", 1);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // TODO: database search for address
            var address = new Address(111, "Street", "Chicago", "IL", "60600", "United States", 1);
            // TODO: database delete
            return RedirectToAction("Index", "Account");
        }
    }
}