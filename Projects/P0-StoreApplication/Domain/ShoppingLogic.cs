using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Storage;

namespace Domain
{
    public class ShoppingLogic
    {
        private readonly DataBaseAccess _dbContext;
        public Customer CurrentCustomer { get; set; }
        public Store CurrentStore { get; set; }

        public ShoppingLogic()
        {
            this._dbContext = new DataBaseAccess();
        }

        public bool Login(string userName, string password)
        {
            Customer customer = _dbContext.getCustomer(userName, password);
            
            if (customer != null)
            {
                customer.StoreLocations = _dbContext.getStores();
                CurrentCustomer = customer;
                return true;
            }
            else
            {
                CurrentCustomer = null;
                return false;
            }
        }

        public void Register(string firstName, string lastName, string userName, string password)
        {
            int id = _dbContext.addCustomer(firstName, lastName, userName, password);
            CurrentCustomer = new Customer(id, firstName, lastName);
            CurrentCustomer.StoreLocations = _dbContext.getStores();
        }

        public int ValidateMainMenuChoice(string userInput)
        {
            int userChoice = ConvertInputToInt(userInput);
            int numOfChoices = 3;

            if (userChoice > numOfChoices)
                userChoice = 0;

            return userChoice;
        }

        public int ValidateStoreChoice(string userInput)
        {
            int userChoice = ConvertInputToInt(userInput);
            int numOfChoices = CurrentCustomer.StoreLocations.Count;

            if (userChoice > numOfChoices + 1)
                userChoice = 0;
            else if (userChoice != numOfChoices + 1)
            {
                CurrentStore = CurrentCustomer.StoreLocations.Find(x => x.StoreId == userChoice);
                CurrentStore.Products = _dbContext.getStoreProducts(CurrentStore.StoreId);
                InitializePreviousStoreOrders();
            }

            return userChoice;
        }

        public int ValidateStoreMenuChoice(string userInput)
        {
            int userChoice = ConvertInputToInt(userInput);
            int numOfChoices = 4;

            if (userChoice > numOfChoices)
                userChoice = 0;

            return userChoice;
        }

        public int ValidateShoppingMenuChoice(string userInput, int numOfChoices)
        {
            int userChoice = ConvertInputToInt(userInput);

            if (userChoice > numOfChoices)
                userChoice = 0;

            return userChoice;
        }

        public int ConvertInputToInt(string userInput)
        {
            bool conversionBool = Int32.TryParse(userInput, out int convertedNumber);

            if (!conversionBool)
                convertedNumber = 0;

            return convertedNumber;
        }

        public List<Order> GetListOfOrders()
        {
            return CurrentCustomer.PastOrders;
        }

        public bool AddProductToCart(Product product)
        {
            if (CurrentCustomer.Order.TotalCost + product.Price < 500.0)
            {
                CurrentCustomer.Cart.Add(product);
                CurrentCustomer.Order.Products.Add(product);
                CurrentCustomer.Order.TotalCost += product.Price;
                return true;
            }
            return false;
        }

        public void Checkout()
        {
            Order order = new Order();
            order.Products = CurrentCustomer.Cart;
            order.TotalCost = Math.Round(CurrentCustomer.Cart.Sum(p => p.Price), 2);
            CurrentCustomer.PastOrders.Add(order);
            order.OrderId = _dbContext.addOrder(CurrentCustomer.CustomerId, order.TotalCost);
            foreach (Product p in order.Products)
                _dbContext.addOrderProduct(order.OrderId, p.ProductId);
        }

        public void InitializePreviousStoreOrders()
        {
            CurrentCustomer.PastOrders = _dbContext.getOrders(CurrentCustomer.CustomerId, CurrentStore.StoreId);
        }

        public void Exit()
        {
            _dbContext.closeDataBaseConnection();
        }
    }
}
