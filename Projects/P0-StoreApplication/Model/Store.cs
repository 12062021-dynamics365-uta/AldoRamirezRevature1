using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Must be able to view past sales
    /// Must be able to view sales by store location
    /// [stretch goal] Must be able to manage product inventory(add, edit, delete any product)
    /// </summary>
    public class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public Store()
        {
            Products = new List<Product>();
        }
    }
}
