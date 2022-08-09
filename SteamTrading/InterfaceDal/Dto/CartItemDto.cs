using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceDal.Dto
{
    public class CartItemDto
    {
        [Key]
        public string? Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        public CartItemDto(string name, decimal price)
        {
            Name = name;
            Price = price;  
        }
    }
}
