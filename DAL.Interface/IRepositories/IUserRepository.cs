using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Models;

namespace DAL.Interface.IRepositories
{
    public interface IUserRepository
    {
        IEnumerable<DalUser> GetAllUsers();
        bool CreateUser(DalUser user);
        bool UpdateUser(DalUser user);
        bool RemoveUser(int id);
        bool BanUser(int id);
        bool UnBanUser(int id);
    }
}
