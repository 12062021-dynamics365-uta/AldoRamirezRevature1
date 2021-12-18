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
            this._dbContext = new DataBaseAccess();
            this.customers = _dbContext.getCustomers();
        }

        public ShoppingLogic(string fName, string lName)
        {
            this.customers = _dbContext.getCustomers();
        }

        public void login(string userFname, string userLname)
        {
            Customer customer = customers.Where(c => c.Fname.Equals(userFname) && c.Lname.Equals(userLname)).FirstOrDefault();
            currentLoggedInCustomer = customer;

            if (customer == null)
            {
                int tempId = _dbContext.addCustomer(userFname, userLname);
                Customer newCustomer = new Customer(tempId, userFname, userLname);
                currentLoggedInCustomer = newCustomer;
                customers.Add(newCustomer);
            }

            //temporary testing getting customer orders
            currentLoggedInCustomer.PastOrders = _dbContext.getOrders(1, 3);
            foreach (Order o in currentLoggedInCustomer.PastOrders)
            {
                Console.WriteLine($"Order Number: {o.OrderId} - Total: ${o.TotalCost}");
                foreach (Product p in o.Products)
                    Console.WriteLine($"{p.ProductId}: {p.Name} - {p.Description} = ${p.Price}");
            }
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

        public void setPreviousOrders(int storeId)
        {
            
        }
    }
}
