using System;
using System.Net;
using System.Web.Mvc;
using FlightBooking.Models;

namespace FlightBooking.Controllers
{
    public class BookingsController : Controller
    {

        public ActionResult Index()
        {
            // TODO: database search for bookings
            var booking = new Booking(1, "ab@email.com", "1111000011110000", "Coach");
            var flight = new Flight(DateTime.Now, 1, DateTimeOffset.Now, DateTimeOffset.Now + TimeSpan.FromHours(3),
                "ORD", "MIA", 10, 10, "AA", 0, 0)
            {
                Prices = new[]
                {
                    new Price("Coach", 11.00M, DateTime.Now, 1, "AA"),
                    new Price("First Class", 11.00M, DateTime.Now, 1, "AA")
                }
            };
            booking.BookingFlights = new[] {flight};
            var bookings = new[] {booking};

            return View("Index", bookings);
        }
        
        public ActionResult Book()
        {
            return View();
        }

        public ActionResult Cancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // TODO: database search for booking
            var booking = new Booking(1, "ab@email.com", "1111000011110000", "First Class", new[]
            {
                new Flight(DateTime.Now, 1, DateTimeOffset.Now, DateTimeOffset.Now, "ORD", "MIA", 10, 10, "AA", 0, 0)
            });
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking.BookingFlights);
        }

        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelConfirmed(int id)
        {
            // TODO: database search for booking
            
            // TODO: database delete
            return RedirectToAction("Index");
        }
    }
}