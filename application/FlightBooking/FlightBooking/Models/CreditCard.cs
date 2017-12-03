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
        public long AddressID { get; set; }

        public CreditCard(string type, string ccNumber, string cardFirstName, string cardLastName, DateTime expirationDate,
            string cvc, long addressID)
        {
            Type = type;
            CcNumber = ccNumber;
            CardFirstName = cardFirstName;
            CardLastName = cardLastName;
            ExpirationDate = expirationDate;
            Cvc = cvc;
            AddressID = addressID;
        }
    }
}