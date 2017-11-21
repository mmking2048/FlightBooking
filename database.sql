CREATE TABLE Airport
(
  IATA_ID CHAR(3) NOT NULL,
  Country VARCHAR(254) NOT NULL,
  State CHAR(2),
  PRIMARY KEY (IATA_ID)
);

CREATE TABLE Address
(
  StreetNumber INT NOT NULL,
  StreetName VARCHAR(254) NOT NULL,
  City VARCHAR(254) NOT NULL,
  State CHAR(2),
  ZipCode CHAR(5),
  Country VARCHAR(254) NOT NULL,
  AddressID SERIAL NOT NULL,
  PRIMARY KEY (AddressID),
  --Ensure we're not creating a new AddressID for a duplicate address
  UNIQUE (StreetNumber, StreetName, City, State, ZipCode, Country),
  --if country = United States or Canada then State and ZipCode NOT NULL
  CHECK (((Country = 'United States' OR Country = 'Canada') AND State IS NOT NULL AND ZipCode IS NOT NULL) OR (Country <> 'United States' AND Country <> 'Canada'))
);

CREATE TABLE Credit_Card
(
  Type VARCHAR(254) NOT NULL,
  CCNumber CHAR(16) NOT NULL,
  CardFirstName VARCHAR(254) NOT NULL,
  CardLastName VARCHAR(254) NOT NULL,
  ExpirationDate DATE NOT NULL,
  CVC CHAR(3) NOT NULL,
  AddressID INT NOT NULL,
  PRIMARY KEY (CCNumber),
  FOREIGN KEY (AddressID) REFERENCES Address(AddressID),
  --check to make sure all characters in CCNumber are digits
  CHECK (CCNumber LIKE '%[^0123456789]%')
);

CREATE TABLE Airline
(
  Airline CHAR(2) NOT NULL,
  Country VARCHAR(254) NOT NULL,
  AirlineName VARCHAR(254) NOT NULL,
  PRIMARY KEY (Airline)
);

CREATE TABLE Customer
(
  FirstName VARCHAR(254) NOT NULL,
  LastName VARCHAR(254) NOT NULL,
  Email VARCHAR(254) NOT NULL,
  IATA_ID CHAR(3) NOT NULL,
  PRIMARY KEY (Email),
  FOREIGN KEY (IATA_ID) REFERENCES Airport(IATA_ID)
);

CREATE TABLE Booking
(
  BookingID SERIAL NOT NULL,
  Class VARCHAR(7) NOT NULL,
  Email VARCHAR(254) NOT NULL,
  CCNumber CHAR(16) NOT NULL,
  PRIMARY KEY (BookingID),
  FOREIGN KEY (Email) REFERENCES Customer(Email),
  FOREIGN KEY (CCNumber) REFERENCES Credit_Card(CCNumber)
);

CREATE TABLE Flight
(
  Date DATE NOT NULL,
  FlightNumber INT NOT NULL,
  DepartureTime TIME(0) NOT NULL,
  MaxCoach INT NOT NULL,
  MaxFirstClass INT NOT NULL,
  ArrivalTime TIME(0) NOT NULL,
  DepartureAirport CHAR(3) NOT NULL,
  ArrivalAirport CHAR(3) NOT NULL,
  Airline CHAR(2) NOT NULL,
  PRIMARY KEY (Date, FlightNumber, Airline),
  FOREIGN KEY (DepartureAirport) REFERENCES Airport(IATA_ID),
  FOREIGN KEY (ArrivalAirport) REFERENCES Airport(IATA_ID),
  FOREIGN KEY (Airline) REFERENCES Airline(Airline)
);

CREATE TABLE Price
(
  Class VARCHAR(7) NOT NULL,
  Cost NUMERIC(7, 2) NOT NULL,
  Date DATE NOT NULL,
  FlightNumber INT NOT NULL,
  Airline CHAR(2) NOT NULL,
  PRIMARY KEY (Class, Date, FlightNumber, Airline),
  FOREIGN KEY (Date, FlightNumber, Airline) REFERENCES Flight(Date, FlightNumber, Airline)
);

CREATE TABLE CreditCardOwner
(
  Email VARCHAR(254) NOT NULL,
  CCNumber CHAR(16) NOT NULL,
  PRIMARY KEY (Email, CCNumber),
  FOREIGN KEY (Email) REFERENCES Customer(Email),
  FOREIGN KEY (CCNumber) REFERENCES Credit_Card(CCNumber)
);

CREATE TABLE LivesAt
(
  Email VARCHAR(254) NOT NULL,
  AddressID INT NOT NULL,
  PRIMARY KEY (Email, AddressID),
  FOREIGN KEY (Email) REFERENCES Customer(Email),
  FOREIGN KEY (AddressID) REFERENCES Address(AddressID)
);

CREATE TABLE BookingFlights
(
  BookingID INT NOT NULL,
  Date DATE NOT NULL,
  FlightNumber INT NOT NULL,
  Airline CHAR(2) NOT NULL,
  PRIMARY KEY (BookingID, Date, FlightNumber, Airline),
  FOREIGN KEY (BookingID) REFERENCES Booking(BookingID),
  FOREIGN KEY (Date, FlightNumber, Airline) REFERENCES Flight(Date, FlightNumber, Airline)
);

CREATE TABLE MileageProgram
(
  Miles INT NOT NULL,
  Email VARCHAR(254) NOT NULL,
  Airline CHAR(2) NOT NULL,
  BookingID INT NOT NULL,
  PRIMARY KEY (Email, Airline, BookingID),
  FOREIGN KEY (Email) REFERENCES Customer(Email),
  FOREIGN KEY (Airline) REFERENCES Airline(Airline),
  FOREIGN KEY (BookingID) REFERENCES Booking(BookingID)
);