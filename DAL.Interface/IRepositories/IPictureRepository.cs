using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.Interface.Models;

namespace DAL.Interface.IRepositories
{
    public interface IPictureRepository
    {
        IEnumerable<DalPicture> GetAllPictures();
        IEnumerable<DalPicture> GetUserPictures(int id);
        void CreatePicture(string url, string name, string description, string email);
        void UpdatePicture(DalPicture picture);
        void RemovePicture(int id);
        void CreateLike(int id, int currentUserId);
        void CreateDislike(int id, int currentUserId);
        DalPicture FindPicture(int id);
        int GetCurrentUserId();
        IEnumerable<DalPicture> GetPagePictures(int page, int pageItems);
        IEnumerable<DalPicture> GetUserPagePictures(int page, int pageItems, int userId);
    }
}
