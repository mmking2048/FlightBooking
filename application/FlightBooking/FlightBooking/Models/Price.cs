using System;

namespace FlightBooking.Models
{
    public class Price
    {
        public string FlightClass { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public int FlightNumber { get; set; }
        public string Airline { get; set; }

        public Price(string flightClass, decimal cost, DateTime date, int flightNumber, string airline)
        {
            FlightClass = flightClass;
            Cost = cost;
            Date = date;
            FlightNumber = flightNumber;
            Airline = airline;
        }
    }
}