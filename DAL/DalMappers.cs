using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Models;
using ORM;

namespace DAL
{
    public static class DalMappers
    {
        public static Likes ToLike(this DalLike dalLike)
        {
            return new Likes()
            {
                PictureId = dalLike.PictureId,
                Like = dalLike.Like,
                UserId = dalLike.UserId,
                Pictures = dalLike.Picture,
                Users = dalLike.User
            };
        }

        public static DalLike ToDalLike(this Likes like)
        {
            return new DalLike()
            {
                PictureId = like.PictureId,
                Like = like.Like,
                UserId = like.UserId,
                Picture = like.Pictures,
                User = like.Users
            };
        }

        public static Users ToUser(this DalUser dalUser)
        {
            return new Users()
            {
                Id = dalUser.Id,
                Email = dalUser.Email,
                Name = dalUser.Name,
                Password = dalUser.Password,
                Likes = dalUser.Likes,
                Pictures = dalUser.Pictures,
                Roles = dalUser.Roles
            };
        }

        public static DalUser ToDalUser(this Users user)
        {
            return new DalUser()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Likes = user.Likes,
                Pictures = user.Pictures,
                Roles = user.Roles
            };
        }

        public static Roles ToRoles(this DalRole dalRole)
        {
            return new Roles()
            {
                Id = dalRole.Id,
                Name = dalRole.Name,
                Users = dalRole.Users
            };
        }

        public static DalRole ToDalRole(this Roles role)
        {
            return new DalRole()
            {
                Id = role.Id,
                Name = role.Name,
                Users = role.Users
            };
        }

        public static Pictures ToPictures(this DalPicture dalPicture)
        {
            return new Pictures()
            {
                Id = dalPicture.Id,
                Description = dalPicture.Description,
                Name = dalPicture.Name,
                BinaryData = dalPicture.BinaryData,
                UserId = dalPicture.UserId,
                Likes = dalPicture.Likes,
                Users = dalPicture.User,
                Extension = dalPicture.Extension
            };
        }

        public static DalPicture ToDalPicture(this Pictures picture)
        {
            return new DalPicture()
            {
                Id = picture.Id,
                Description = picture.Description,
                Name = picture.Name,
                BinaryData = picture.BinaryData,
                UserId = picture.UserId,
                Likes = picture.Likes,
                User = picture.Users,
                Extension = picture.Extension
            };
        }


    }
}
