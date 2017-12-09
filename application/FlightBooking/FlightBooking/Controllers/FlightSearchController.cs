using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FlightBooking.Models;
using FlightBooking.ViewModels;

namespace FlightBooking.Controllers
{
    public class FlightSearchController : Controller
    {
        private static readonly SqlParser Parser = new SqlParser();
        private static readonly SqlClient Client = new SqlClient(Parser);

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
                    var flight1 = new Flight(DateTime.Now, 1, DateTimeOffset.Now, DateTimeOffset.Now + TimeSpan.FromHours(3), "ORD", "MIA", 10, 10, "AA", 0, 0);
                    var flight2 = new Flight(DateTime.Now, 2, DateTimeOffset.Now + TimeSpan.FromHours(4), DateTimeOffset.Now + TimeSpan.FromHours(12), "MIA", "FCO", 10, 10, "BA", 0, 0);
                    flight1.Prices = new[]
                    {
                        new Price("Coach", 110.00M, DateTime.Now, 1, "AA"),
                        new Price("First Class", 200M, DateTime.Now, 2, "BA")
                    };
                    flight2.Prices = new[]
                    {
                        new Price("Coach", 110.00M, DateTime.Now, 3, "AA"),
                        new Price("First Class", 200M, DateTime.Now, 4, "BA")
                    };
                    booking.BookingFlights = new[] {flight1, flight2};
                    var connections = new[] {booking};

                    var bookings = connections.Select(c => new FlightConnectionViewModel(c));
                    return View("Results", bookings);
                }
            }
            catch (Exception /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(vm);
        }
    }
}