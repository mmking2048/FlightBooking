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
                CoachPrice = flights.Sum(f => f.Prices.First(p => p.FlightClass == "Coach").Cost);

            if (flights.All(f => f.BookedFirstClass != f.MaxFirstClass))
                FirstClassPrice = flights.Sum(f => f.Prices.First(p => p.FlightClass == "First Class").Cost);
        }

        public decimal? CoachPrice { get; }
        public decimal? FirstClassPrice { get; }
        public TimeSpan TotalLength { get; }
        public DateTimeOffset DepartureTime { get; }
        public DateTimeOffset ArrivalTime { get; }
        public IEnumerable<Flight> Flights { get; }
    }
}