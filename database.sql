CREATE TABLE Airport
(
  IATA_ID CHAR(3) NOT NULL,
  AirportName VARCHAR(256) NOT NULL,
  Country VARCHAR(256) NOT NULL,
  State CHAR(2),
  PRIMARY KEY (IATA_ID),
  -- State is only allowed to be null for non US and Canadian addresses
  CHECK (((Country = 'United States' OR Country = 'Canada') AND State IS NOT NULL OR (Country <> 'United States' AND Country <> 'Canada')))
);

CREATE TABLE Address
(
  StreetNumber INT NOT NULL,
  StreetName VARCHAR(256) NOT NULL,
  City VARCHAR(256) NOT NULL,
  State CHAR(2),
  ZipCode CHAR(5),
  Country VARCHAR(256) NOT NULL,
  --Address ID needed since State and ZipCode are optional
  AddressID SERIAL NOT NULL,
  PRIMARY KEY (AddressID),
  --Ensure we're not creating a new AddressID for a duplicate address
  UNIQUE (StreetNumber, StreetName, City, State, ZipCode, Country),
  --if country = United States or Canada then State and ZipCode NOT NULL
  CHECK (((Country = 'United States' OR Country = 'Canada') AND State IS NOT NULL AND ZipCode IS NOT NULL) OR (Country <> 'United States' AND Country <> 'Canada'))
);

CREATE TABLE Credit_Card
(
  Type VARCHAR(256) NOT NULL,
  CCNumber CHAR(16) NOT NULL,
  CardFirstName VARCHAR(256) NOT NULL,
  CardLastName VARCHAR(256) NOT NULL,
  ExpirationDate DATE NOT NULL,
  CVC CHAR(3) NOT NULL,
  AddressID INT NOT NULL,
  PRIMARY KEY (CCNumber),
  FOREIGN KEY (AddressID) REFERENCES Address(AddressID) ON UPDATE CASCADE,
  --check to make sure all characters in CCNumber are digits
  CHECK (CCNumber NOT LIKE '%[^0-9]%')
);

CREATE TABLE Airline
(
  Airline CHAR(2) NOT NULL,
  Country VARCHAR(256) NOT NULL,
  AirlineName VARCHAR(256) NOT NULL,
  PRIMARY KEY (Airline)
);

CREATE TABLE Customer
(
  FirstName VARCHAR(256) NOT NULL,
  LastName VARCHAR(256) NOT NULL,
  Email VARCHAR(256) NOT NULL,
  IATA_ID CHAR(3) NOT NULL,
  PRIMARY KEY (Email),
  FOREIGN KEY (IATA_ID) REFERENCES Airport(IATA_ID) ON UPDATE CASCADE ON DELETE SET NULL
);

CREATE TABLE Booking
(
  BookingID SERIAL NOT NULL,
  Class VARCHAR(7) NOT NULL,
  Email VARCHAR(256) NOT NULL,
  CCNumber CHAR(16) NOT NULL,
  PRIMARY KEY (BookingID),
  FOREIGN KEY (Email) REFERENCES Customer(Email) ON UPDATE CASCADE,
  FOREIGN KEY (CCNumber) REFERENCES Credit_Card(CCNumber) ON UPDATE CASCADE
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
  FOREIGN KEY (DepartureAirport) REFERENCES Airport(IATA_ID) ON UPDATE CASCADE,
  FOREIGN KEY (ArrivalAirport) REFERENCES Airport(IATA_ID) ON UPDATE CASCADE,
  FOREIGN KEY (Airline) REFERENCES Airline(Airline) ON UPDATE CASCADE
);

CREATE TABLE Price
(
  Class VARCHAR(7) NOT NULL,
  Cost NUMERIC(7, 2) NOT NULL,
  Date DATE NOT NULL,
  FlightNumber INT NOT NULL,
  Airline CHAR(2) NOT NULL,
  PRIMARY KEY (Class, Date, FlightNumber, Airline),
  FOREIGN KEY (Date, FlightNumber, Airline) REFERENCES Flight(Date, FlightNumber, Airline) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE CreditCardOwner
(
  Email VARCHAR(256) NOT NULL,
  CCNumber CHAR(16) NOT NULL,
  PRIMARY KEY (Email, CCNumber),
  FOREIGN KEY (Email) REFERENCES Customer(Email) ON UPDATE CASCADE,
  FOREIGN KEY (CCNumber) REFERENCES Credit_Card(CCNumber) ON UPDATE CASCADE
);

CREATE TABLE LivesAt
(
  Email VARCHAR(256) NOT NULL,
  AddressID INT NOT NULL,
  PRIMARY KEY (Email, AddressID),
  FOREIGN KEY (Email) REFERENCES Customer(Email) ON UPDATE CASCADE,
  FOREIGN KEY (AddressID) REFERENCES Address(AddressID) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE BookingFlights
(
  BookingID INT NOT NULL,
  Date DATE NOT NULL,
  FlightNumber INT NOT NULL,
  Airline CHAR(2) NOT NULL,
  PRIMARY KEY (BookingID, Date, FlightNumber, Airline),
  FOREIGN KEY (BookingID) REFERENCES Booking(BookingID) ON UPDATE CASCADE,
  FOREIGN KEY (Date, FlightNumber, Airline) REFERENCES Flight(Date, FlightNumber, Airline) ON UPDATE CASCADE
);

CREATE TABLE MileageProgram
(
  Miles INT NOT NULL,
  Email VARCHAR(256) NOT NULL,
  Airline CHAR(2) NOT NULL,
  BookingID INT NOT NULL,
  PRIMARY KEY (Email, Airline, BookingID),
  FOREIGN KEY (Email) REFERENCES Customer(Email) ON UPDATE CASCADE,
  FOREIGN KEY (Airline) REFERENCES Airline(Airline) ON UPDATE CASCADE,
  FOREIGN KEY (BookingID) REFERENCES Booking(BookingID) ON UPDATE CASCADE
);