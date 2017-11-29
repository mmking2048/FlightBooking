using System.Collections.Generic;

namespace FlightBooking.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IataID { get; set; }
        public IEnumerable<Address> LivesAt { get; set; }
        public IEnumerable<CreditCard> OwnsCreditCards { get; set; }
    }
}