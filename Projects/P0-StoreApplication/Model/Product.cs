using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Must have a name, price, and description
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public double Price { get; set; }

        public Product(int productId, string name, string description, double price)
        {
            this.ProductId = productId;
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }
    }
}
