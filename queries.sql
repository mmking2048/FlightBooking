-- Create new customer
-- In UI, use dropdown for IATA_ID
INSERT INTO Customer (FirstName, LastName, Email, IATA_ID)
VALUES ('First', 'Last', 'ab@email.com', 'ORD');

-- Add new address for customer
-- In UI, use dropdown for country and state
INSERT INTO Address (StreetNumber, StreetName, City, State, ZipCode, Country)
VALUES (111, 'Street', 'City', 'IL', 60600, 'United States');
-- Do this if for customer address (not credit card) or to link address to owner
INSERT INTO LivesAt (Email, AddressID) VALUES ('ab@email.com', 1);

-- Modify address info
UPDATE Address
SET StreetNumber = 111, StreetName = 'Street', City = 'City', State = 'IL', ZipCode = 60600, Country = 'United States'
WHERE AddressID = 1;

-- Delete address
DELETE FROM Address
WHERE AddressID = 1;

-- Add credit card
INSERT INTO CreditCard (Type, CCNumber, CardFirstName, CardLastName, ExpirationDate, CVC, AddressID)
VALUES ('Visa', '1111000011110000', 'First', 'Last', 01-01-2000, '111', 1);
-- Link credit card to owner
INSERT INTO CreditCardOwner (Email, CCNumber)
VALUES ('ab@email.com', '1111000011110000');

-- Modify credit card (cannot modify ccnumber, if necessary create new cc)
UPDATE CreditCard
SET Type = 'Visa', CardFirstName = 'First', CardLastName = 'Last', ExpirationDate = 01-01-2000, CVC = '111', AddressID = 1
WHERE CCNumber = '1111000011110000';

-- Delete credit card
DELETE FROM CreditCard
WHERE CCNumber = '1111000011110000';