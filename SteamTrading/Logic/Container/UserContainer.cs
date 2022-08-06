using IntefaceLogic.Model;
using InterfaceDal.Dto;
using InterfaceDal.Interface;
using InterfaceLogic.Inteface;

namespace Logic.Container
{
    public class UserContainer : IUserContainer
    {
        private readonly IUserDal _userDal;

        public UserContainer(IUserDal userDal)
        {
            _userDal = userDal;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns true if the user was made succesfully.</returns>
        public bool CreateUser(UserModel user)
        {
            UserDto userDto = new(user.SteamId, user.UserName, user.ProfilePic, user.CreatedAt);

            if (!user.CheckIfUserExists(user.SteamId))
            {
                if (_userDal.CreateUser(userDto).Result) return true;
            }

            return false;
        }
    }
}
