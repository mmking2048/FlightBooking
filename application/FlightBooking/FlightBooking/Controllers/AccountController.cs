using System.Web.Mvc;
using FlightBooking.Models;
using FlightBooking.ViewModels;

namespace FlightBooking.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        public ActionResult Index([Bind(Include = "IataID")] Customer customer)
        {
            // TODO: database save iata ID
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public ActionResult LoginConfirmed([Bind(Include = "Email,FirstName,LastName")] LoginViewModel vm)
        {
            // TODO: database search for login
            // TODO: create new customer if necessary
            // TODO: set a cookie for login stuff

            // TODO: database delete
            return RedirectToAction("Index");
        }
    }
}