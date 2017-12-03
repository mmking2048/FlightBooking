namespace FlightBooking.Models
{
    public class Address
    {
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public long AddressID { get; set; }

        public Address(int streetNumber, string streetName, string city, string zipCode, string country, long addressID)
        {
            StreetNumber = streetNumber;
            StreetName = streetName;
            City = city;
            ZipCode = zipCode;
            Country = country;
            AddressID = addressID;
        }
    }
}