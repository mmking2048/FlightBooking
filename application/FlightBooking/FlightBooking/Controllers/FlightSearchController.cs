using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
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
                    var booking = new Booking();
                    var flight1 = new Flight();
                    var flight2 = new Flight();
                    flight1.FlightNumber = 111;
                    flight2.FlightNumber = 222;
                    flight1.DepartureAirport = "ORD";
                    flight1.ArrivalAirport = "MIA";
                    flight2.DepartureAirport = "MIA";
                    flight2.ArrivalAirport = "FCO";
                    flight1.DepartureTime = DateTime.Now;
                    flight1.ArrivalTime = DateTime.Now + TimeSpan.FromHours(3);
                    flight1.DepartureTime = DateTime.Now + TimeSpan.FromHours(4);
                    flight1.ArrivalTime = DateTime.Now + TimeSpan.FromHours(12);
                    
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