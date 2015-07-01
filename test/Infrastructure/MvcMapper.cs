﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using BLL.Interface.Models;
using ORM;
using test.ViewModels;

namespace test.Infrastructure
{
    public static class MvcMapper
    {
        public static ORM.Pictures ToPicture(this BllPicture bllPicture)
        {
            return new ORM.Pictures()
            {
                Id = bllPicture.Id,
                Description = bllPicture.Description,
                Name = bllPicture.Name,
                Url = bllPicture.Url,
                UserId = bllPicture.UserId,
                Likes = bllPicture.Likes,
                Users = bllPicture.User
            };
        }

        public static BllPicture ToBllPicture(this ORM.Pictures picture)
        {
            return new BllPicture()
            {
                Id = picture.Id,
                Description = picture.Description,
                Name = picture.Name,
                Url = picture.Url,
                UserId = picture.UserId,
                Likes = picture.Likes,
                User = picture.Users
            };
        }

        public static PictureViewModel ToMvcPicture(this BllPicture bllPicture, int currentUserId)
        {
            return new PictureViewModel()
            {
                Id = bllPicture.Id,
                Description = bllPicture.Description,
                Like = bllPicture.Likes.SingleOrDefault(j => j.UserId == currentUserId && j.PictureId == bllPicture.Id),
                Name = bllPicture.Name,
                Rating = bllPicture.Likes.Count(j => j.Like == true) - bllPicture.Likes.Count(j => j.Like == false),
                Url = bllPicture.Url,
                UserEmail = bllPicture.User.Email,
                UserId = bllPicture.UserId
            };
        }
    }
}