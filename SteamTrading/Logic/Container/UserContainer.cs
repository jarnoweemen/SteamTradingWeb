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
            UserDto userDto = new(user.SteamId, user.UserName, user.ProfilePic, user.ProfileUrl, user.CreatedAt);

            if (!user.CheckIfUserExists(user.SteamId))
            {
                if (_userDal.CreateUser(userDto).Result) return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the user by SteamID.
        /// </summary>
        /// <param name="steamId"></param>
        /// <returns>Returns the user or null.</returns>
        public UserModel? GetUser(long steamId)
        {
            UserDto? userDto = _userDal.GetUser(steamId).Result;

            if (userDto == null)
            {
                return null;
            }
            else
            {
                return new(userDto.SteamId, userDto.UserName, userDto.ProfilePic, userDto.ProfileUrl, _userDal, userDto.Id, userDto.IsBot, userDto.IsAdmin);
            }
        }
    }
}
