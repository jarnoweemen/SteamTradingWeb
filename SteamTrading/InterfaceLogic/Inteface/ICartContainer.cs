using InterfaceLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLogic.Inteface
{
    public interface ICartContainer
    {
        bool AddToCart(CartItemModel item);
    }
}
