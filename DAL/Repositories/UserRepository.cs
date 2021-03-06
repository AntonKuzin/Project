﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.IRepositories;
using DAL.Interface.Models;
using ORM;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;

        public UserRepository(DbContext uow)
        {
            _context = uow;
        }

        public IEnumerable<DalUser> GetAllUsers()
        {
            return _context.Set<Users>().Select(u => new DalUser()
            {
                Id = u.Id,
                Email = u.Email,
                Likes = u.Likes,
                Name = u.Name,
                Password = u.Password,
                Pictures = u.Pictures,
                Roles = u.Roles
            }
            );
        }

        public bool CreateUser(DalUser user)
        {
            if (user.Id != 0) return false;
            _context.Set<Users>().Add(user.ToUser());
            _context.SaveChanges();
            return true;
        }

        public bool UpdateUser(DalUser user)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUser(int id)
        {
            Users user = _context.Set<Users>().FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Set<Users>().Remove(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }


        public bool BanUser(int id)
        {
            var role = _context.Set<Roles>().FirstOrDefault(r => r.Name == "banned");
            if (role != null)
            {
                var user = _context.Set<Users>().FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    if (!user.Roles.Contains(role))
                    {
                        user.Roles.Add(role);
                        _context.Set<Users>().AddOrUpdate(user);
                        _context.SaveChanges();
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool UnBanUser(int id)
        {
            var role = _context.Set<Roles>().FirstOrDefault(r => r.Name == "banned");
            if (role != null)
            {
                var user = _context.Set<Users>().FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    user.Roles.Remove(role);
                    _context.Set<Users>().AddOrUpdate(user);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
