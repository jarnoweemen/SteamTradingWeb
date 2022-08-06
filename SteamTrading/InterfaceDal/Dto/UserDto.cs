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
        public bool IsBot { get; private set; }
        [Required]
        public bool IsAdmin { get; private set; }
        [Required]
        public DateTime CreatedAt { get; private set; }

        public UserDto(long steamId, string userName, string profilePic, DateTime createdAt, int id = 0, bool isBot = false, bool isAdmin = false)
        {
            Id = id;
            SteamId = steamId;
            UserName = userName;
            ProfilePic = profilePic;
            IsBot = isBot;
            IsAdmin = isAdmin;
            CreatedAt = createdAt;
        }
    }
}
