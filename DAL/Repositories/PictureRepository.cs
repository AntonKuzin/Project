﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.IO;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.Interface.IRepositories;
using DAL.Interface.Models;
using ORM;

namespace DAL.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly ProjectDbEntities1 _context = new ProjectDbEntities1();

        public IEnumerable<DalPicture> GetAllPictures()
        {
            return _context.Pictures.Select(
                picture => new DalPicture() 
                    {
                        Id = picture.Id,
                        Description = picture.Description,
                        Name = picture.Name,
                        Url = picture.Url,
                        UserId = picture.UserId,
                        Likes = picture.Likes,
                        User = picture.Users
                    }
                );
        }

        public IEnumerable<DalPicture> GetUserPictures(int id)
        {
            return _context.Users.Single(j => j.Id == id).Pictures.Select(u => u.ToDalPicture());
        }

        public bool CreatePicture(IEnumerable<HttpPostedFileBase> fileUpload, string name, string description,
            string email)
        {
            var picture = new ORM.Pictures();
            foreach (var item in fileUpload)
            {
                if (item == null) continue;
                string pathToOriginals = HttpContext.Current.Server.MapPath("~/Pictures/Originals");
                string pathToSmall = HttpContext.Current.Server.MapPath("~/Pictures/Small");
                string filename = item.FileName;
                if (filename != null)
                {
                    Random rnd = new Random();
                    int temp = rnd.Next(10000);
                    filename = temp + filename;
                    item.SaveAs(Path.Combine(pathToOriginals, filename));
                    item.SaveAs(Path.Combine(pathToSmall, filename));
                    picture.Url = filename;
                    picture.UserId = _context.Users.Single(u => u.Email == email).Id;
                    picture.Name = name;
                    picture.Description = description;
                    _context.Pictures.Add(picture);
                    _context.SaveChanges();
                }
            }
            return true;
        }

        public bool UpdatePicture(DalPicture picture)
        {
            if (picture == null)
                return false;
            if (GetCurrentUserId() == picture.UserId)
            {
                _context.Entry(picture.ToPictures()).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemovePicture(int id)
        {
            var picture = _context.Pictures.FirstOrDefault(u => u.Id == id);
            if (picture != null)
                if (GetCurrentUserId() == picture.UserId)
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~/Pictures/Originals/") + picture.Url);
                    File.Delete(HttpContext.Current.Server.MapPath("~/Pictures/Small/") + picture.Url);
                    _context.Pictures.Remove(picture);
                    _context.SaveChanges();
                    return true;
                }
            return false;
        }

        public bool CreateLike(int id, int currentUserId)
        {
            if (id == 0)
                return false;
            var temp =
                (from n in _context.Likes
                 where n.PictureId == id && n.UserId == currentUserId
                 select n)
                .FirstOrDefault();

            if (temp == null)
            {
                temp = new ORM.Likes() { Like = true, PictureId = id, UserId = currentUserId };
                _context.Likes.Add(temp);
            }
            else
            {
                if (temp.Like == true)
                    _context.Likes.Remove(temp);
                else
                {
                    temp.Like = true;
                    _context.Likes.AddOrUpdate(temp);
                }
            }

            _context.SaveChanges();

            return true;
        }

        public bool CreateDislike(int id, int currentUserId)
        {
            if (id == 0)
                return false;
            var temp =
                (from n in _context.Likes
                 where n.PictureId == id && n.UserId == currentUserId
                 select n)
                .FirstOrDefault();

            if (temp == null)
            {
                temp = new ORM.Likes() { Like = false, PictureId = id, UserId = currentUserId };
                _context.Likes.Add(temp);
            }
            else
            {
                if (temp.Like == false)
                    _context.Likes.Remove(temp);
                else
                {
                    temp.Like = false;
                    _context.Likes.AddOrUpdate(temp);
                }
            }

            _context.SaveChanges();

            return true;
        }

        public DalPicture FindPicture(int id)
        {
            if (id != 0)
                return _context.Pictures.Find(id).ToDalPicture();
            return null;
                
        }

        public int GetCurrentUserId()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
                return _context.Users.First(u => u.Email == HttpContext.Current.User.Identity.Name).Id;
            return 0;
        }
    }
}
