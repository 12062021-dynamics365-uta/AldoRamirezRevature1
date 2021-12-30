using Model;
using System.Collections.Generic;

namespace Storage
{
    public interface IDataBaseAccess
    {
        int AddCustomer(string fName, string lName, string uName, string password);
        int AddOrder(int customerId, decimal totalAmount);
        void AddOrderProduct(int orderId, int productId);
        void CloseDataBaseConnection();
        Customer GetCustomer(string userName, string password);
        List<Order> GetOrders(int customerId, int storeId);
        List<Product> GetStoreProducts(int storeId);
        List<Store> GetStores();
    }
}