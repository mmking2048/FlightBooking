using System;

namespace FlightBooking.Models
{
    public class CreditCard
    {
        public string Type { get; set; }
        public string CcNumber { get; set; }
        public string CardFirstName { get; set; }
        public string CardLastName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Cvc { get; set; }
        public int AddressID { get; set; }
    }
}