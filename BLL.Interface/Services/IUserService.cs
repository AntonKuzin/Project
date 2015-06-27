using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Models;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        IEnumerable<BllUser> GetAllUsers();
        bool CreateUser(BllUser user);
        bool UpdateUser(BllUser user);
        bool RemoveUser(int id);
        bool BanUser(int id);
        bool UnBanUser(int id);
    }
}
