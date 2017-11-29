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
    }
}