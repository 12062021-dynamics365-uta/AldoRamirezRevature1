using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Storage
{
    public class Mapper
    {
        public Customer EntityToCustomer(SqlDataReader dr)
        {
            Customer customer = null;

            while (dr.Read())
            {
                customer = new Customer()
                {
                    CustomerId = dr.GetInt32(0),
                    Fname = dr.GetString(1),
                    Lname = dr.GetString(2)
                };
            }
            return customer;
        }

        public List<Store> EntityToStoreList(SqlDataReader dr)
        {
            List<Store> stores = new List<Store>();

            while (dr.Read())
            {
                Store s = new Store()
                {
                    StoreId = dr.GetInt32(0), 
                    Name = dr.GetString(1)
                };
                stores.Add(s);
            }
            return stores;
        }

        public List<Product> EntityToProductList(SqlDataReader dr)
        {
            List<Product> products = new List<Product>();

            while (dr.Read())
            {
                Product p = new Product()
                {
                    ProductId = dr.GetInt32(0),
                    Name = dr.GetString(1),
                    Description = dr.GetString(3),
                    Price = (double)dr.GetDecimal(2)
                };
                products.Add(p);
            }
            return products;
        }

        public List<Order> EntityToOrderList(SqlDataReader dr)
        {
            List<Order> orders = new List<Order>();

            while (dr.Read())
            {
                Order order = new Order()
                {
                    OrderId = dr.GetInt32(0),
                    TotalCost = (double)dr.GetDecimal(6)
                };

                if (!orders.Exists(x => x.OrderId == dr.GetInt32(0)))
                    orders.Add(order);
                foreach (Order o in orders)
                {
                    if (o.OrderId == dr.GetInt32(0))
                    {
                        Product p = new Product()
                        {
                            ProductId = dr.GetInt32(2),
                            Name = dr.GetString(3),
                            Description = dr.GetString(4),
                            Price = (double)dr.GetDecimal(5)
                        };
                        o.Products.Add(p);
                    }
                }
            }
            return orders;
        }
    }
}
