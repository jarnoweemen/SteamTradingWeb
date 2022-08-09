using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLogic.Model
{
    public class CartItemModel
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }

        public CartItemModel(string name, decimal price)
        {
            Name = name;
            Price = price;  
        }
    }
}
