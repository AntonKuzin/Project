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
            return _pictureRepository.GetAllPictures().Select(picture => picture.ToBllPicture());
        }

        public IEnumerable<BllPicture> GetUserPictures(int id)
        {
            return _pictureRepository.GetUserPictures(id).Select(picture => picture.ToBllPicture());
        }

        public void CreatePicture(HttpPostedFileBase fileUpload, string name, string description, string userEmail)
        {
            // получаем расширение
            var ext = fileUpload.FileName.Substring(fileUpload.FileName.LastIndexOf(".") + 1);
            if (ext == "jpeg") ext = "jpg";

            Random rnd = new Random();
            int temp = rnd.Next(10000);

            string[] paths =
            {
                HttpContext.Current.Server.MapPath("~/Pictures/Small/"),
                HttpContext.Current.Server.MapPath("~/Pictures/Originals/")
            };

            // создаем инструкции для различных версий
            var versions = new Dictionary<string, string>();
            versions.Add("small", "maxwidth=250&format=" + ext);
            versions.Add("original", "format=" + ext);

            int i = 0;
            foreach (var suffix in versions.Keys)
            {
                fileUpload.InputStream.Seek(0, SeekOrigin.Begin);

                ImageBuilder.Current.Build(
                    new ImageJob(
                        fileUpload.InputStream,
                        paths[i] + fileUpload.FileName.Remove(fileUpload.FileName.LastIndexOf(".")) + temp,
                        new Instructions(versions[suffix]),
                        false,
                        true));
                i++;
            }
            string url = fileUpload.FileName.Remove(fileUpload.FileName.LastIndexOf(".")) + temp +"." + ext;
            _pictureRepository.CreatePicture(url, name, description, userEmail);
            //_uow.Commit();
        }

        public void UpdatePicture(BllPicture picture)
        {
            if (picture == null)
                return;
            if (GetCurrentUserId() == picture.UserId)
            {
                _pictureRepository.UpdatePicture(picture.ToDalPicture());
                //_uow.Commit();
            }
        }

        public void RemovePicture(int id)
        {
            if (id != 0)
            {
                _pictureRepository.RemovePicture(id);
                //_uow.Commit();
            }
        }

        public void CreateLike(int id, int currentUserId)
        {
            _pictureRepository.CreateLike(id, currentUserId);
            //_uow.Commit();
        }

        public void CreateDislike(int id, int currentUserId)
        {
            _pictureRepository.CreateDislike(id, currentUserId);
            //_uow.Commit();
        }

        public BllPicture FindPicture(int id)
        {
            return _pictureRepository.FindPicture(id).ToBllPicture();
        }

        public int GetCurrentUserId()
        {
            return _pictureRepository.GetCurrentUserId();
        }




        public IEnumerable<BllPicture> GetPagePictures(int page, int pageItems)
        {
            return _pictureRepository.GetPagePictures(page, pageItems).Select(u => u.ToBllPicture());
        }


        public IEnumerable<BllPicture> GetUserPagePictures(int page, int pageItems, int userId)
        {
            return _pictureRepository.GetUserPagePictures(page, pageItems, userId).Select(u => u.ToBllPicture());
        }
    }
}
