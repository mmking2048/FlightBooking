namespace FlightBooking.Models
{
    public class MileageProgram
    {
        public int Miles { get; set; }
        public string Email { get; set; }
        public string Airline { get; set; }
        public long BookingID { get; set; }

        public MileageProgram(int miles, string email, string airline, long bookingID)
        {
            Miles = miles;
            Email = email;
            Airline = airline;
            BookingID = bookingID;
        }
    }
}