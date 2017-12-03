using System;
using System.Collections.Generic;
using FlightBooking.Models;
using Npgsql;

namespace FlightBooking
{
    public class SqlClient
    {
        private readonly string _connString;
        private readonly SqlParser _sqlParser;

        public SqlClient(SqlParser sqlParser)
        {
            _connString = string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4}",
                "cs425project.cfo2e3troldi.us-east-2.rds.amazonaws.com", "5432", "CS425project", "CS425project",
                "CS425project");
            _sqlParser = sqlParser;
        }

        #region SQL SELECTS

        public IEnumerable<Customer> GetCustomers(string email, string firstName, string lastName)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "SELECT * FROM customer WHERE email = @email AND firstname = @firstname AND lastname = @lastname";
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("firstname", firstName);
                    cmd.Parameters.AddWithValue("lastname", lastName);

                    using (var reader = cmd.ExecuteReader())
                    {
                        return _sqlParser.ParseCustomer(reader);
                    }
                }
            }
        }

        public IEnumerable<Address> GetAddress(int streetNumber, string streetName, string city, string zipCode, string country, long addressID)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM address WHERE streetnumber = @streeetnumber AND streetname = @streetname" +
                        "AND city = @city AND zipcode = @zipcode AND country = @country AND addressid = @addressid";
                    cmd.Parameters.AddWithValue("streetnumber", streetNumber);
                    cmd.Parameters.AddWithValue("streetname", streetName);
                    cmd.Parameters.AddWithValue("city", city);
                    cmd.Parameters.AddWithValue("zipcode", zipCode);
                    cmd.Parameters.AddWithValue("country", country);
                    cmd.Parameters.AddWithValue("addressid", addressID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        return _sqlParser.ParseAddress(reader);
                    }
                }
            }
        }

        public IEnumerable<Airline> GetAirline(string airlineID, string country, string airlineName)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM airline WHERE airlineid = @airlineid AND country = @country AND airlinename = @airlinename";
                    cmd.Parameters.AddWithValue("airlineid", airlineID);
                    cmd.Parameters.AddWithValue("country", country);
                    cmd.Parameters.AddWithValue("airlinename", airlineName);

                    using (var reader = cmd.ExecuteReader())
                    {
                        return _sqlParser.ParseAirline(reader);
                    }
                }
            }
        }

        public IEnumerable<Airport> GetAirport(string iataID, string airportName, string country, string state, double latitude, double longitude)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM airport WHERE iataid = @iataid AND airportname = @airportname AND country = @country AND " +
                        "state = @state AND latitude = @latitude AND longitude = @longitude";
                    cmd.Parameters.AddWithValue("iataid", iataID);
                    cmd.Parameters.AddWithValue("country", country);
                    cmd.Parameters.AddWithValue("state", state);
                    cmd.Parameters.AddWithValue("latitude", latitude);
                    cmd.Parameters.AddWithValue("longitude", longitude);

                    using (var reader = cmd.ExecuteReader())
                    {
                        return _sqlParser.ParseAirport(reader);
                    }
                }
            }
        }

        public IEnumerable<Booking> GetBooking(long bookingID, string email, string ccNumber, string flightClass)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM booking WHERE bookingid = @bookingid AND email = @email AND ccnumber = @ccnumber AND " +
                        "flightclass = @flightclass";
                    cmd.Parameters.AddWithValue("bookingid", bookingID);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("ccnumber", ccNumber);
                    cmd.Parameters.AddWithValue("flightclass", flightClass);

                    using (var reader = cmd.ExecuteReader())
                    {
                        return _sqlParser.ParseBooking(reader);
                    }
                }
            }
        }

        public IEnumerable<CreditCard> GetCreditCard(string type, string ccNumber, string cardFirstName, string cardLastName, 
            DateTime expirationDate, string cvc, long addressID)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM creditcard WHERE type = @type AND ccnumber = @ccnumber AND cardfirstname = @cardfirstname" +
                        "AND cardlastname = @cardlastname AND expirationdate = @expirationdate AND cvc = @cvc AND addressid = @addressid";
                    cmd.Parameters.AddWithValue("type", type);
                    cmd.Parameters.AddWithValue("ccnumber", ccNumber);
                    cmd.Parameters.AddWithValue("cardfirstname", cardFirstName);
                    cmd.Parameters.AddWithValue("cardlastname", cardLastName);
					cmd.Parameters.AddWithValue("expirationdate", expirationDate);
                    cmd.Parameters.AddWithValue("cvc", cvc);
                    cmd.Parameters.AddWithValue("addressid", addressID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        return _sqlParser.ParseCreditCard(reader);
                    }
                }
            }
        }

        public IEnumerable<Flight> GetFlight(DateTime date, int flightNumber, DateTime departureTime, DateTime arrivalTime, 
            string departureAirport, string arrivalAirport, int maxCoach, int maxFirstClass, int bookedCoach, int bookedFirstClass)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM flight WHERE date = @date AND flightnumber = @flightnumber AND departuretime = @departuretime AND" +
                        "arrivaltime = @arrivaltime AND departureairport = @departureairport AND arrivalairport = @arrivalairport AND" +
                        "maxcoach = @maxcoach AND maxfirstclass = @maxfirstclass AND bookedcoach = @bookedcoach AND bookedfirstclass = @bookedfirstclass";
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("flightnumber", flightNumber);
                    cmd.Parameters.AddWithValue("departuretime", departureTime);
                    cmd.Parameters.AddWithValue("arrivaltime", arrivalTime);
					cmd.Parameters.AddWithValue("departureairport", departureAirport);
                    cmd.Parameters.AddWithValue("arrivalairport", arrivalAirport);
                    cmd.Parameters.AddWithValue("maxcoach", maxCoach);
                    cmd.Parameters.AddWithValue("maxfirstclass", maxFirstClass);
                    cmd.Parameters.AddWithValue("bookedcoach", bookedCoach);
                    cmd.Parameters.AddWithValue("bookedfirstclass", bookedFirstClass);

                    using (var reader = cmd.ExecuteReader())
                    {
                        return _sqlParser.ParseFlight(reader);
                    }
                }
            }
        }

        public IEnumerable<MileageProgram> GetMileageProgram(int miles, string email, string airline, long bookingID)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM mileageprogram WHERE miles = @miles AND email = @email AND airline = @airline AND " +
                        "bookingid = @bookingid";
                    cmd.Parameters.AddWithValue("miles", miles);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("airline", airline);
                    cmd.Parameters.AddWithValue("bookingid", bookingID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        return _sqlParser.ParseMileageProgram(reader);
                    }
                }
            }
        }

        public IEnumerable<Price> GetPrice(string flightClass, decimal cost, DateTime date, int flightNumber, string airline)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM price WHERE flightclass = @flightclass AND cost = @cost AND date = @date AND airline = @airline";
                    cmd.Parameters.AddWithValue("", flightClass);
                    cmd.Parameters.AddWithValue("cost", cost);
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("", flightNumber);
					cmd.Parameters.AddWithValue("airline", airline);

                    using (var reader = cmd.ExecuteReader())
                    {
                        return _sqlParser.ParsePrice(reader);
                    }
                }
            }
        }
        #endregion

        #region SQL INSERTS

        public void InsertCustomer(string firstName, string lastName, string email, string iataID)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "INSERT INTO customer (email, firstname, lastname, iata_id) VALUES (@email, @firstname, @lastname, @iata_id);";
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("firstname", firstName);
                    cmd.Parameters.AddWithValue("lastname", lastName);
                    cmd.Parameters.AddWithValue("iata_id", iataID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region SQL UPDATES

        public void UpdateCustomer(string firstName, string lastName, string email, string iataID)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "UPDATE customer SET firstname = @firstname, lastname = @lastname, iata_id = @iata_id WHERE email = @email;";
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("firstname", firstName);
                    cmd.Parameters.AddWithValue("lastname", lastName);
                    cmd.Parameters.AddWithValue("iata_id", iataID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region SQL DELETES

        public void DeleteCustomer(string email)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "DELETE FROM customer WHERE email = @email;";
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion
     }
}