using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FlightBooking.Models;

namespace FlightBooking.ViewModels
{
    public class FlightConnectionViewModel
    {
        public FlightConnectionViewModel(IEnumerable<Flight> flights)
        {
            flights = flights.ToArray();
            DepartureTime = flights.Min(f => f.DepartureTime);
            ArrivalTime = flights.Max(f => f.ArrivalTime);
            TotalLength = ArrivalTime - DepartureTime;
            Flights = flights;

            if (flights.All(f => f.BookedCoach != f.MaxCoach))
                CoachPrice = flights.Sum(f => f.Prices.First(p => p.FlightClass == "Coach").Cost);

            if (flights.All(f => f.BookedFirstClass != f.MaxFirstClass))
                FirstClassPrice = flights.Sum(f => f.Prices.First(p => p.FlightClass == "First Class").Cost);
        }

        [DisplayName("Coach")]
        public decimal? CoachPrice { get; }
        [DisplayName("First Class")]
        public decimal? FirstClassPrice { get; }
        [DisplayName("Length")]
        public TimeSpan TotalLength { get; }
        [DisplayName("Departure")]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTimeOffset DepartureTime { get; }
        [DisplayName("Arrival")]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTimeOffset ArrivalTime { get; }
        public IEnumerable<Flight> Flights { get; }
    }
}