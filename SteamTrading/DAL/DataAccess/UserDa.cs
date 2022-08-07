using DAL.Context;
using InterfaceDal.Dto;
using InterfaceDal.Interface;
using Microsoft.EntityFrameworkCore;
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

        public async Task<UserDto?> GetUser(long steamID)
        {
            try
            {
                UserDto userDto = await _dbContext.Users.Where(u => u.SteamId == steamID).SingleAsync();

                return userDto;
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }

            return null;
        }
    }
}
