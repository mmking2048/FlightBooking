using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FlightBooking.Models;

namespace FlightBooking.Controllers
{
    public class BookingsController : Controller
    {
        private static readonly SqlParser Parser = new SqlParser();
        private static readonly SqlClient Client = new SqlClient(Parser);

        public ActionResult Index()
        {
            // TODO: database search for bookings
            var booking = new Booking(2043, "ab@email.com", "1111000011110000", "Coach");
            booking.BookingFlights = Client.GetBookingFlights(booking.BookingID);
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
            
            var bookings = Client.GetBooking(id.Value).ToArray();
            var booking = bookings.Length != 0 ? bookings.First() : null;
            if (booking == null)
            {
                return HttpNotFound();
            }
            booking.BookingFlights = Client.GetBookingFlights(id.Value);

            return View(booking.BookingFlights);
        }

        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelConfirmed(int id)
        {
            Client.DeleteBooking(id);
            return RedirectToAction("Index");
        }
    }
}