using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        #endregion

        #region SQL INSERTS
        #endregion

        #region SQL UPDATES
        #endregion

        #region SQL DELETES
        #endregion
    }
}