using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P0_StoreApplication
{
    /// <summary>
    /// Must be able to view past purchases
    /// Must be able to view available store locations
    /// Must be able to purchase 1 or more products
    /// Must be able to view cuurrent cart
    /// Must be able to checkout
    /// Must be able to cancel a purchase
    /// </summary>
    class Customer
    {
        Order order;
        public string Fname { get; set; }
        public string Lname { get; set; }
        public List<Product> Cart { get; set; }
        public List<Store> StoreLocations { get; set; }
        public List<Order> PastOrders { get; set; }

        public Customer(string fName, string lName)
        {
            this.Fname = fName;
            this.Lname = lName;
            order = new Order();
            Cart = new List<Product>();
            StoreLocations = new List<Store>();
            PastOrders = new List<Order>();
        }
    }
}
