using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace test.Models
{
    public interface IPictureRepository
    {
        IEnumerable<ORM.Pictures> GetAllPictures();
        IEnumerable<ORM.Pictures> GetUserPictures(int id);
        bool CreatePicture(IEnumerable<HttpPostedFileBase> fileUpload, string name, string description, string userEmail);
        bool UpdatePicture(ORM.Pictures picture);
        bool RemovePicture(int id);
        bool CreateLike(int id, int currentUserId);
        bool CreateDislike(int id, int currentUserId);
        ORM.Pictures FindPicture(int id);
        int GetCurrentUserId();
    }
}
