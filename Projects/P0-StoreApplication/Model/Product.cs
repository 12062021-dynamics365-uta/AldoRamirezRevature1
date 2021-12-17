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
        public String Name { get; set; }
        public double Price { get; set; }
        public String  Description { get; set; }

        public Product(string name, double price, string description)
        {
            this.Name = name;
            this.Price = price;
            this.Description = description;
        }
    }
}
