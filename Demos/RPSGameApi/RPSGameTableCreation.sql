CREATE TABLE Players(
	PlayerId INT PRIMARY KEY IDENTITY NOT NULL,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	Losses INT NOT NULL,
	Wins INT NOT NULL
);

INSERT INTO Players (FirstName, LastName, Losses, Wins) VALUES ('Aldo', 'Ramirez', 0, 0);