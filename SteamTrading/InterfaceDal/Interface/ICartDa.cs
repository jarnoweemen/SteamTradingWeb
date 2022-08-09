using InterfaceDal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceDal.Interface
{
    public interface ICartDa
    {
        Task<bool> AddToCart(CartItemDto item);
    }
}
