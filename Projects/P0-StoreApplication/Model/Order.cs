using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Must be able to compute its total cost
    /// Must be able to contain at least 1 product
    /// Must be able to limit its content to no more than 50 items
    /// Must be able to limit its total cost to no more than $500
    /// </summary>
    public class Order
    {
        public int OrderId { get; set; }
        public List<Product> Products { get; set; }
        public double TotalCost { get; set; }

        public Order ()
        {
            this.OrderId = 0;
            this.Products = new List<Product>();
            this.TotalCost = 0.0;
        }

        public Order (int orderId, List<Product> products, double totalCost)
        {
            this.OrderId = orderId;
            this.Products = products;
            this.TotalCost = totalCost;
        }
    }
}
