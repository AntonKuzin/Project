using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetAllUsers();
        bool CreateUser(Users user);
        bool UpdateUser(Users user);
        bool RemoveUser(int id);
    }
}