using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}