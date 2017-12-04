using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightBooking.Controllers
{
    public class BookingsController : Controller
    {
        public ActionResult Book()
        {
            return View();
        }
    }
}