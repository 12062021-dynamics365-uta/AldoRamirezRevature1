using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P0_StoreApplication
{
    /// <summary>
    /// Must be able to view past sales
    /// Must be able to view sales by store location
    /// [stretch goal] Must be able to manage product inventory(add, edit, delete any product)
    /// </summary>
    class Store
    {
        public string Location { get; set; }
        public List<Product> Products { get; set; }

        public Store(string location)
        {
            this.Location = location;
            Products = new List<Product>();
        }
    }
}
