using DAL.Context;
using FactoryDal.Factory;
using InterfaceDal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model
{
    public class UserModel
    {
        private readonly IUserDal _iUserDal;

        public int Id { get; private set; }
        public long SteamId { get; private set; }
        public string UserName { get; private set; }
        public string ProfilePic { get; private set; }
        public bool IsBot { get; private set; } = false;
        public bool IsAdmin { get; private set; } = false;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public UserModel(long steamId, string userName, string profilePic, ApplicationDbContext dbContext, int id = 0)
        {
            SteamId = steamId;
            UserName = userName;
            ProfilePic = profilePic;

            FUserDa fUserDa = new(dbContext);
            _iUserDal = fUserDa.GetUserDa();
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
            if (_iUserDal.CheckIfUserExists(steamId))
            {
                return true;
            }

            return false;
        }
    }
}
