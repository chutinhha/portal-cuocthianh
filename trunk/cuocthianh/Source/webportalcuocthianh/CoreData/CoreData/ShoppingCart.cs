using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreData
{
    public class ShoppingCart
    {
        public int Qty { get; set; }
        public Product Product { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }
        public int Amount { get; set; }
    }
}
