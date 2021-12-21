﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Model;

namespace Storage
{
    public class DataBaseAccess
    {
        string connectionStr = "Data source = ALDITONE-DESKTO\\SQLEXPRESS; initial Catalog=P0-StoreApplication; integrated security = true";
        private readonly SqlConnection connection;

        public DataBaseAccess()
        {
            this.connection = new SqlConnection(connectionStr);
            this.connection.Open();
        }

        public Customer getCustomer(string userName, string password)
        {
            string queryString = "SELECT * FROM Customers;";
            SqlCommand cmd = new SqlCommand(queryString, this.connection);
            SqlDataReader dr = cmd.ExecuteReader();

            Customer c = null;

            while (dr.Read())
            {
                if (dr.GetString(3) == userName && dr.GetString(4) == password)
                    c = new Customer(dr.GetInt32(0), dr.GetString(1), dr.GetString(2));
            }
            dr.Close();

            return c;
        }

        public List<Store> getStores()
        {
            //TODO: Return Stores from database
            string queryString = "SELECT * FROM Stores;";
            SqlCommand cmd = new SqlCommand(queryString, this.connection);
            SqlDataReader dr = cmd.ExecuteReader();

            List<Store> stores = new List<Store>();
            
            while (dr.Read())
                stores.Add(new Store(dr.GetInt32(0), dr.GetString(1)));
            dr.Close();

            return stores;
        }

        public List<Product> getStoreProducts(int storeId)
        {
            //TODO: Return Products per store from database
            string queryString = "SELECT ProductId, ProductName, ProductAmount, ProductDesc FROM Products WHERE StoreId = " + storeId;
            SqlCommand cmd = new SqlCommand(queryString, this.connection);
            SqlDataReader dr = cmd.ExecuteReader();

            //Only for testing purposes
            while (dr.Read())
                Console.WriteLine($"{dr.GetInt32(0)}: {dr.GetString(1)} - Price:${dr.GetSqlMoney(2)}\nDescription: {dr.GetString(3)}\n");
            dr.Close();

            return null;
        }

        public List<Order> getOrders(int customerId, int storeId)
        {
            string queryString = "SELECT o.OrderId, p.StoreId, p.ProductId, p.ProductName, p.ProductDesc, p.ProductAmount, o.TotalAmount " +
                "FROM Products p " +
                "LEFT JOIN OrderProduct op " +
                "ON op.ProductId = p.ProductId " +
                "LEFT JOIN Orders o " +
                "ON op.OrderId = o.OrderId " +
                "WHERE o.CustomerId = " + customerId + " AND p.StoreId = " + storeId;
            SqlCommand cmd = new SqlCommand(queryString, this.connection);
            SqlDataReader dr = cmd.ExecuteReader();

            List<Order> orders = new List<Order>();
            Order order;
            while (dr.Read())
            {
                order = new Order();
                order.OrderId = dr.GetInt32(0);
                order.TotalCost = (double)dr.GetDecimal(6);
                if (!orders.Exists(x => x.OrderId == dr.GetInt32(0)))
                    orders.Add(order);
                
                foreach(Order o in orders)
                {
                    if (o.OrderId == dr.GetInt32(0))
                        o.Products.Add(new Product(dr.GetInt32(2), dr.GetString(3), dr.GetString(4), (double)dr.GetDecimal(5)));
                }
            }
            dr.Close();
            return orders;
        }
        
        public int addCustomer(string fName, string lName, string uName, string password)
        {
            int newId = 0;
            string queryString = ($"INSERT INTO Customers (FirstName, LastName, UserName, UserPassword) OUTPUT INSERTED.CustomerId VALUES ('{fName}', '{lName}', '{uName}', '{password}');");

            using (SqlCommand cmd = new SqlCommand(queryString, this.connection))
            {
                newId = (int)cmd.ExecuteScalar();
                return newId;
            }
        }

        public void addOrder(int customerId, int totalAmount)
        {
            //TODO: Add new order to database
        }

        public void addOrderProduct(int OrderId, int productId)
        {
            //TODO: Add orderId and productId to OrderProduct junction table
        }

        public void closeDataBaseConnection()
        {
            this.connection.Close();
        }
    }
}