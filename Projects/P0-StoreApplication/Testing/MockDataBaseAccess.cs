using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Storage;

namespace Testing
{
    class MockDataBaseAccess : IDataBaseAccess
    {
        public Customer GetCustomer(string userName, string password)
        {
            Customer customer = null;

            if (userName.Equals("Alditone") && password.Equals("12345"))
                customer = new Customer(1, "Aldo", "Ramirez");

            return customer;
        }

        public List<Store> GetStores()
        {
            List<Store> stores = new List<Store>();

            Store bestBuy = new Store(1, "Store1");
            stores.Add(bestBuy);

            Store kroger = new Store(2, "Store2");
            stores.Add(kroger);

            return stores;
        }

        public int AddCustomer(string fName, string lName, string uName, string password)
        {
            return 1;
        }

        public List<Product> GetStoreProducts(int storeId)
        {
            List<Product> products = new List<Product>();

            Product prod1 = new Product()
            {
                ProductId = 1,
                Name = "Product 1",
                Description = "Test Product 1",
                Price = 1
            };

            Product prod2 = new Product()
            {
                ProductId = 2,
                Name = "Product 2",
                Description = "Test Product 2",
                Price = 2
            };

            products.Add(prod1);
            products.Add(prod2);

            return products;
        }

        public int AddOrder(int customerId, decimal totalAmount)
        {
            return 1;
        }

        public void AddOrderProduct(int orderId, int productId)
        {
            
        }

        public void CloseDataBaseConnection()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrders(int customerId, int storeId)
        {
            List<Order> orders = new List<Order>();
            List<Product> products = new List<Product>();

            Product prod1 = new Product()
            {
                ProductId = 1,
                Name = "Product 1",
                Description = "Test Product 1",
                Price = 1
            };
            products.Add(prod1);

            Product prod2 = new Product()
            {
                ProductId = 2,
                Name = "Product 2",
                Description = "Test Product 2",
                Price = 2
            };
            products.Add(prod2);

            Order order1 = new Order()
            {
                OrderId = 1,
                TotalCost = 3,
                Products = products
            };
            orders.Add(order1);

            return orders;
        }
    }
}
