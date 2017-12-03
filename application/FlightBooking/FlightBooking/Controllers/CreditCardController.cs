using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlightBooking.Models;

namespace FlightBooking.Controllers
{
    public class CreditCardController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            var creditCards = new[] {new CreditCard("Visa", "1111000011110000", "First", "Last", DateTime.Now, "111", 1)};
            return PartialView("Index", creditCards);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Type,CcNumber,CardFirstName,CardLastName,ExpirationDate,Cvc,AddressID")] CreditCard creditCard)
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

            return View(creditCard);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // TODO: database search for credit card
            var creditCard = new CreditCard("Visa", "1111000011110000", "First", "Last", DateTime.Now, "111", 1);

            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // TODO: database search for credit card
            var creditCard = new CreditCard("Visa", "1111000011110000", "First", "Last", DateTime.Now, "111", 1);
            if (TryUpdateModel(creditCard, "", new[] { "Type", "CcNumber", "CardFirstName", "CardLastName", "ExpirationDate", "Cvc", "AddressID" }))
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

            return View(creditCard);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // TODO: database search for credit card
            var creditCard = new CreditCard("Visa", "1111000011110000", "First", "Last", DateTime.Now, "111", 1);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // TODO: database search for credit card
            var creditCard = new CreditCard("Visa", "1111000011110000", "First", "Last", DateTime.Now, "111", 1);
            // TODO: database delete
            return RedirectToAction("Index", "Account");
        }
    }
}