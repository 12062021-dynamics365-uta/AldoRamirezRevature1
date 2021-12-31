using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Model;

namespace Storage
{
    public class DataBaseAccess : IDataBaseAccess
    {
        string connectionStr = "Data source = ALDITONE-DESKTO\\SQLEXPRESS; initial Catalog=P0-StoreApplication; integrated security = true";
        private readonly SqlConnection connection;
        private readonly IMapper mapper;

        public DataBaseAccess(IMapper mapper)
        {
            this.connection = new SqlConnection(connectionStr);
            this.connection.Open();
            this.mapper = mapper;
        }

        /// <summary>
        /// Searches database for customer with equivalent
        /// UserName and Password
        /// </summary>
        /// <param name="userName">Users username</param>
        /// <param name="password">Users password</param>
        /// <returns>Customer</returns>
        public Customer GetCustomer(string userName, string password)
        {
            string queryString = ($"SELECT * FROM Customers WHERE UserName = '{userName}' AND UserPassword = '{password}';");

            Customer customer;
            using (SqlCommand cmd = new SqlCommand(queryString, this.connection))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                customer = this.mapper.EntityToCustomer(dr);
                dr.Close();
            }
            return customer;
        }

        /// <summary>
        /// Gets List of stores from database
        /// </summary>
        /// <returns>List<Store></Store></returns>
        public List<Store> GetStores()
        {
            string queryString = "SELECT * FROM Stores;";
            List<Store> stores = new List<Store>();
            using (SqlCommand cmd = new SqlCommand(queryString, this.connection))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                stores = this.mapper.EntityToStoreList(dr);
                dr.Close();
            }
            return stores;
        }

        /// <summary>
        /// Gets products from store
        /// </summary>
        /// <param name="storeId">Stores ID</param>
        /// <returns></returns>
        public List<Product> GetStoreProducts(int storeId)
        {
            string queryString = "SELECT ProductId, ProductName, ProductAmount, ProductDesc FROM Products WHERE StoreId = " + storeId;
            List<Product> products = new List<Product>();
            using (SqlCommand cmd = new SqlCommand(queryString, this.connection))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                products = this.mapper.EntityToProductList(dr);
                dr.Close();
            }
            return products;
        }

        /// <summary>
        /// Gets customers orders from database
        /// </summary>
        /// <param name="customerId">Customers unique Id</param>
        /// <param name="storeId">Stores unique Id</param>
        /// <returns></returns>
        public List<Order> GetOrders(int customerId, int storeId)
        {
            string queryString = "SELECT o.OrderId, p.StoreId, p.ProductId, p.ProductName, p.ProductDesc, p.ProductAmount, o.TotalAmount " +
                "FROM Products p " +
                "LEFT JOIN OrderProduct op " +
                "ON op.ProductId = p.ProductId " +
                "LEFT JOIN Orders o " +
                "ON op.OrderId = o.OrderId " +
                "WHERE o.CustomerId = " + customerId + " AND p.StoreId = " + storeId;

            List<Order> orders = new List<Order>();
            using (SqlCommand cmd = new SqlCommand(queryString, this.connection))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                orders = this.mapper.EntityToOrderList(dr);
                dr.Close();
            }
            return orders;
        }

        /// <summary>
        /// Adds a customer to the database
        /// </summary>
        /// <param name="firstName">Users first name</param>
        /// <param name="lastName">Users last name</param>
        /// <param name="userName">Users username</param>
        /// <param name="password">Users password</param>
        public int AddCustomer(string fName, string lName, string uName, string password)
        {
            int newId;
            string queryString = ($"INSERT INTO Customers (FirstName, LastName, UserName, UserPassword) OUTPUT INSERTED.CustomerId VALUES ('{fName}', '{lName}', '{uName}', '{password}');");

            using (SqlCommand cmd = new SqlCommand(queryString, this.connection))
            {
                newId = (int)cmd.ExecuteScalar();
                return newId;
            }
        }

        /// <summary>
        /// Adds new order to the database
        /// </summary>
        /// <param name="customerId">Customers unique Id</param>
        /// <param name="totalAmount">Order total amount</param>
        /// <returns>New Order unique Id</returns>
        public int AddOrder(int customerId, decimal totalAmount)
        {
            int newId;
            string queryString = ($"INSERT INTO Orders (CustomerId, TotalAmount) OUTPUT INSERTED.OrderId VALUES ({customerId}, {(decimal)totalAmount});");

            using (SqlCommand cmd = new SqlCommand(queryString, this.connection))
            {
                newId = (int)cmd.ExecuteScalar();
                return newId;
            }
        }

        /// <summary>
        /// Adds order and product id to junction table in database
        /// </summary>
        /// <param name="orderId">Order unique Id</param>
        /// <param name="productId">Product unique Id</param>
        public void AddOrderProduct(int orderId, int productId)
        {
            string queryString = ($"INSERT INTO OrderProduct (OrderId, ProductId) VALUES ({orderId}, {productId});");

            using (SqlCommand cmd = new SqlCommand(queryString, this.connection))
            {
                cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Closes database connection
        /// </summary>
        public void CloseDataBaseConnection()
        {
            this.connection.Close();
        }
    }
}
