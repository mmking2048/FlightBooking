using System.Web.Mvc;

namespace FlightBooking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var client = new SqlClient(new SqlParser());
            var customers = client.GetCustomers("ab@email.com", "A", "B");
            return View();
        }
    }
}