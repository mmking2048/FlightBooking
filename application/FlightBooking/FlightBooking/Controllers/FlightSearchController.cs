using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FlightBooking.Models;
using FlightBooking.ViewModels;

namespace FlightBooking.Controllers
{
    public class FlightSearchController : Controller
    {
        // GET: FlightSearch
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include = "DepartureAirport,ArrivalAirport,DepartureDate,ReturnDate,MaximumConnections,MaximumTime,MaximumPrice")] FlightSearchViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: database search for flights
                    var booking = new Booking(1, "ab@email.com", "1111000011110000", "Coach");
                    var flight1 = new Flight(DateTime.Now, 1, DateTimeOffset.Now, DateTimeOffset.Now + TimeSpan.FromHours(3), "ORD", "MIA", 10, 10, 0, 0);
                    var flight2 = new Flight(DateTime.Now, 2, DateTimeOffset.Now + TimeSpan.FromHours(4), DateTimeOffset.Now + TimeSpan.FromHours(12), "MIA", "FCO", 10, 10, 0, 0);
                    booking.BookingFlights = new[] {flight1, flight2};
                    var connections = new[] {booking};

                    return RedirectToAction("Results", connections);
                }
            }
            catch (Exception /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(vm);
        }

        public ActionResult Results(IEnumerable<Booking> connections)
        {
            return View("Results", connections);
        }
    }
}