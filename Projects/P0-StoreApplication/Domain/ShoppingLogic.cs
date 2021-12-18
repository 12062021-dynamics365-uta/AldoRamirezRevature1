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
        private List<Customer> customers;
        private Customer currentLoggedInCustomer;
        public Store CurrentStore { get; set; }

        public ShoppingLogic()
        {
            customers = new List<Customer>();
            this._dbContext = new DataBaseAccess();
        }

        public ShoppingLogic(string fName, string lName)
        {
            this.customers = new List<Customer>();
        }

        public void login(string userFname, string userLname)
        {
            Customer customer = customers.Where(c => c.Fname.Equals(userFname) && c.Lname.Equals(userLname)).FirstOrDefault();
            currentLoggedInCustomer = customer;

            if (customer == null)
            {
                Customer newCustomer = new Customer(userFname, userLname);
                currentLoggedInCustomer = newCustomer;
                customers.Add(newCustomer);
            }

            Console.WriteLine("\nCustomers\n-----------------------------------------");
            _dbContext.getCustomers();
            Console.WriteLine("\nStores\n-----------------------------------------");
            _dbContext.getStores();
            Console.WriteLine("\nBest Buy Products\n-----------------------------------------");
            _dbContext.getStoreProducts(1);
            _dbContext.closeDataBaseConnection();

        }

        public int validateStoreChoice(String userInput)
        {
            int convertedNumber = 0;
            bool conversionBool = Int32.TryParse(userInput, out convertedNumber);

            if (!conversionBool || convertedNumber < 1 || convertedNumber > currentLoggedInCustomer.StoreLocations.Count)
                return 0;

            return convertedNumber;
        }
    }
}
