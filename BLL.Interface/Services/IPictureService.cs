using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BLL.Interface.Models;


namespace BLL.Interface.Services
{
    public interface IPictureService
    {
        IEnumerable<BllPicture> GetAllPictures();
        IEnumerable<BllPicture> GetUserPictures(int id);
        void CreatePicture(HttpPostedFileBase fileUpload, string name, string description, string userEmail);
        void UpdatePicture(BllPicture picture);
        void RemovePicture(int id);
        void CreateLike(int id, int currentUserId);
        void CreateDislike(int id, int currentUserId);
        BllPicture FindPicture(int id);
        int GetCurrentUserId();
        IEnumerable<BllPicture> GetPagePictures(int page, int pageItems);
        IEnumerable<BllPicture> GetUserPagePictures(int page, int pageItems, int userId);
    }
}
