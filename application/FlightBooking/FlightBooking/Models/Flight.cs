using System;

namespace FlightBooking.Models
{
    public class Flight
    {
        public DateTime Date { get; set; }
        public int FlightNumber { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public DateTimeOffset ArrivalTime { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public int MaxCoach { get; set; }
        public int MaxFirstClass { get; set; }
        public int BookedCoach { get; set; }
        public int BookedFirstClass { get; set; }

        public Flight(DateTime date, int flightNumber, DateTimeOffset departureTime,
        DateTimeOffset arrivalTime, string departureAirport, string arrivalAirport,
        int maxCoach, int maxFirstClass, int bookedCoach, int bookedFirstClass)
        {
            Date = date;
            FlightNumber = flightNumber;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            MaxCoach = maxCoach;
            MaxFirstClass = maxFirstClass;
            BookedCoach = bookedCoach;
            BookedFirstClass = bookedFirstClass;
        }
    }
}