using System;

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
        public decimal Price { get; set; }

        public Product()
        {

        }
    }
}
