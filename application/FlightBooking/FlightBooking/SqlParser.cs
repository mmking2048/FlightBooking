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
                var date = reader.GetDateTime(dateColumn);
                var flightNumber = reader.GetInt16(flightNumberColumn);
                var departureTime = reader.GetDateTime(departureTimeColumn);
                var arrivalTime = reader.GetDateTime(arrivalTimeColumn);
                var departureAirport = reader.GetString(departureAirportColumn);
                var arrivalAirport = reader.GetString(arrivalAirportColumn);
                var maxCoach = reader.GetInt16(maxCoachColumn);
                var maxFirstClass = reader.GetInt16(maxFirstClassColumn);
                var bookedCoach = reader.GetInt16(bookedCoachColumn);
                var bookedFirstClass = reader.GetInt16(bookedFirstClassColumn);

                flights.Add(new Flight(date, flightNumber, departureTime, arrivalTime, departureAirport,
                    arrivalAirport, maxCoach, maxFirstClass, bookedCoach, bookedFirstClass));
            }

            return flights;
        }

        public IEnumerable<Address> ParseAddress(NpgsqlDataReader reader)
        {
            var addresses = new List<Address>();
            var streetNumberColumn = reader.GetOrdinal("streetnumber");
            var streetNameColumn = reader.GetOrdinal("streetname");
            var cityColumn = reader.GetOrdinal("city");
            var zipCodeColumn = reader.GetOrdinal("zipcode");
            var countryColumn = reader.GetOrdinal("country");
            var addressIDColumn = reader.GetOrdinal("addressid");

            while (reader.Read())
            {
                var streetNumber = reader.GetInt16(streetNumberColumn);
                var streetName = reader.GetString(streetNameColumn);
                var city = reader.GetString(cityColumn);
                var zipCode = reader.GetString(zipCodeColumn);
                var country = reader.GetString(countryColumn);
                var addressID = reader.GetInt64(addressIDColumn);

                addresses.Add(new Address(streetNumber, streetName, city, zipCode, country, addressID));
            }

            return addresses;
        }

        public IEnumerable<Airline> ParseAirline(NpgsqlDataReader reader)
        {
            var airlines = new List<Airline>();
            var airlineIDColumn = reader.GetOrdinal("airlineid");
            var countryColumn = reader.GetOrdinal("country");
            var airlineNameColumn = reader.GetOrdinal("airlinename");

            while (reader.Read())
            {
                var airlineID = reader.GetString(airlineIDColumn);
                var country = reader.GetString(countryColumn);
                var airlineName = reader.GetString(airlineNameColumn);

                airlines.Add(new Airline(airlineID, country, airlineName));
            }

            return airlines;
        }

        public IEnumerable<Airport> ParseAirport(NpgsqlDataReader reader)
        {
            var airports = new List<Airport>();
            var iataIDColumn = reader.GetOrdinal("iataid");
            var countryColumn = reader.GetOrdinal("country");
            var stateColumn = reader.GetOrdinal("state");

            while (reader.Read())
            {
                var iataID = reader.GetString(iataIDColumn);
                var country = reader.GetString(countryColumn);
                var state = reader.GetString(stateColumn);

                airports.Add(new Airport(iataID, country, state));
            }

            return airports;
        }

        public IEnumerable<CreditCard> ParseCreditCard(NpgsqlDataReader reader)
        {
            var creditCards = new List<CreditCard>();
            var typeColumn = reader.GetOrdinal("type");
            var ccNumberColumn = reader.GetOrdinal("ccnumber");
            var cardFirstNameColumn = reader.GetOrdinal("cardfirstname");
            var cardLastNameColumn = reader.GetOrdinal("cardlastname");
            var expirationDateColumn = reader.GetOrdinal("expirationdate");
            var cvcColumn = reader.GetOrdinal("cvc");
            var addressIDColumn = reader.GetOrdinal("addressid");

            while (reader.Read())
            {
                var type = reader.GetString(typeColumn);
                var ccNumber = reader.GetString(ccNumberColumn);
                var cardFirstName = reader.GetString(cardFirstNameColumn);
                var cardLastName = reader.GetString(cardLastNameColumn);
                var expirationDate = reader.GetDateTime(expirationDateColumn);
                var cvc = reader.GetString(cvcColumn);
                var addressID = reader.GetInt64(addressIDColumn);

                creditCards.Add(new CreditCard(type, ccNumber, cardFirstName, cardLastName, expirationDate, cvc, addressID));
            }

            return creditCards;
        }

        public IEnumerable<MileageProgram> ParseMileageProgram(NpgsqlDataReader reader)
        {
            var mileagePrograms = new List<MileageProgram>();
            var milesColumn = reader.GetOrdinal("miles");
            var emailColumn = reader.GetOrdinal("email");
            var airlineColumn = reader.GetOrdinal("airline");
            var bookingIDColumn = reader.GetOrdinal("bookingid");

            while (reader.Read())
            {
                var miles = reader.GetInt32(milesColumn);
                var email = reader.GetString(emailColumn);
                var airline = reader.GetString(airlineColumn);
                var bookingID = reader.GetInt64(bookingIDColumn);

                mileagePrograms.Add(new MileageProgram(miles, email, airline, bookingID));
            }
            return mileagePrograms;
        }

        public IEnumerable<Price> ParsePrice(NpgsqlDataReader reader)
        {
            var prices = new List<Price>();
            var flightClassColumn = reader.GetOrdinal("flightclass");
            var costColumn = reader.GetOrdinal("cost");
            var dateColumn = reader.GetOrdinal("date");
            var flightNumberColumn = reader.GetOrdinal("flightnumber");
            var airlineColumn = reader.GetOrdinal("airline");

            while (reader.Read())
            {
                var flightClass = reader.GetString(flightClassColumn);
                var cost = reader.GetDecimal(costColumn);
                var date = reader.GetDateTime(dateColumn);
                var flightNumber = reader.GetInt16(flightNumberColumn);
                var airline = reader.GetString(airlineColumn);

                prices.Add(new Price(flightClass, cost, date, flightNumber, airline));
            }

            return prices;
        }

        public IEnumerable<Booking> ParseBooking(NpgsqlDataReader reader)
        {
            var bookings = new List<Booking>();
            var bookingIDColumn = reader.GetOrdinal("bookingid");
            var emailColumn = reader.GetOrdinal("email");
            var ccNumberColumn = reader.GetOrdinal("ccnumber");
            var flightClassColumn = reader.GetOrdinal("flightclass");

            while (reader.Read())
            {
                var bookingID = reader.GetInt64(bookingIDColumn);
                var email = reader.GetString(emailColumn);
                var ccNumber = reader.GetString(ccNumberColumn);
                var flightClass = reader.GetString(flightClassColumn);

                bookings.Add(new Booking(bookingID, email, ccNumber, flightClass));
            }

            return bookings;
        }
    }
}