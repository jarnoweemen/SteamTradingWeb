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
        public int Id { get; set; } 
        [Required]
        public string SkinId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        public CartItemDto(string skinId, string name, decimal price, int id = 0)
        {
            Id = id;
            SkinId = skinId;
            Name = name;
            Price = price;  
        }
    }
}
