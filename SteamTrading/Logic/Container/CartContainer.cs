using InterfaceDal.Dto;
using InterfaceDal.Interface;
using InterfaceLogic.Inteface;
using InterfaceLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Container
{
    public class CartContainer : ICartContainer
    {
        private readonly ICartDa _cartDa;

        public CartContainer(ICartDa cartDa)
        {
            _cartDa = cartDa;
        }

        public bool AddToCart(CartItemModel item)
        {
            if (item.Name == null) return false;

            CartItemDto itemDto = new(item.Name, item.Price);

            // 
            // Check if skin is already in someones cart.
            //
            if (_cartDa.AddToCart(itemDto).Result) return true;

            return false;
        }
    }
}
