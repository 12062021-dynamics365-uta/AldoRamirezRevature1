using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Storage;

namespace Domain
{
    public class ShoppingLogic : IShoppingLogic
    {
        private readonly IDataBaseAccess _dbContext;
        public Customer CurrentCustomer { get; set; }
        public Store CurrentStore { get; set; }

        public ShoppingLogic(IDataBaseAccess dba)
        {
            this._dbContext = dba;
        }

        /// <summary>
        /// Logs in user, if data base insert returns null
        /// </summary>
        /// <param name="userName">Users username</param>
        /// <param name="password">Users password</param>
        /// <returns>bool</returns>
        public bool Login(string userName, string password)
        {
            Customer customer = _dbContext.GetCustomer(userName, password);

            if (customer != null)
            {
                customer.StoreLocations = _dbContext.GetStores();
                CurrentCustomer = customer;
                return true;
            }
            else
            {
                CurrentCustomer = null;
                return false;
            }
        }

        /// <summary>
        /// Adds new customer to the
        /// </summary>
        /// <param name="firstName">Users first name</param>
        /// <param name="lastName">Users last name</param>
        /// <param name="userName">Users username</param>
        /// <param name="password">Users password</param>
        public void Register(string firstName, string lastName, string userName, string password)
        {
            int id = _dbContext.AddCustomer(firstName, lastName, userName, password);
            CurrentCustomer = new Customer(id, firstName, lastName);
            CurrentCustomer.StoreLocations = _dbContext.GetStores();
        }

        /// <summary>
        /// Validates users choice in the Main Menu
        /// </summary>
        /// <param name="userInput">Users Input string</param>
        /// <returns>int</returns>
        public int ValidateMainMenuChoice(string userInput)
        {
            int userChoice = ConvertInputToInt(userInput);
            int numOfChoices = 3;

            if (userChoice > numOfChoices)
                userChoice = 0;

            return userChoice;
        }

        /// <summary>
        /// Validates user choice in the Store List Menu
        /// </summary>
        /// <param name="userInput">Users Input string</param>
        /// <returns>int</returns>
        public int ValidateStoreListMenuChoice(string userInput)
        {
            int userChoice = ConvertInputToInt(userInput);
            int numOfChoices = CurrentCustomer.StoreLocations.Count;

            if (userChoice > numOfChoices + 1)
                userChoice = 0;
            else if (userChoice != numOfChoices + 1)
            {
                CurrentStore = CurrentCustomer.StoreLocations.Find(x => x.StoreId == userChoice);
                CurrentStore.Products = _dbContext.GetStoreProducts(CurrentStore.StoreId);
                InitializePreviousStoreOrders();
            }

            return userChoice;
        }

        /// <summary>
        /// Validates users choice in the Store Menu
        /// </summary>
        /// <param name="userInput">Users Input string</param>
        /// <returns>int</returns>
        public int ValidateStoreMenuChoice(string userInput)
        {
            int userChoice = ConvertInputToInt(userInput);
            int numOfChoices = 4;

            if (userChoice > numOfChoices)
                userChoice = 0;

            return userChoice;
        }

        /// <summary>
        /// Validates users choice in the shopping menu
        /// </summary>
        /// <param name="userInput">Users Input string</param>
        /// <param name="numOfChoices"></param>
        /// <returns> int</returns>
        public int ValidateShoppingMenuChoice(string userInput, int numOfChoices)
        {
            int userChoice = ConvertInputToInt(userInput);

            if (userChoice > numOfChoices)
                userChoice = 0;

            return userChoice;
        }

        /// <summary>
        /// Converts user input into integer
        /// </summary>
        /// <param name="userInput">Users Input string</param>
        /// <returns>int</returns>
        public int ConvertInputToInt(string userInput)
        {
            bool conversionBool = Int32.TryParse(userInput, out int convertedNumber);

            if (!conversionBool)
                convertedNumber = 0;

            return convertedNumber;
        }

        /// <summary>
        /// Gets current customers list of orders
        /// </summary>
        /// <returns>List<Order></Order></returns>
        public List<Order> GetListOfOrders()
        {
            return CurrentCustomer.PastOrders;
        }

        /// <summary>
        /// Adds product to current customers cart
        /// </summary>
        /// <param name="product"></param>
        /// <returns>bool</returns>
        public bool AddProductToCart(Product product)
        {
            if (CurrentCustomer.Order.TotalCost + product.Price < 500)
            {
                CurrentCustomer.Cart.Add(product);
                CurrentCustomer.Order.Products.Add(product);
                CurrentCustomer.Order.TotalCost += product.Price;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checkout current customer, creates new order
        /// adds it to current customer past orders list
        /// and adds it to the database
        /// </summary>
        public void Checkout()
        {
            Order order = new Order();
            order.Products = CurrentCustomer.Cart;
            order.TotalCost = Math.Round(CurrentCustomer.Cart.Sum(p => p.Price), 2);
            CurrentCustomer.PastOrders.Add(order);
            order.OrderId = _dbContext.AddOrder(CurrentCustomer.CustomerId, order.TotalCost);
            foreach (Product p in order.Products)
                _dbContext.AddOrderProduct(order.OrderId, p.ProductId);
        }

        /// <summary>
        /// Cancels current customers shopping
        /// clears cart and order
        /// </summary>
        public void CancelOrder()
        {
            CurrentCustomer.Cart = new List<Product>();
            CurrentCustomer.Order = new Order();
        }

        /// <summary>
        /// Initializes current customers past orders
        /// </summary>
        public void InitializePreviousStoreOrders()
        {
            CurrentCustomer.PastOrders = _dbContext.GetOrders(CurrentCustomer.CustomerId, CurrentStore.StoreId);
        }

        /// <summary>
        /// Closes database connection
        /// </summary>
        public void Exit()
        {
            _dbContext.CloseDataBaseConnection();
        }
    }
}
