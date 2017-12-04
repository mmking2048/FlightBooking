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

        public IEnumerable<Customer> GetCustomer(string email, string firstName, string lastName, string iataID)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "SELECT * FROM customer WHERE email = @email AND firstname = @firstname AND lastname = @lastname AND iata_id = @iata_id";
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("firstname", firstName);
                    cmd.Parameters.AddWithValue("lastname", lastName);
                    cmd.Parameters.AddWithValue("iata_id", iataID);

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
                    cmd.CommandText = "SELECT * FROM airport WHERE iata_id = @iata_id AND airportname = @airportname AND country = @country AND " +
                        "state = @state AND latitude = @latitude AND longitude = @longitude";
                    cmd.Parameters.AddWithValue("iata_id", iataID);
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

        // TODO: GetCreditCardOwner
        /*
        public IEnumerable<Customer> GetCreditCardOwner(string email, string ccNumber)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM creditcardowner WHERE email = @email AND ccnumber = @ccnumber";
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("ccnumber", ccNumber);

                    using (var reader = cmd.ExecuteReader())
                    {
                        return _sqlParser.ParseCreditCardOwner(reader);
                    }
                }
            }
        }
        */
        // TODO: GetLivesAt
        // TODO: GetBookingFlights

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
                        "INSERT INTO customer (firstname, lastname, email, iata_id) VALUES (@firstname, @lastname, @email, @iata_id);";
                    cmd.Parameters.AddWithValue("firstname", firstName);
                    cmd.Parameters.AddWithValue("lastname", lastName);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("iata_id", iataID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertAirport(string iataID, string airportName, string country, string state, double latitude, double longitude)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "INSERT INTO airport (iata_id, airportname, country, state, latitude, longitude) " +
                        "VALUES (@iata_id, @airportname, @country, @state, @latitude, @longitude);";
                    cmd.Parameters.AddWithValue("iata_id", iataID);
                    cmd.Parameters.AddWithValue("airportname", airportName);
                    cmd.Parameters.AddWithValue("country", country);
                    cmd.Parameters.AddWithValue("state", state);
                    cmd.Parameters.AddWithValue("latitude", latitude);
                    cmd.Parameters.AddWithValue("longitude", longitude);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public long InsertAddress(int streetNumber, string streetName, string city, string state, string zipCode, string country)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                long addressID;
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "INSERT INTO address (streetnumber, streetname, city, state, zipcode, country) " +
                        "VALUES (@streetnumber, @streetname, @city, @state, @zipcode, @country) RETURNING addressid;";
                    cmd.Parameters.AddWithValue("streetnumber", streetNumber);
                    cmd.Parameters.AddWithValue("streetname", streetName);
                    cmd.Parameters.AddWithValue("city", city);
                    cmd.Parameters.AddWithValue("state", state);
                    cmd.Parameters.AddWithValue("zipcode", zipCode);
                    cmd.Parameters.AddWithValue("country", country);
                    addressID = (long) cmd.ExecuteScalar();
                }

                return addressID;
            }
        }

        public void InsertCreditCard(string type, string ccNumber, string cardFirstName, string cardLastName, DateTime expirationDate,
            string cvc, long addressID)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "INSERT INTO creditcard (type, ccnumber, cardfirstname, cardlastname, expirationdate, cvc, addressid) " +
                        "VALUES (@type, @ccnumber, @cardfirstname, @cardlastname, @expirationdate, @cvc, @addressid);";
                    cmd.Parameters.AddWithValue("type", type);
                    cmd.Parameters.AddWithValue("ccnumber", ccNumber);
                    cmd.Parameters.AddWithValue("cardfirstname", cardFirstName);
                    cmd.Parameters.AddWithValue("cardlastname", cardLastName);
                    cmd.Parameters.AddWithValue("expirationdate", expirationDate);
                    cmd.Parameters.AddWithValue("cvc", cvc);
                    cmd.Parameters.AddWithValue("addressid", addressID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertAirline(string airlineID, string country, string airlineName)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "INSERT INTO airline (airlineid, country, airlinename) " +
                        "VALUES (@airlineid, @country, @airlinename);";
                    cmd.Parameters.AddWithValue("airlineid", airlineID);
                    cmd.Parameters.AddWithValue("country", country);
                    cmd.Parameters.AddWithValue("airlinename", airlineName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertBooking(string email, string ccNumber, string flightClass, IEnumerable<Flight> flights)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "INSERT INTO booking (email, ccnumber, flightclass) " +
                        "VALUES (@email, @ccnumber, @flightclass) RETURNING bookingid;";
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("ccnumber", ccNumber);
                    cmd.Parameters.AddWithValue("flightclass", flightClass);
                    var bookingID = (int) cmd.ExecuteScalar();

                    foreach (Flight flight in flights)
                    {
                        cmd.CommandText =
                            "INSERT INTO bookingflights (bookingid, date, flightnumber, airlineid) " +
                            "VALUES (@bookingid, @date, @flightnumber, @airlineid)";
                        cmd.Parameters.AddWithValue("bookingid", bookingID);
                        cmd.Parameters.AddWithValue("date", flight.Date);
                        cmd.Parameters.AddWithValue("flightnumber", flight.FlightNumber);
                        cmd.Parameters.AddWithValue("airlineid", flight.AirlineID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void InsertFlight(DateTime date, int flightNumber, DateTimeOffset departureTime,
        DateTimeOffset arrivalTime, string departureAirport, string arrivalAirport,
        int maxCoach, int maxFirstClass, string airlineID, int bookedCoach, int bookedFirstClass)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "INSERT INTO flight (date, flightnumber, departuretime, arrivaltime, departureairport, maxcoach, maxfirstclass, airlineid, bookedcoach, bookedfirstclass) " +
                        "VALUES (@date, @flightnumber, @departuretime, @departureairport, @arrivalairport, @maxcoach, @maxfirstclass, @airlineid, @bookedcoach, @bookedfirstclass);";
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("flightnumber", flightNumber);
                    cmd.Parameters.AddWithValue("departuretime", departureTime);
                    cmd.Parameters.AddWithValue("arrivaltime", arrivalTime);
                    cmd.Parameters.AddWithValue("departureairport", departureAirport);
                    cmd.Parameters.AddWithValue("arrivalairport", arrivalAirport);
                    cmd.Parameters.AddWithValue("maxcoach", maxCoach);
                    cmd.Parameters.AddWithValue("maxfirstclass", maxFirstClass);
                    cmd.Parameters.AddWithValue("airlineid", airlineID);
                    cmd.Parameters.AddWithValue("bookedcoach", bookedCoach);
                    cmd.Parameters.AddWithValue("bookedfirstclass", bookedFirstClass);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertPrice(string flightClass, decimal cost, DateTime date, int flightNumber, string airline)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "INSERT INTO price (flightclass, cost, flightnumber, airlineid) " +
                        "VALUES (@flightclass, @cost, @flightnumber, @airlineid);";
                    cmd.Parameters.AddWithValue("flightclass", flightClass);
                    cmd.Parameters.AddWithValue("cost", cost);
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("flightnumber", flightNumber);
                    cmd.Parameters.AddWithValue("airlineid", airline);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertCreditCardOwner(string email, string ccNumber)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "INSERT INTO creditcardowner (email, ccnumber) " +
                        "VALUES (@email, @ccnumber);";
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("ccnumber", ccNumber);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertLivesAt(string email, string addressID)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "INSERT INTO livesat (email, addressid) " +
                        "VALUES (@email, @addressid);";
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("addressid", addressID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertBookingFlights(long bookingID, DateTime date, int flightNumber, string airlineID)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "INSERT INTO bookingflights (bookingid, date, flightnumber, airlineid) " +
                        "VALUES (@bookingid, @date, @flightnumber, @airlineid);";
                    cmd.Parameters.AddWithValue("bookingid", bookingID);
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("flightnumber", flightNumber);
                    cmd.Parameters.AddWithValue("airlineid", airlineID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertMileageProgram(int miles, string email, string airlineID, int bookingID)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "INSERT INTO mileageprogram (miles, email, airlineid, bookingid) " +
                        "VALUES (@miles, @email, @airlineid, @bookingid);";
                    cmd.Parameters.AddWithValue("miles", miles);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("airlineid", airlineID);
                    cmd.Parameters.AddWithValue("bookingid", bookingID);
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

        public void UpdateAirport(string iataID, string airportName, string country, string state, double latitude, double longitude)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "UPDATE airport SET airportname = @airportname, country = @country, state = @state, latitude = @latitude, longitude = longitude@ WHERE iata_id = @iata_id;";
                    cmd.Parameters.AddWithValue("airportname", airportName);
                    cmd.Parameters.AddWithValue("country", country);
                    cmd.Parameters.AddWithValue("state", state);
                    cmd.Parameters.AddWithValue("latitude", latitude);
					cmd.Parameters.AddWithValue("longitude", longitude);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateAddress(int streetNumber, string streetName, string city, string state, string zipCode, string country, int addressID)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "UPDATE address SET  streetnumber= @streetnumber, streetname = @streetname, city = @city, state = @state, zipcode = @zipcode, country = @country WHERE addressid = @addressid;";
                    cmd.Parameters.AddWithValue("streetnumber", streetNumber);
                    cmd.Parameters.AddWithValue("streetname", streetName);
                    cmd.Parameters.AddWithValue("city", city);
                    cmd.Parameters.AddWithValue("state", state);
					cmd.Parameters.AddWithValue("zipcode", zipCode);
                    cmd.Parameters.AddWithValue("country", country);
                    cmd.Parameters.AddWithValue("addressid", addressID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCreditCard(string type, string ccNumber, string cardFirstName, string cardLastName, DateTime expirationDate,
            string cvc, int addressID)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "UPDATE creditcard SET type = @type, cardfirstname = @cardfirstname, cardlastname = @cardlastname, expirationdate = @expirationdate, cvc = @cvc, addressid = @addressid WHERE ccnumber = @ccnumber;";
                    cmd.Parameters.AddWithValue("type", type);
                    cmd.Parameters.AddWithValue("cardfirstname", cardFirstName);
                    cmd.Parameters.AddWithValue("cardlastname", cardLastName);
                    cmd.Parameters.AddWithValue("expirationdate", expirationDate);
					cmd.Parameters.AddWithValue("cvc", cvc);
                    cmd.Parameters.AddWithValue("addressid", addressID);
                    cmd.Parameters.AddWithValue("ccnumber", ccNumber);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateAirline(string airlineID, string country, string airlineName)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "UPDATE airline SET country = @country, airlinename = @airlinename WHERE airlineid = @airlineid;";
                    cmd.Parameters.AddWithValue("airlineid", airlineID);
                    cmd.Parameters.AddWithValue("country", country);
                    cmd.Parameters.AddWithValue("airlinename", airlineName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateBooking(int bookingID, string email, string ccNumber, string flightClass)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "UPDATE booking SET email = @email, ccnumber = @ccnumber, flightclass = @flightclass WHERE bookingid = @bookingid;";
                    cmd.Parameters.AddWithValue("bookingid", bookingID);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("ccnumber", ccNumber);
                    cmd.Parameters.AddWithValue("flightclass", flightClass);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateFlight(DateTime date, int flightNumber, DateTimeOffset departureTime, DateTimeOffset arrivalTime, string departureAirport, 
            string arrivalAirport, int maxCoach, int maxFirstClass, string airlineID, int bookedCoach, int bookedFirstClass)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "UPDATE flight SET departuretime = @departuretime, arrivaltime = @arrivaltime, departureairport = @departureairport, " +
                        "arrivalairport = @arrivalairport, maxcoach = @maxcoach, maxfirstclass = @maxfirstclass, bookedcoach = @bookedcoach, bookedfirstclass = @bookedfirstclass" +
                        "WHERE date = @date AND flightnumber = @flightnumber AND airlineid = @airlineid;";
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("flightnumber", flightNumber);
                    cmd.Parameters.AddWithValue("departuretime", departureTime);
                    cmd.Parameters.AddWithValue("arrivaltime", arrivalTime);
					cmd.Parameters.AddWithValue("departureairport", departureAirport);
                    cmd.Parameters.AddWithValue("arrivalairport", arrivalAirport);
                    cmd.Parameters.AddWithValue("maxcoach", maxCoach);
                    cmd.Parameters.AddWithValue("maxfirstclass", maxFirstClass);
					cmd.Parameters.AddWithValue("airlineid", airlineID);
                    cmd.Parameters.AddWithValue("bookedcoach", bookedCoach);
                    cmd.Parameters.AddWithValue("bookedfirstclass", bookedFirstClass);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePrice(string flightClass, decimal cost, DateTime date, int flightNumber, string airline)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "UPDATE price SET cost = @cost WHERE flightclass = @flightclass AND date = @date AND flightnumber = @flightnumber AND airlineid = @airlineid;";
                    cmd.Parameters.AddWithValue("flightclass", flightClass);
                    cmd.Parameters.AddWithValue("cost", cost);
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("flightnumber", flightNumber);
					cmd.Parameters.AddWithValue("airlineid", airline);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateMileageProgram(int miles, string email, string airline, int bookingID)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "UPDATE mileageprogram SET miles = @miles WHERE email = @email AND airlineid = @airlineid AND bookingid = @bookingid;";
                    cmd.Parameters.AddWithValue("miles", miles);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("airlineid", airline);
                    cmd.Parameters.AddWithValue("bookingid", bookingID);
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