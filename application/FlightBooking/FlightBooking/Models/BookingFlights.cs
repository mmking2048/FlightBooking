using System;

namespace FlightBooking.Models
{
    // TODO: ideally reduce this to a list of flights
    // on the bookings object
    public class BookingFlights
    {
        public int BookingID { get; set; }
        public DateTime Date { get; set; }
        public int FlightNumber { get; set; }
        public string Airline { get; set; }
        public string Class { get; set; }
    }
}