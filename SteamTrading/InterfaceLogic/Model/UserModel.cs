using InterfaceDal.Interface;
using InterfaceLogic.Inteface;

namespace IntefaceLogic.Model
{
    public class UserModel
    {
        private readonly IUserDa _userDal;

        public int Id { get; private set; }
        public long SteamId { get; private set; }
        public string UserName { get; private set; }
        public string ProfilePic { get; private set; }
        public string ProfileUrl { get; private set; }  
        public bool IsBot { get; private set; } = false;
        public bool IsAdmin { get; private set; } = false;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public UserModel(long steamId, string userName, string profilePic, string profileUrl, IUserDa userDal, int id = 0, bool isBot = false, bool isAdmin = false)
        {
            SteamId = steamId;
            UserName = userName;
            ProfilePic = profilePic;
            ProfileUrl = profileUrl;
            _userDal = userDal;
        }

        /// <summary>
        /// Set an account as a bot account.
        /// </summary>
        /// <returns>Returns false if the account is already a bot or the acccount is an admin account.</returns>
        public bool SetBotAccount()
        {
            if (IsBot || IsAdmin) return false;

            IsBot = true;

            return true;
        }

        /// <summary>
        /// Set an account as a admin account.
        /// </summary>
        /// <returns>Returns false if the account is already an admin or the acccount is a bot account.</returns>
        public bool SetAdminAccount()
        {
            if (IsBot || IsAdmin) return false;

            IsAdmin = true;

            return true;
        }
        
        /// <summary>
        /// Check if the user already exists inside of the database via SteamID.
        /// </summary>
        /// <returns>Returns true if user exists.</returns>
        public bool CheckIfUserExists(long steamId)
        {
            if (_userDal.CheckIfUserExists(steamId))
            {
                return true;
            }

            return false;
        }
    }
}
