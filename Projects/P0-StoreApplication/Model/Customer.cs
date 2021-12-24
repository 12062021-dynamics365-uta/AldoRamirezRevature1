using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Must be able to view past purchases
    /// Must be able to view available store locations
    /// Must be able to purchase 1 or more products
    /// Must be able to view current cart
    /// Must be able to checkout
    /// Must be able to cancel a purchase
    /// </summary>
    public class Customer
    {
        public Order Order { get; set; }
        public int CustomerId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public List<Product> Cart { get; set; }
        public List<Store> StoreLocations { get; set; }
        public List<Order> PastOrders { get; set; }

        public Customer()
        {
            Order = new Order();
            Cart = new List<Product>();
            StoreLocations = new List<Store>();
            PastOrders = new List<Order>();
        }
        public Customer(int customerId, string fName, string lName)
        {
            this.CustomerId = customerId;
            this.Fname = fName;
            this.Lname = lName;
            Order = new Order();
            Cart = new List<Product>();
            StoreLocations = new List<Store>();
            PastOrders = new List<Order>();
        }
    }
}
