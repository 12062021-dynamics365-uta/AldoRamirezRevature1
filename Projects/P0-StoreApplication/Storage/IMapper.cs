using Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Storage
{
    public interface IMapper
    {
        Customer EntityToCustomer(SqlDataReader dr);
        List<Order> EntityToOrderList(SqlDataReader dr);
        List<Product> EntityToProductList(SqlDataReader dr);
        List<Store> EntityToStoreList(SqlDataReader dr);
    }
}