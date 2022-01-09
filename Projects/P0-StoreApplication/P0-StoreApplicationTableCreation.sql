CREATE TABLE Customers(
	CustomerId INT PRIMARY KEY IDENTITY NOT NULL,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	UserName nvarchar(50) NOT NULL,
	UserPassword nvarchar(50) NOT NULL
);

CREATE TABLE Orders(
	OrderId INT PRIMARY KEY IDENTITY NOT NULL,
	CustomerId INT NOT NULL FOREIGN KEY REFERENCES Customers(CustomerId) ON DELETE CASCADE,
	TotalAmount DECIMAL(6,2) NOT NULL
);

CREATE TABLE Stores(
	StoreId INT PRIMARY KEY IDENTITY NOT NULL,
	StoreName nvarchar(50) NOT NULL
);

CREATE TABLE Products(
	ProductId INT PRIMARY KEY IDENTITY NOT NULL,
	StoreId INT NOT NULL FOREIGN KEY REFERENCES Stores(StoreId) ON DELETE CASCADE,
	ProductName nvarchar(50) NOT NULL,
	ProductDesc nvarchar(150) NOT NULL,
	ProductAmount DECIMAL(6,2) NOT NULL
);

CREATE TABLE OrderProduct(
	OrderId INT NOT NULL FOREIGN KEY REFERENCES Orders(OrderId) ON DELETE CASCADE,
	ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(ProductId) ON DELETE CASCADE
);

INSERT INTO Stores (StoreName) VALUES ('Best Buy');
INSERT INTO Stores (StoreName) VALUES ('Kroger');
INSERT INTO Stores (StoreName) VALUES ('The Home Depot');
INSERT INTO Stores (StoreName) VALUES ('Kohl''s');


INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (1, 'AirPods Pro', 'Apple - AirPods Pro (with Magsafe Charging Case) - White', 249.00);
INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (1, 'Sony PlayStation 5', 'Sony - Playstation 5 Disc Edition', 499.99);
INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (1, 'iPhone 13 Pro Max', 'Apple - iPhone 13 Pro Max 5G 128GB - Graphite (Verizon)', 1099.99);
INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (1, 'Samsung Soundbar', 'Samsung - 2.1-Channel Soundbar with Wireless Subwoofer and DOLBY AUDIO / DTS 2.0 - Black', 129.99);

INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (2, 'Wheat Bread', 'Nature''s Own® Honey Wheat Sliced Bread', 3.79);
INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (2, 'Eggs', 'Kroger® Grade A Large Eggs', 2.49);
INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (2, 'Bacon', 'Kroger® Naturally Hardwood Smoked Bacon', 5.49);
INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (2, 'Milk', 'Kroger® 2% Reduced Fat Milk', 3.29);

INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (3, 'Christmas Tree', '7.5 ft Jackson Noble Fir LED Pre-Lit Artificial Christmas Tree with 1200 Color Changing Micro Dot Lights', 299.00);
INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (3, 'Nexgrill', '2-Burner Propane Gas Grill in Black with Griddle Top', 199.00);
INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (3, 'Wood Decking Board', '2 in. x 4 in. x 8 ft. #2 Ground Contact Pressure-Treated Lumber', 5.98);
INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (3, 'Ryobi Electric Lawnmower', '40V Brushless 20 in. Cordless Walk Behind Self-Propelled Lawn Mower with 6.0 Ah Battery & Charger', 429.00);

INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (4, 'Sweater', 'IZOD Advantage Performance Fleece Crewneck Pullover Sweater - Black', 25.49);
INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (4, 'Hoodie', 'Nike Pullover Fleece Hoodie - Red', 52.00);
INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (4, 'Polo', 'adidas Primegreen Check Print Polo', 45.00);
INSERT INTO Products (StoreId, ProductName, ProductDesc, ProductAmount) VALUES (4, 'T-Shirt', 'Skechers® GOWalk On the Road Tee', 20.80);

INSERT INTO Customers (FirstName, LastName, UserName, UserPassword) VALUES ('Aldo', 'Ramirez', 'Alditone', '12345');
INSERT INTO Customers (FirstName, LastName, UserName, UserPassword) VALUES ('Bob', 'Ross', 'BobbyR', '54321');
INSERT INTO Customers (FirstName, LastName, UserName, UserPassword) VALUES ('John', 'Doe', 'helloworld', 'abc123');

INSERT INTO Orders (CustomerId, TotalAmount) VALUES (1, 7.89);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (1, 6);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (1, 7);

INSERT INTO Orders (CustomerId, TotalAmount) VALUES (1, 499.99);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (2, 2);

INSERT INTO Orders (CustomerId, TotalAmount) VALUES (1, 98.29);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (3, 13);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (3, 14);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (3, 16);

INSERT INTO Orders (CustomerId, TotalAmount) VALUES (1, 14.56);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (4, 5);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (4, 6);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (4, 7);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (4, 8);

INSERT INTO Orders (CustomerId, TotalAmount) VALUES (2, 14.56);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (5, 5);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (5, 6);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (5, 7);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (5, 8);