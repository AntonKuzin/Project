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
        bool CreatePicture(IEnumerable<HttpPostedFileBase> fileUpload, string name, string description, string email);
        bool UpdatePicture(DalPicture picture);
        bool RemovePicture(int id);
        bool CreateLike(int id, int currentUserId);
        bool CreateDislike(int id, int currentUserId);
        DalPicture FindPicture(int id);
        int GetCurrentUserId();
    }
}
