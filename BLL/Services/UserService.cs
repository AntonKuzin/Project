using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Services;
using BLL.Interface.Models;
using BLL.Mappers;
using DAL.Interface.IRepositories;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public IEnumerable<BllUser> GetAllUsers()
        {
            return _userRepository.GetAllUsers().Select(u => u.ToBllUser());
        }

        public bool CreateUser(BllUser user)
        {
            return _userRepository.CreateUser(user.ToDalUser());
        }

        public bool UpdateUser(BllUser user)
        {
            return _userRepository.UpdateUser(user.ToDalUser());
        }

        public bool RemoveUser(int id)
        {
            return _userRepository.RemoveUser(id);
        }


        public bool BanUser(int id)
        {
            return _userRepository.BanUser(id);
        }


        public bool UnBanUser(int id)
        {
            return _userRepository.UnBanUser(id);
        }
    }
}
