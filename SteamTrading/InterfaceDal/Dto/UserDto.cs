using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceDal.Dto
{
    public class UserDto
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public long SteamId { get; private set; }
        [Required]
        public string UserName { get; private set; }
        [Required]
        public string ProfilePic { get; private set; }
        [Required]
        public bool IsBot { get; private set; } = false;
        [Required]
        public bool IsAdmin { get; private set; } = false;
        [Required]
        public DateTime CreatedAt { get; private set; } = DateTime.Now; 

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
