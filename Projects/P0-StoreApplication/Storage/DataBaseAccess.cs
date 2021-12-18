using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace Storage
{
    public class DataBaseAccess
    {
        string connectionStr = "Data source = DESKTOP-TMS5341\\SQLEXPRESS; initial Catalog=P0-StoreApplication; integrated security = true";
        private readonly SqlConnection connection;

        public DataBaseAccess()
        {
            this.connection = new SqlConnection(connectionStr);
            this.connection.Open();
        }

        public void getCustomers()
        {
            //TODO: Return Customers from database
            string queryString = "SELECT FirstName, LastName FROM Customers;";
            SqlCommand cmd = new SqlCommand(queryString, this.connection);
            SqlDataReader dr = cmd.ExecuteReader();

            //Only for testing purposes
            while (dr.Read())
                Console.WriteLine($"{dr.GetString(0)} {dr.GetString(1)}");
            dr.Close();
        }

        public void getStores()
        {
            //TODO: Return Stores from database
            string queryString = "SELECT * FROM Stores;";
            SqlCommand cmd = new SqlCommand(queryString, this.connection);
            SqlDataReader dr = cmd.ExecuteReader();

            //Only for testing purposes
            while (dr.Read())
                Console.WriteLine($"{dr.GetInt32(0)}: {dr.GetString(1)}");
            dr.Close();
        }

        public void getStoreProducts(int storeId)
        {
            //TODO: Return Products per store from database
            string queryString = "SELECT ProductId, ProductName, ProductAmount, ProductDesc FROM Products WHERE StoreId = " + storeId;
            SqlCommand cmd = new SqlCommand(queryString, this.connection);
            SqlDataReader dr = cmd.ExecuteReader();

            //Only for testing purposes
            while (dr.Read())
                Console.WriteLine($"{dr.GetInt32(0)}: {dr.GetString(1)} - Price:${dr.GetSqlMoney(2)}\nDescription: {dr.GetString(3)}\n");
            dr.Close();
        }

        public void getOrders()
        {
            //TODO: Return Order id from database
        }

        public void getOrderProducts()
        {
            //TODO: Return Order products per order id from database
        }
        
        public void addCustomer()
        {
            //TODO: Add new customer to database
        }

        public void addOrder()
        {
            //TODO: Add new order to database
        }

        public void closeDataBaseConnection()
        {
            this.connection.Close();
        }
    }
}
