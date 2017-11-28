using System;

namespace FlightBooking.Models
{
    public class Price
    {
        public string Class { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public int FlightNumber { get; set; }
        public string Airline { get; set; }
    }
}