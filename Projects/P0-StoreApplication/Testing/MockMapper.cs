using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class MockMapper : IMapper
    {
        public Customer EntityToCustomer(SqlDataReader dr)
        {
            throw new NotImplementedException();
        }

        public List<Order> EntityToOrderList(SqlDataReader dr)
        {
            throw new NotImplementedException();
        }

        public List<Product> EntityToProductList(SqlDataReader dr)
        {
            throw new NotImplementedException();
        }

        public List<Store> EntityToStoreList(SqlDataReader dr)
        {
            throw new NotImplementedException();
        }
    }
}
