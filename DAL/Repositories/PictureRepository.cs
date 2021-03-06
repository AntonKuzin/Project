﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
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
        private readonly DbContext _context;

        public PictureRepository(DbContext uow)
        {
            _context = uow;
        }

        public IEnumerable<DalPicture> GetAllPictures()
        {
            if(_context == null)
                throw new NullReferenceException("repository");
            return _context.Set<Pictures>().Select(u => new DalPicture()
            {
                Id = u.Id,
                Description = u.Description,
                Likes = u.Likes,
                Name = u.Name,
                BinaryData = u.BinaryData,
                User = u.Users,
                UserId = u.UserId,
                Extension = u.Extension
            }
                );
        }

        public IEnumerable<DalPicture> GetUserPictures(int id)
        {
            if (_context == null)
                throw new NullReferenceException("repository");
            return _context.Set<Users>().Single(j => j.Id == id).Pictures.Select(u => u.ToDalPicture());
        }

        public void CreatePicture(DalPicture picture, string email)
        {
            if (_context == null)
                throw new NullReferenceException("repository");
            picture.UserId = _context.Set<Users>().Single(u => u.Email == email).Id;
            _context.Set<Pictures>().Add(picture.ToPictures());
            _context.SaveChanges();

        }

        public void UpdatePicture(DalPicture picture)
        {
            if (_context == null)
                throw new NullReferenceException("repository");
            if (picture == null)
                return ;
            if (GetCurrentUserId() == picture.UserId)
            {
                _context.Entry(picture.ToPictures()).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void RemovePicture(int id)
        {
            if (_context == null)
                throw new NullReferenceException("repository");
            var picture = _context.Set<Pictures>().FirstOrDefault(u => u.Id == id);
            if (picture != null)
                if (GetCurrentUserId() == picture.UserId)
                {
                    _context.Set<Pictures>().Remove(picture);
                    _context.SaveChanges();
                }
        }

        public void CreateLike(int id, int currentUserId)
        {
            if (_context == null)
                throw new NullReferenceException("repository");
            if (id == 0)
                return;
            var temp =
                (from n in _context.Set<Likes>()
                 where n.PictureId == id && n.UserId == currentUserId
                 select n)
                .FirstOrDefault();

            if (temp == null)
            {
                temp = new ORM.Likes() { Like = true, PictureId = id, UserId = currentUserId };
                _context.Set<Likes>().Add(temp);
            }
            else
            {
                if (temp.Like == true)
                    _context.Set<Likes>().Remove(temp);
                else
                {
                    temp.Like = true;
                    _context.Set<Likes>().AddOrUpdate(temp);
                }
            }

            _context.SaveChanges();
        }

        public void CreateDislike(int id, int currentUserId)
        {
            if (_context == null)
                throw new NullReferenceException("repository");
            if (id == 0)
                return;
            var temp =
                (from n in _context.Set<Likes>()
                 where n.PictureId == id && n.UserId == currentUserId
                 select n)
                .FirstOrDefault();

            if (temp == null)
            {
                temp = new ORM.Likes() { Like = false, PictureId = id, UserId = currentUserId };
                _context.Set<Likes>().Add(temp);
            }
            else
            {
                if (temp.Like == false)
                    _context.Set<Likes>().Remove(temp);
                else
                {
                    temp.Like = false;
                    _context.Set<Likes>().AddOrUpdate(temp);
                }
            }

            _context.SaveChanges();

        }

        public DalPicture FindPicture(int id)
        {
            if (_context == null)
                throw new NullReferenceException("repository");
            if (id != 0)
                return _context.Set<Pictures>().Find(id).ToDalPicture();
            return null;
                
        }

        public int GetCurrentUserId()
        {
            if (_context == null)
                throw new NullReferenceException("repository");
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var temp = _context.Set<Users>().FirstOrDefault(u => u.Email == HttpContext.Current.User.Identity.Name);
                if (temp != null)
                    return temp.Id;
            }
            return 0;
        }


        public IEnumerable<DalPicture> GetPagePictures(int page, int pageItems)
        {
            if (_context == null)
                throw new NullReferenceException("repository");
            return _context.Set<Pictures>()
                .OrderBy(u => u.Id)
                .Skip(page * pageItems)
                .Take(pageItems)
                .Select(u => new DalPicture()
                {
                    Id = u.Id,
                    Description = u.Description,
                    Likes = u.Likes,
                    Name = u.Name,
                    BinaryData = u.BinaryData,
                    User = u.Users,
                    UserId = u.UserId,
                    Extension = u.Extension
                }
                );
        }


        public IEnumerable<DalPicture> GetUserPagePictures(int page, int pageItems, int userId)
        {
            if (_context == null)
                throw new NullReferenceException("repository");
            return _context.Set<Pictures>()
                .Where(u => u.UserId == userId)
                .OrderBy(u => u.Id)
                .Skip(page * pageItems)
                .Take(pageItems)
                .Select(u => new DalPicture()
                {
                    Id = u.Id,
                    Description = u.Description,
                    Likes = u.Likes,
                    Name = u.Name,
                    BinaryData = u.BinaryData,
                    User = u.Users,
                    UserId = u.UserId,
                    Extension = u.Extension
                }
                );
        }
    }
}
