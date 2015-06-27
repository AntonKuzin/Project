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
        bool CreatePicture(IEnumerable<HttpPostedFileBase> fileUpload, string name, string description, string userEmail);
        bool UpdatePicture(BllPicture picture);
        bool RemovePicture(int id);
        bool CreateLike(int id, int currentUserId);
        bool CreateDislike(int id, int currentUserId);
        BllPicture FindPicture(int id);
        int GetCurrentUserId();
    }
}
