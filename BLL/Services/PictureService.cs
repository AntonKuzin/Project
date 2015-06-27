using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Data.Entity.Migrations;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BLL.Interface.Services;
using BLL.Interface.Models;
using BLL.Mappers;
using DAL.Interface.IRepositories;

namespace BLL.Services
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;

        public PictureService(IPictureRepository repository)
        {
            _pictureRepository = repository;
        }

        public IEnumerable<BllPicture> GetAllPictures()
        {
            return _pictureRepository.GetAllPictures().Select(picture => picture.ToBllPicture());
        }

        public IEnumerable<BllPicture> GetUserPictures(int id)
        {
            return _pictureRepository.GetUserPictures(id).Select(picture => picture.ToBllPicture());
        }

        public bool CreatePicture(IEnumerable<HttpPostedFileBase> fileUpload, string name, string description, string userEmail)
        {
            return _pictureRepository.CreatePicture(fileUpload, name, description, userEmail);
        }

        public bool UpdatePicture(BllPicture picture)
        {
            if (picture == null)
                return false;
            if (GetCurrentUserId() == picture.UserId)
            {
                return _pictureRepository.UpdatePicture(picture.ToDalPicture());
            }
            return false;
        }

        public bool RemovePicture(int id)
        {
            return _pictureRepository.RemovePicture(id);
        }

        public bool CreateLike(int id, int currentUserId)
        {
            return _pictureRepository.CreateLike(id, currentUserId);
        }

        public bool CreateDislike(int id, int currentUserId)
        {
            return _pictureRepository.CreateDislike(id, currentUserId);
        }

        public BllPicture FindPicture(int id)
        {
            return _pictureRepository.FindPicture(id).ToBllPicture();
        }

        public int GetCurrentUserId()
        {
            return _pictureRepository.GetCurrentUserId();
        }
    }
}
