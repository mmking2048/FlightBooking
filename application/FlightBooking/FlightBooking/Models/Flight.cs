using System;

namespace FlightBooking.Models
{
    public class Flight
    {
        public DateTime Date { get; set; }
        public int FlightNumber { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public int MaxCoach { get; set; }
        public int MaxFirstClass { get; set; }
        public int BookedCoach { get; set; }
        public int BookedFirstClass { get; set; }
    }
}