CREATE TABLE Person(
	PersonId INT PRIMARY KEY IDENTITY NOT NULL,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL
);

CREATE TABLE Flavor(
	FlavorId INT PRIMARY KEY IDENTITY NOT NULL,
	FlavorName nvarchar(50) NOT NULL
);

CREATE TABLE PersonFlavor(
	PeopleFlavorsId INT PRIMARY KEY IDENTITY NOT NULL,
	PersonId INT NOT NULL FOREIGN KEY REFERENCES Person(PersonId) ON DELETE CASCADE,
	FlavorId INT NOT NULL FOREIGN KEY REFERENCES Flavor(FlavorId) ON DELETE CASCADE
);

/*
DROP TABLE PersonFlavor;
DROP TABLE Flavor;
DROP TABLE Person;
*/