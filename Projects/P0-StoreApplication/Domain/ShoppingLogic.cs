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
        //private List<Customer> customers;
        public Customer CurrentCustomer { get; set; }
        public Store CurrentStore { get; set; }

        public ShoppingLogic()
        {
            this._dbContext = new DataBaseAccess();
        }

        public int validateMainMenuChoice(string userInput)
        {
            int convertedNumber = 0;
            bool conversionBool = Int32.TryParse(userInput, out convertedNumber);

            if (!conversionBool || convertedNumber < 1 || convertedNumber > 3)
                convertedNumber = 0;

            return convertedNumber;
        }

        public bool login(string userName, string password)
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
            /*Customer customer = customers.Where(c => c.Fname.Equals(userFname) && c.Lname.Equals(userLname)).FirstOrDefault();
            currentLoggedInCustomer = customer;

            if (customer == null)
            {
                int tempId = _dbContext.addCustomer(userFname, userLname);
                Customer newCustomer = new Customer(tempId, userFname, userLname);
                currentLoggedInCustomer = newCustomer;
                customers.Add(newCustomer);
                Console.WriteLine(newCustomer.CustomerId);
            }
            currentLoggedInCustomer.StoreLocations = _dbContext.getStores();*/

        }
        public void register(string firstName, string lastName, string userName, string password)
        {
            int id = _dbContext.addCustomer(firstName, lastName, userName, password);
            CurrentCustomer = new Customer(id, firstName, lastName);
            CurrentCustomer.StoreLocations = _dbContext.getStores();
        }

        public List<Store> getListOfStores()
        {
            return CurrentCustomer.StoreLocations;
        }

        public List<Order> getListOfOrders()
        {
            return CurrentCustomer.PastOrders;
        }

        public int validateStoreChoice(string userInput)
        {
            int convertedNumber = 0;
            bool conversionBool = Int32.TryParse(userInput, out convertedNumber);

            if (!conversionBool || convertedNumber < 1 || convertedNumber > CurrentCustomer.StoreLocations.Count + 1)
                convertedNumber = 0;
            else if (convertedNumber != CurrentCustomer.StoreLocations.Count + 1)
            {
                CurrentStore = CurrentCustomer.StoreLocations.Find(x => x.StoreId == convertedNumber);
                initializePreviousStoreOrders();
            }

            return convertedNumber;
        }

        public void initializePreviousStoreOrders()
        {
            CurrentCustomer.PastOrders = _dbContext.getOrders(CurrentCustomer.CustomerId, CurrentStore.StoreId);
        }

        public void exit()
        {
            _dbContext.closeDataBaseConnection();
        }
    }
}
