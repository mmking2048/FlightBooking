using System;
using System.Collections.Generic;
using System.Linq;
using FlightBooking.Models;

namespace FlightBooking.ViewModels
{
    public class FlightConnectionViewModel
    {
        public FlightConnectionViewModel(Booking booking)
        {
            var flights = booking.BookingFlights.ToArray();
            DepartureTime = flights.Min(f => f.DepartureTime);
            ArrivalTime = flights.Min(f => f.ArrivalTime);
            TotalLength = ArrivalTime - DepartureTime;
            Flights = booking.BookingFlights;

            if (flights.All(f => f.BookedCoach != f.MaxCoach))
                CoachPrice = flights.Sum(f => f.Prices.First(p => p.Class == "Coach").Cost);

            if (flights.All(f => f.BookedFirstClass != f.MaxFirstClass))
                FirstClassPrice = flights.Sum(f => f.Prices.First(p => p.Class == "First Class").Cost);
        }

        public decimal? CoachPrice { get; }
        public decimal? FirstClassPrice { get; }
        public TimeSpan TotalLength { get; }
        public DateTime DepartureTime { get; }
        public DateTime ArrivalTime { get; }
        public IEnumerable<Flight> Flights { get; }
    }
}