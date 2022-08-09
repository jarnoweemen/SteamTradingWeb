using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLogic.Model
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public string SkinId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public CartItemModel(string skinId, string name, decimal price, int id = 0)
        {
            Id = id; 
            SkinId = skinId;
            Name = name;
            Price = price;  
        }
    }
}
