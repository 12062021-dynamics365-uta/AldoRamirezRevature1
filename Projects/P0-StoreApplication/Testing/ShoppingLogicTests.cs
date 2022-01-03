using Xunit;
using Model;
using Domain;
using System.Collections.Generic;
using System.Linq;
using Storage;

namespace Testing
{
    public class ShoppingLogicTests
    {
        [Fact]
        public void LoginUserTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);

            bool login = shopping.Login("Alditone", "12345");
            Customer customer = shopping.CurrentCustomer;

            Assert.True(login);
            Assert.Equal(1, customer.CustomerId);
            Assert.Equal("Aldo", customer.Fname);
            Assert.Equal("Ramirez", customer.Lname);
        }

        [Fact]
        public void RegisterUserTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);

            shopping.Register("Aldo", "Ramirez", "Alditone", "12345");
            Customer customer = shopping.CurrentCustomer;

            Assert.Equal(1, customer.CustomerId);
            Assert.Equal("Aldo", customer.Fname);
            Assert.Equal("Ramirez", customer.Lname);
        }

        [Fact]
        public void InitializeStoresTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);

            shopping.CurrentCustomer = new Customer();
            shopping.InitializeStores();
            List<Store> stores = shopping.CurrentCustomer.StoreLocations;

            Assert.Equal(1, stores[0].StoreId);
            Assert.Equal("Store1", stores[0].Name);

            Assert.Equal(2, stores[1].StoreId);
            Assert.Equal("Store2", stores[1].Name);
        }

        [Fact]
        public void ValidateMainMenuChoiceTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);

            int validChoice = shopping.ValidateMainMenuChoice("1");
            int invalidChoice = shopping.ValidateMainMenuChoice("4");

            Assert.StrictEqual(1, validChoice);
            Assert.StrictEqual(0, invalidChoice);
        }

        [Fact]
        public void ValidateStoreListMenuChoiceTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);

            shopping.Login("Alditone", "12345");
            int storeCount = shopping.CurrentCustomer.StoreLocations.Count + 2;

            int validChoice = shopping.ValidateStoreListMenuChoice("1");
            int invalidChoice = shopping.ValidateStoreListMenuChoice(storeCount.ToString());

            Assert.StrictEqual(1, validChoice);
            Assert.StrictEqual(0, invalidChoice);
        }

        [Fact]
        public void InitializeCurrentStoreProductsTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);

            shopping.CurrentStore = new Store();
            shopping.InitializeCurrentStoreProducts();
            List<Product> products = shopping.CurrentStore.Products;

            Assert.Equal(1, products[0].ProductId);
            Assert.Equal("Product 1", products[0].Name);
            Assert.Equal("Test Product 1", products[0].Description);
            Assert.Equal(1, products[0].Price);

            Assert.Equal(2, products[1].ProductId);
            Assert.Equal("Product 2", products[1].Name);
            Assert.Equal("Test Product 2", products[1].Description);
            Assert.Equal(2, products[1].Price);
        }

        [Fact]
        public void InitializePreviousStoreOrdersTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);

            shopping.CurrentCustomer = new Customer();
            shopping.CurrentStore = new Store();
            shopping.InitializePreviousStoreOrders();

            List<Order> orders = shopping.CurrentCustomer.PastOrders;

            Assert.Equal(1, orders[0].OrderId);
            Assert.Equal(3, orders[0].TotalCost);
            Assert.Equal(1, orders[0].Products[0].ProductId);
            Assert.Equal("Product 1", orders[0].Products[0].Name);
            Assert.Equal("Test Product 1", orders[0].Products[0].Description);
            Assert.Equal(1, orders[0].Products[0].Price);

            Assert.Equal(2, orders[0].Products[1].ProductId);
            Assert.Equal("Product 2", orders[0].Products[1].Name);
            Assert.Equal("Test Product 2", orders[0].Products[1].Description);
            Assert.Equal(2, orders[0].Products[1].Price);
        }

        [Fact]
        public void ValidateStoreMenuChoiceTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);

            shopping.Login("Alditone", "12345");
            int storeCount = shopping.CurrentCustomer.StoreLocations.Count;

            int validChoice = shopping.ValidateStoreMenuChoice("1");
            int invalidChoice = shopping.ValidateStoreMenuChoice((storeCount + 1).ToString());

            Assert.StrictEqual(1, validChoice);
            Assert.StrictEqual(0, invalidChoice);
        }

        [Fact]
        public void ValidateShoppingMenuChoiceTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);

            int validChoice = shopping.ValidateShoppingMenuChoice("1", 4);
            int invalidChoice = shopping.ValidateShoppingMenuChoice("5", 4);

            Assert.StrictEqual(1, validChoice);
            Assert.StrictEqual(0, invalidChoice);
        }

        [Fact]
        public void ConvertToIntTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);

            int number = shopping.ConvertInputToInt("123");
            int word = shopping.ConvertInputToInt("hello");

            Assert.StrictEqual(123, number);
            Assert.StrictEqual(0, word);
        }

        [Fact]
        public void GetListOfOrdersTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);

            shopping.CurrentCustomer = new Customer();
            shopping.CurrentStore = new Store();
            shopping.InitializePreviousStoreOrders();

            Customer customer = shopping.CurrentCustomer;
            List<Order> orders = shopping.GetListOfOrders();

            Assert.Equal(customer.PastOrders[0], orders[0]);
        }

        [Fact]
        public void AddProductToCartTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);

            shopping.CurrentCustomer = new Customer();
            Product product = new Product() { Price = 40 };
            List<Product> fullCartList = Enumerable.Range(0, 50).Select(x => new Product()).ToList();

            shopping.CurrentCustomer.Cart = fullCartList;
            bool cartFull = shopping.AddProductToCart(product);
            Assert.False(cartFull);

            shopping.CurrentCustomer.Order.TotalCost = 499;
            bool maxTotal = shopping.AddProductToCart(product);
            Assert.False(maxTotal);

            shopping.CurrentCustomer.Cart = new List<Product>();
            shopping.CurrentCustomer.Order.TotalCost = 0;
            bool cleanCart = shopping.AddProductToCart(product);
            Assert.True(cleanCart);
        }

        [Fact]
        public void ConvertCartToIEnumTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);
            shopping.CurrentCustomer = new Customer();

            List<Product> cart = new List<Product>() {
                new Product { ProductId = 1, Name = "Product1", Description = "Product description 1", Price = 1 },
                new Product { ProductId = 1, Name = "Product1", Description = "Product description 1", Price = 1 },
                new Product { ProductId = 2, Name = "Product2", Description = "Product description 2", Price = 2 },
                new Product { ProductId = 3, Name = "Product3", Description = "Product description 3", Price = 3 }
            };
            shopping.CurrentCustomer.Cart = cart;

            var q = shopping.ConvertCartToIEnum();

            Assert.Equal(3, q.Count());
            Assert.Equal(2, q.ElementAt(0).Value);
            Assert.Equal("Product1", q.ElementAt(0).Key.Name);
            Assert.Equal(1, q.ElementAt(0).Key.Price);

            Assert.Equal(1, q.ElementAt(1).Value);
            Assert.Equal("Product2", q.ElementAt(1).Key.Name);
            Assert.Equal(2, q.ElementAt(1).Key.Price);

            Assert.Equal(1, q.ElementAt(2).Value);
            Assert.Equal("Product3", q.ElementAt(2).Key.Name);
            Assert.Equal(3, q.ElementAt(2).Key.Price);
        }

        [Fact]
        public void ConvertOrdersToIEnumTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);
            shopping.CurrentCustomer = new Customer();

            List<Product> products = new List<Product>() { 
                new Product { ProductId = 1, Name = "Product1", Description = "Product description 1", Price = 1 },
                new Product { ProductId = 1, Name = "Product1", Description = "Product description 1", Price = 1 },
                new Product { ProductId = 2, Name = "Product2", Description = "Product description 2", Price = 2 },
                new Product { ProductId = 3, Name = "Product3", Description = "Product description 3", Price = 3 }
            };
            Order order = new Order() { OrderId = 1, Products = products, TotalCost = 7 };

            shopping.CurrentCustomer.PastOrders.Add(order);

            var q = shopping.ConvertOrdersToIEnum(0);

            Assert.Equal(3, q.Count());
            Assert.Equal(2, q.ElementAt(0).Value);
            Assert.Equal("Product1", q.ElementAt(0).Key.Name);
            Assert.Equal(1, q.ElementAt(0).Key.Price);

            Assert.Equal(1, q.ElementAt(1).Value);
            Assert.Equal("Product2", q.ElementAt(1).Key.Name);
            Assert.Equal(2, q.ElementAt(1).Key.Price);

            Assert.Equal(1, q.ElementAt(2).Value);
            Assert.Equal("Product3", q.ElementAt(2).Key.Name);
            Assert.Equal(3, q.ElementAt(2).Key.Price);
        }

        [Fact]
        public void CheckoutTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);
            shopping.CurrentCustomer = new Customer();

            List<Product> products = new List<Product>() {
                new Product { ProductId = 1, Name = "Product1", Description = "Product description 1", Price = 1 },
                new Product { ProductId = 1, Name = "Product1", Description = "Product description 1", Price = 1 },
                new Product { ProductId = 2, Name = "Product2", Description = "Product description 2", Price = 2 },
                new Product { ProductId = 3, Name = "Product3", Description = "Product description 3", Price = 3 }
            };

            shopping.CurrentCustomer.Cart = products;
            shopping.CurrentCustomer.Order.Products = products;
            shopping.CurrentCustomer.Order.TotalCost = 7;

            bool successfull = shopping.Checkout();

            Assert.Empty(shopping.CurrentCustomer.Cart);

            Assert.True(successfull);
            Assert.Equal(0, shopping.CurrentCustomer.Order.OrderId);
            Assert.Equal(0, shopping.CurrentCustomer.Order.TotalCost);
            Assert.Empty(shopping.CurrentCustomer.Order.Products);
        }

        [Fact]
        public void RemoveItemFromCartTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);
            shopping.CurrentCustomer = new Customer();

            Product product1 = new Product { ProductId = 1, Name = "Product1", Description = "Product description 1", Price = 1 };
            Product product2 = new Product { ProductId = 1, Name = "Product1", Description = "Product description 1", Price = 1 };
            Product product3 = new Product { ProductId = 2, Name = "Product2", Description = "Product description 2", Price = 2 };
            List<Product> products = new List<Product>() { product1, product2, product3 };

            shopping.CurrentCustomer.Cart = products;
            shopping.CurrentCustomer.Order.Products = products;
            shopping.CurrentCustomer.Order.TotalCost = 4;

            shopping.RemoveItemFromCart(product1);

            Assert.Equal(2, shopping.CurrentCustomer.Order.Products.Count);
            Assert.Equal(2, shopping.CurrentCustomer.Cart.Count);

            Assert.Equal(3, shopping.CurrentCustomer.Order.TotalCost);
        }

        [Fact]
        public void CancelOrderTest()
        {
            IDataBaseAccess mockDataBase = new MockDataBaseAccess();
            ShoppingLogic shopping = new ShoppingLogic(mockDataBase);
            shopping.CurrentCustomer = new Customer();

            List<Product> products = new List<Product>() {
                new Product { ProductId = 1, Name = "Product1", Description = "Product description 1", Price = 1 },
                new Product { ProductId = 1, Name = "Product1", Description = "Product description 1", Price = 1 },
                new Product { ProductId = 2, Name = "Product2", Description = "Product description 2", Price = 2 },
                new Product { ProductId = 3, Name = "Product3", Description = "Product description 3", Price = 3 }
            };

            shopping.CurrentCustomer.Cart = products;
            shopping.CurrentCustomer.Order.Products = products;
            shopping.CurrentCustomer.Order.TotalCost = 7;

            shopping.CancelOrder();

            Assert.Empty(shopping.CurrentCustomer.Cart);

            Assert.Equal(0, shopping.CurrentCustomer.Order.OrderId);
            Assert.Equal(0, shopping.CurrentCustomer.Order.TotalCost);
            Assert.Empty(shopping.CurrentCustomer.Order.Products);
        }
    }
}
