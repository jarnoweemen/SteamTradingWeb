using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceDal.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public long SteamId { get; private set; }
        public string UserName { get; private set; }
        public string ProfilePic { get; private set; }
        public bool IsBot { get; private set; } = false;
        public bool IsAdmin { get; private set; } = false;
        public DateTime CreatedAt { get; } = DateTime.Now;  

        public UserDto(int id, long steamId, string userName, string profilePic)
        {
            Id = id;
            SteamId = steamId;
            UserName = userName;
            ProfilePic = profilePic;
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
    }
}
