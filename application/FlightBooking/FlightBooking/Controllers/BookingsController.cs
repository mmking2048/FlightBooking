using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FlightBooking.Controllers
{
    public class BookingsController : Controller
    {
        private static readonly SqlParser Parser = new SqlParser();
        private static readonly SqlClient Client = new SqlClient(Parser);

        public ActionResult Index()
        {
            var email = CurrentUser.Email;
            if (string.IsNullOrWhiteSpace(email))
                return RedirectToAction("Login", "Account");

            var bookings = Client.GetBooking(email);
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