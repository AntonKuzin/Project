using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using BLL.Interface.Services;
using test.Filters;
using test.Infrastructure;
using test.ViewModels;
using ORM;

namespace test.Controllers
{
    [AlbumAuthorize(Roles = "user, admin")]
    public class PictureController : Controller
    {
        private readonly IPictureService _repository;
        int pageItems = 4;

        public PictureController(IPictureService repository)
        {
            _repository = repository;
        }

        public ActionResult Index(int? id)
        {
            int pageItems = 4;
            int page = id ?? 0;
            int userId = _repository.GetCurrentUserId();
            var model = _repository
                .GetPagePictures(page, pageItems)
                .Select(u => u.ToMvcPicture(userId));
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Pictures", _repository
                    .GetPagePictures(page, pageItems)
                    .Select(u => u.ToMvcPicture(userId))
                    );
            }
            return View(model);
        }

        //
        // GET: /Picture/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Picture/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                String name = Request.Form["PictureName"];
                String description = Request.Form["PictureDescription"];
                
                _repository.CreatePicture(fileUpload, name, description, User.Identity.Name);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Picture/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ORM.Pictures pictures = _repository.FindPicture(id).ToPicture();
            if (pictures == null)
            {
                return HttpNotFound();
            }
            return View(pictures);
        }

        //
        //POST: /Picture/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ORM.Pictures pictures)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdatePicture(pictures.ToBllPicture());
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Произошла ошибка!");
            return View(pictures);
        }

        //
        // GET: /Picture/Delete/5

        public ActionResult Delete(int id = 0)
        {
            return View();
        }

        //
        // POST: /Picture/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.RemovePicture(id);
                return RedirectToAction("Index","Home");
        }


        public PartialViewResult Like(int id)
        {
            int currentUserId = _repository.GetCurrentUserId();
            _repository.CreateLike(id, currentUserId);
            var temp = _repository.FindPicture(id);
            var model = new PictureViewModel()
            {
                Id = temp.Id,
                Name = temp.Name,
                Like = temp.Likes.SingleOrDefault(j => j.UserId == currentUserId && j.PictureId == temp.Id),
                Url = ""
            };

            return PartialView("_Buttons", model);
        }

        public PartialViewResult Dislike(int id)
        {
            int currentUserId = _repository.GetCurrentUserId();
            _repository.CreateDislike(id, currentUserId);
            var temp = _repository.FindPicture(id);
            var model = new PictureViewModel()
            {
                Id = temp.Id,
                Name = temp.Name,
                Like = temp.Likes.SingleOrDefault(j => j.UserId == currentUserId && j.PictureId == temp.Id),
                Url = ""
            };

            return PartialView("_Buttons", model);

        }

        [AllowAnonymous]
        public ActionResult Image(int id)
        {
            var picture = _repository.FindPicture(id).ToPicture();
            MemoryStream ms = new MemoryStream(picture.BinaryData);
  
            var ci = new Bitmap(System.Drawing.Image.FromStream(ms));

            var versions = new Dictionary<string, ImageFormat>();
            versions.Add("jpg", ImageFormat.Jpeg);
            versions.Add("jpeg", ImageFormat.Jpeg);
            versions.Add("png", ImageFormat.Png);
            versions.Add("bmp", ImageFormat.Bmp);
            versions.Add("gif", ImageFormat.Gif);

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/" + picture.Extension;

            // Write the image to the response stream in JPEG format.
            ci.Save(this.Response.OutputStream, versions[picture.Extension]);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
            return null;
        }

        [AllowAnonymous]
        public ActionResult ShowPhoto(int id)
        {
            int currentUserId = _repository.GetCurrentUserId();
            var temp = _repository.FindPicture(id);
            var model = new PictureViewModel()
            {
                Id = temp.Id,
                Name = temp.Name,
                Like = temp.Likes.SingleOrDefault(j => j.UserId == currentUserId && j.PictureId == temp.Id),
                Url = "",
                UserEmail = temp.User.Email
            };

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult UserPictures(int userId)
        {
            ViewBag.userId = userId;
            int currentUserId = _repository.GetCurrentUserId();
            var model = _repository.GetUserPagePictures(0, pageItems, userId).Select(u => u.ToMvcPicture(currentUserId)) ;
            return View(model);
        }

        [AllowAnonymous]
        public PartialViewResult GetPagePictures(int? page, int userId)
        {
            int temp = page ?? 1;
            int currentUserId = _repository.GetCurrentUserId();
            var model = _repository
                .GetUserPagePictures(temp, pageItems, userId)
                .Select(u => u.ToMvcPicture(currentUserId));
            return PartialView("_Pictures", model);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}