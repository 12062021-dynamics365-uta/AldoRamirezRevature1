using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P0_StoreApplication
{
    class ShoppingLogic
    {
        private List<Customer> customers;
        public Store CurrentStore { get; set; }
        public Customer CurrentCustomer { get; set; }

        public ShoppingLogic()
        {
            customers = new List<Customer>();
        }

        public ShoppingLogic(string fName, string lName)
        {
            customers = new List<Customer>();

        }

        internal void login(string userFname, string userLname)
        {
            Customer c = customers.Where(c => c.Fname.Equals(userFname) && c.Lname.Equals(userLname)).FirstOrDefault();
            CurrentCustomer = c;

            if (c == null)
            {
                Customer c1 = new Customer(userFname, userLname);
                CurrentCustomer = c1;
                customers.Add(c1);
            }

            initializeStores();
        }

        internal void initializeStores()
        {
            //TODO: retrieve stores from database

            CurrentCustomer.StoreLocations.Add(new Store("Best Buy"));
            CurrentCustomer.StoreLocations.Add(new Store("Kroger"));
            CurrentCustomer.StoreLocations.Add(new Store("The Home Depot"));
            CurrentCustomer.StoreLocations.Add(new Store("Kohl's"));
        }

        internal int validateStoreChoice(String userInput)
        {
            int convertedNumber = 0;
            bool conversionBool = Int32.TryParse(userInput, out convertedNumber);

            if (!conversionBool || convertedNumber < 1 || convertedNumber > CurrentCustomer.StoreLocations.Count)
                return 0;

            return convertedNumber;
        }

        internal void selectStore(int i)
        {
            CurrentStore = CurrentCustomer.StoreLocations.ElementAt(i - 1);
        }


    }
}
