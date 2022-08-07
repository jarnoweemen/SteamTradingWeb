using IntefaceLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLogic.Inteface
{
    public interface IUserContainer
    {
        bool CreateUser(UserModel userModel);
        UserModel? GetUser(long steamId);
    }
}
