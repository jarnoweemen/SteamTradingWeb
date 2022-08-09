using DAL.Context;
using InterfaceDal.Dto;
using InterfaceDal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class CartDa : ICartDa
    {
        private readonly ApplicationDbContext _dbContext;

        public CartDa(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddToCart(CartItemDto item)
        {
            try
            {
                _dbContext.CartItems.Add(item);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
