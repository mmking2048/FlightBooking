using System.Collections.Generic;

namespace FlightBooking.Models
{
    public class Booking
    {
        public long BookingID { get; set; }
        public string Email { get; set; }
        public string CcNumber { get; set; }
        public string FlightClass { get; set; }
        public IEnumerable<Flight> BookingFlights { get; set; }

        public Booking(long bookingID, string email, string ccNumber, string flightClass)
        {
            BookingID = bookingID;
            Email = email;
            CcNumber = ccNumber;
            FlightClass = flightClass;
            BookingFlights = new List<Flight>();
        }
    }
}