using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectDbEntities _context = new ProjectDbEntities();

        public IEnumerable<Users> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public bool CreateUser(Users user)
        {
            if (user.Id != 0) return false;
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateUser(Users user)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUser(int id)
        {
            Users user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
