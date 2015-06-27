using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
//using test.Models;
using test.ViewModels;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPictureService _service;
        //
        // GET: /Home/
        public HomeController(IPictureService service)
        {
            this._service = service;
        }

        public ActionResult Index()
        {
            int id = _service.GetCurrentUserId();
            var model = (from u in _service.GetAllPictures()
                select new PictureViewModel
                {
                    Description = u.Description,
                    Name = u.Name,
                    Url = u.Url,
                    Id = u.Id,
                    Like = u.Likes.SingleOrDefault(j => j.UserId == id && j.PictureId == u.Id),
                    UserEmail = u.User.Email,
                    UserId = u.UserId,
                    Rating =
                        u.Likes.Count(j => j.Like == true) - u.Likes.Count(j => j.Like == false)
                })
                .OrderByDescending(u => u.Rating)
                .Take(12);
            return View(model);
 
        }

    }
}
