using DAL.Context;
using FactoryDal.Factory;
using InterfaceDal.Dto;
using InterfaceDal.Interface;
using Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Container
{
    public class UserContainer
    {
        private readonly IUserDal _iUserDal;

        public UserContainer(ApplicationDbContext dbContext)
        {
            FUserDa fUserDa = new(dbContext);
            _iUserDal = fUserDa.GetUserDa();
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns true if the user was made succesfully.</returns>
        public bool CreateUser(UserModel user)
        {
            UserDto userDto = new(user.SteamId, user.UserName, user.ProfilePic, user.CreatedAt);

            if (!_iUserDal.CheckIfUserExists(user.SteamId))
            {
                if (_iUserDal.CreateUser(userDto).Result) return true;
            }

            return false;
        }
    }
}
