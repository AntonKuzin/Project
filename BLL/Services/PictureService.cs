using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Data.Entity.Migrations;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BLL.Interface.Services;
using BLL.Interface.Models;
using BLL.Mappers;
using DAL.Interface.IRepositories;
using ImageResizer;

namespace BLL.Services
{
    public class PictureService : IPictureService
    {
        private readonly IUnitOfWork _uow;
        private readonly IPictureRepository _pictureRepository;

        public PictureService(IPictureRepository repository, IUnitOfWork uow)
        {
            _pictureRepository = repository;
            _uow = uow;
        }

        public IEnumerable<BllPicture> GetAllPictures()
        {

            if (_pictureRepository == null)
                throw new NullReferenceException("service");
            return _pictureRepository.GetAllPictures().Select(picture => picture.ToBllPicture());
            
           
        }

        public IEnumerable<BllPicture> GetUserPictures(int id)
        {
            if (_pictureRepository == null)
                throw new NullReferenceException("service");
             return _pictureRepository.GetUserPictures(id).Select(picture => picture.ToBllPicture());
        }

        public void CreatePicture(HttpPostedFileBase fileUpload, string name, string description, string userEmail)
        {

            if (_pictureRepository == null)
                throw new NullReferenceException("service");
            // получаем расширение
            var ext = fileUpload.FileName.Substring(fileUpload.FileName.LastIndexOf(".") + 1);
            byte[] fileData;
            using (var binaryReader = new BinaryReader(fileUpload.InputStream))
            {
                fileData = binaryReader.ReadBytes(fileUpload.ContentLength);
            }
            var picture = new BllPicture()
            {
                Name = name,
                Description = description,
                BinaryData = fileData,
                Extension = ext
            };
            _pictureRepository.CreatePicture(picture.ToDalPicture(), userEmail);
            //_uow.Commit();
        }

        public void UpdatePicture(BllPicture picture)
        {
            if (_pictureRepository == null)
                throw new NullReferenceException("service");
            if (picture == null)
                throw new ArgumentNullException("picture_service");
            if (GetCurrentUserId() == picture.UserId)
            {
                _pictureRepository.UpdatePicture(picture.ToDalPicture());
                //_uow.Commit();
            }
        }

        public void RemovePicture(int id)
        {
            if (_pictureRepository == null)
                throw new NullReferenceException("service");
            if (id != 0)
            {
                _pictureRepository.RemovePicture(id);
                //_uow.Commit();
            }
        }

        public void CreateLike(int id, int currentUserId)
        {
            if (_pictureRepository == null)
                throw new NullReferenceException("service");
            _pictureRepository.CreateLike(id, currentUserId);
            //_uow.Commit();
        }

        public void CreateDislike(int id, int currentUserId)
        {
            if (_pictureRepository == null)
                throw new NullReferenceException("service");
            _pictureRepository.CreateDislike(id, currentUserId);
            //_uow.Commit();
        }

        public BllPicture FindPicture(int id)
        {
            if (_pictureRepository == null)
                throw new NullReferenceException("service");
            return _pictureRepository.FindPicture(id).ToBllPicture();
        }

        public int GetCurrentUserId()
        {
            if (_pictureRepository == null)
                throw new NullReferenceException("service");

            return _pictureRepository.GetCurrentUserId();
            
        }




        public IEnumerable<BllPicture> GetPagePictures(int page, int pageItems)
        {
            if (_pictureRepository == null)
                throw new NullReferenceException("service");
            return _pictureRepository.GetPagePictures(page, pageItems).Select(u => u.ToBllPicture());
        }


        public IEnumerable<BllPicture> GetUserPagePictures(int page, int pageItems, int userId)
        {
            if (_pictureRepository == null)
                throw new NullReferenceException("service");

            return _pictureRepository.GetUserPagePictures(page, pageItems, userId).Select(u => u.ToBllPicture());

            
        }
    }
}
