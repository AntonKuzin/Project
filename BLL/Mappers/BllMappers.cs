using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Models;
using DAL.Interface.Models;
using ORM;

namespace BLL.Mappers
{
    public static class BllMappers
    {
        public static DalUser ToDalUser(this BllUser bllUser)
        {
            return new DalUser()
            {
                Id = bllUser.Id,
                Name = bllUser.Name,
                Email = bllUser.Email,
                Password = bllUser.Password,
                Likes = bllUser.Likes,
                Pictures = bllUser.Pictures,
                Roles = bllUser.Roles
            };
        }

        public static BllUser ToBllUser(this DalUser dalUser)
        {
            return new BllUser()
            {
                Id = dalUser.Id,
                Name = dalUser.Name,
                Email = dalUser.Email,
                Password = dalUser.Password,
                Likes = dalUser.Likes,
                Pictures = dalUser.Pictures,
                Roles = dalUser.Roles
            };
        }

        public static DalLike ToDalLike(this BllLike bllLike)
        {
            return new DalLike()
            {
                Like = bllLike.Like,
                PictureId = bllLike.PictureId,
                UserId = bllLike.UserId,
                Picture = bllLike.Picture,
                User = bllLike.User
            };
        }

        public static BllLike ToBllLike(this DalLike dalLike)
        {
            return new BllLike()
            {
                Like = dalLike.Like,
                PictureId = dalLike.PictureId,
                UserId = dalLike.UserId,
                Picture = dalLike.Picture,
                User = dalLike.User
            };
        }

        public static DalPicture ToDalPicture(this BllPicture bllPicture)
        {
            return new DalPicture()
            {
                Id = bllPicture.Id,
                Description = bllPicture.Description,
                Name = bllPicture.Name,
                Url = bllPicture.Url,
                UserId = bllPicture.UserId,
                Likes = bllPicture.Likes,
                User = bllPicture.User
            };
        }

        public static BllPicture ToBllPicture(this DalPicture dalPicture)
        {
            return new BllPicture()
            {
                Id = dalPicture.Id,
                Description = dalPicture.Description,
                Name = dalPicture.Name,
                Url = dalPicture.Url,
                UserId = dalPicture.UserId,
                Likes = dalPicture.Likes,
                User = dalPicture.User
            };
        }

        public static DalRole ToDalRole(this BllRole bllRole)
        {
            return new DalRole()
            {
                Id = bllRole.Id,
                Name = bllRole.Name,
                Users = bllRole.Users
            };
        }

        public static BllRole ToBllRole(this DalRole dalRole)
        {
            return new BllRole()
            {
                Id = dalRole.Id,
                Name = dalRole.Name,
                Users = dalRole.Users
            };
        }
    }
}
