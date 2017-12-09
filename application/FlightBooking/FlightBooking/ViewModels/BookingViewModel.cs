using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FlightBooking.Models;

namespace FlightBooking.ViewModels
{
    public class BookingViewModel
    {
        public IEnumerable<Flight> Flights;
        public CreditCard CreditCard { get; set; }
    }
}