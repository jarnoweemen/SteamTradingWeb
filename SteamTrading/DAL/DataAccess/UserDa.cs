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
    public class UserDa : IUserDal
    {
        private readonly ApplicationDbContext _dbContext;

        public UserDa(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateUser(UserDto user)
        {
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
        public bool CheckIfUserExists(long steamId)
        {
            try
            {
                if (_dbContext.Users.Any(u => u.SteamId == steamId))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
