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
        }

        public void getStores()
        {
            //TODO: Return Stores from database
        }

        public void getStoreProducts()
        {
            //TODO: Return Products per store from database
        }

        public void getOrderProducts()
        {
            //TODO: Return Order products per order id from database
        }
    }
}
