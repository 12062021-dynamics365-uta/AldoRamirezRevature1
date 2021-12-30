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
                CurrentCustomer = customer;
                InitializeStores();
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
            InitializeStores();
        }

        /// <summary>
        /// Initializes current customers store list
        /// </summary>
        public void InitializeStores()
        {
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

            if (userChoice > numOfChoices + 1 || userChoice == 0)
                userChoice = 0;
            else if (userChoice != numOfChoices + 1)
            {
                CurrentStore = CurrentCustomer.StoreLocations.Find(x => x.StoreId == userChoice);
                InitializeCurrentStoreProducts();
                InitializePreviousStoreOrders();
            }

            return userChoice;
        }

        /// <summary>
        /// Initializes the current store products
        /// </summary>
        public void InitializeCurrentStoreProducts()
        {
            CurrentStore.Products = _dbContext.GetStoreProducts(CurrentStore.StoreId);
        }

        /// <summary>
        /// Initializes current customers past orders
        /// </summary>
        public void InitializePreviousStoreOrders()
        {
            CurrentCustomer.PastOrders = _dbContext.GetOrders(CurrentCustomer.CustomerId, CurrentStore.StoreId);
        }

        /// <summary>
        /// Validates users choice in the Store Menu
        /// </summary>
        /// <param name="userInput">Users Input string</param>
        /// <returns>int</returns>
        public int ValidateStoreMenuChoice(string userInput)
        {
            int userChoice = ConvertInputToInt(userInput);
            int numOfChoices = CurrentCustomer.StoreLocations.Count;

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
            if (CurrentCustomer.Order.TotalCost + product.Price < 500 &&
                CurrentCustomer.Cart.Count < 50)
            {
                CurrentCustomer.Cart.Add(product);
                CurrentCustomer.Order.Products.Add(product);
                CurrentCustomer.Order.TotalCost += product.Price;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Uses linq to get products and their quantities from cart
        /// </summary>
        /// <returns>IEnumerable</returns>
        public IEnumerable<KeyValuePair<(string Name, decimal Price), int>> ConvertCartToIEnum()
        {
            var q = CurrentCustomer.Cart
                .GroupBy(x => (x.Name, x.Price))
                .Select(group => new KeyValuePair<(string Name, decimal Price), int>((group.Key.Name, group.Key.Price), group.Count()));

            return q;
        }

        /// <summary>
        /// Uses linq to get products and their quantities from past orders
        /// </summary>
        /// <param name="i">Past Orders index</param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<KeyValuePair<(string Name, decimal Price), int>> ConvertOrdersToIEnum(int i)
        {

            var q = CurrentCustomer.PastOrders[i].Products
                .GroupBy(x => (x.Name, x.Price))
                .Select(group => new KeyValuePair<(string Name, decimal Price), int>((group.Key.Name, group.Key.Price), group.Count()));

            return q;
        }

        /// <summary>
        /// Uses linq to get last order products and their quantities
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<(string Name, decimal Price), int>> ConvertSuccsessfullOrderToEnum()
        {
            var q = CurrentCustomer.PastOrders[^1].Products
                .GroupBy(x => (x.Name, x.Price))
                .Select(group => new KeyValuePair<(string Name, decimal Price), int>((group.Key.Name, group.Key.Price), group.Count()));

            return q;
        }
        
        /// <summary>
        /// Checkout current customer, creates new order
        /// adds it to current customer past orders list
        /// and adds it to the database
        /// </summary>
        public bool Checkout()
        {
            if (CurrentCustomer.Cart.Count != 0)
            {
                CurrentCustomer.Order.OrderId = _dbContext.AddOrder(CurrentCustomer.CustomerId, CurrentCustomer.Order.TotalCost);
                CurrentCustomer.PastOrders.Add(CurrentCustomer.Order);

                foreach (Product p in CurrentCustomer.Order.Products)
                    _dbContext.AddOrderProduct(CurrentCustomer.Order.OrderId, p.ProductId);

                CurrentCustomer.Cart = new List<Product>();
                CurrentCustomer.Order = new Order();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes item from cart and order
        /// </summary>
        /// <param name="product"></param>
        public void RemoveItemFromCart(Product product)
        {
            CurrentCustomer.Cart.Remove(product);
            CurrentCustomer.Order.Products.Remove(product);
            CurrentCustomer.Order.TotalCost -= product.Price;
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
        /// Closes database connection
        /// </summary>
        public void Exit()
        {
            _dbContext.CloseDataBaseConnection();
        }
    }
}
