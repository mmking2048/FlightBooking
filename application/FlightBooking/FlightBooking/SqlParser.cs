using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightBooking.Models;
using Npgsql;

namespace FlightBooking
{
    public class SqlParser
    {
        public IEnumerable<Customer> ParseCustomer(NpgsqlDataReader reader)
        {
            var customers = new List<Customer>();
            var firstNameColumn = reader.GetOrdinal("firstname");
            var lastNameColumn = reader.GetOrdinal("lastname");
            var emailColumn = reader.GetOrdinal("email");
            var iataIDColumn = reader.GetOrdinal("iata_id");

            while (reader.Read())
            {
                var firstName = reader.GetString(firstNameColumn);
                var lastName = reader.GetString(lastNameColumn);
                var email = reader.GetString(emailColumn);
                var iataID = reader.GetString(iataIDColumn);

                customers.Add(new Customer(firstName, lastName, email, iataID));
            }

            return customers;
        }

        public IEnumerable<Flight> ParseFlight(NpgsqlDataReader reader)
        {
            var flights = new List<Flight>();
            var dateColumn = reader.GetOrdinal("date");
            var flightNumberColumn = reader.GetOrdinal("flightnumber");
            var departureTimeColumn = reader.GetOrdinal("departuretime");
            var arrivalTimeColumn = reader.GetOrdinal("arrivaltime");
            var departureAirportColumn = reader.GetOrdinal("departureairport");
            var arrivalAirportColumn = reader.GetOrdinal("arrivalairport");
            var maxCoachColumn = reader.GetOrdinal("maxcoach");
            var maxFirstClassColumn = reader.GetOrdinal("maxfirstclass");
            var bookedCoachColumn = reader.GetOrdinal("bookedcoach");
            var bookedFirstClassColumn = reader.GetOrdinal("bookedfirstclass");

            while(reader.Read())
            {
                var date = reader.GetString(dateColumn);
                var flightNumber = reader.GetString(flightNumberColumn);
                var departureTime = reader.GetString(departureTimeColumn);
                var arrivalTime = reader.GetString(arrivalTimeColumn);
                var departureAirport = reader.GetString(departureAirportColumn);
                var arrivalAirport = reader.GetString(arrivalAirportColumn);
                var maxCoach = reader.getString(maxCoachColumn);
                var maxFirstClass = reader.getString(maxFirstClassColumn);
                var bookedCoach = reader.getString(bookedCoachColumn);
                var bookedFirstClass = reader.getString(bookedFirstClassColumn);

                flights.Add(new Flight(date, flightNumber, departureTime, arrivalTime, departureAirport, arrivalAirport, maxCoach, maxFirstClass, bookedCoach, bookedFirstClass));
            }

            return flights;
        }
    }
}