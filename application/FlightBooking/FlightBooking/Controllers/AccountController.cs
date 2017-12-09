using System.Linq;
using System.Web.Mvc;
using FlightBooking.Models;
using FlightBooking.ViewModels;

namespace FlightBooking.Controllers
{
    public class AccountController : Controller
    {
        private static readonly SqlParser Parser = new SqlParser();
        private static readonly SqlClient Client = new SqlClient(Parser);

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
            if (!string.IsNullOrWhiteSpace(CurrentUser.Email))
            {
                return RedirectToAction("Index");
            }

            var customers = Client.GetCustomer(vm.Email);
            if (!customers.Any())
            {
                // TODO: insert new customer
                CurrentUser.Email = vm.Email;
            }
            else
            {
                // log in
                if (Client.GetCustomer(vm.Email, vm.FirstName, vm.LastName).Any())
                {
                    CurrentUser.Email = vm.Email;
                }
            }

            return RedirectToAction("Index");
        }
    }
}