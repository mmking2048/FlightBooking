using System.Web.Mvc;
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

            // TODO: database delete
            return RedirectToAction("Index");
        }
    }
}