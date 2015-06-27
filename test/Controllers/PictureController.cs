using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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

        public PictureController(IPictureService repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var model = from u in _repository.GetAllPictures()
                        select new PictureViewModel
                        {
                            Description = u.Description,
                            Name = u.Name,
                            Url = u.Url,
                            Id = u.Id
                        };
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
        public ActionResult Create(IEnumerable<HttpPostedFileBase> fileUpload)
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
                if(_repository.UpdatePicture(pictures.ToBllPicture()))
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
            if (_repository.RemovePicture(id))
                return RedirectToAction("Index","Home");
            ModelState.AddModelError("", "Произошла ошибка!");
                return View();
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
                Url = temp.Url
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
                Url = temp.Url
            };

            return PartialView("_Buttons", model);

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
                Url = temp.Url,
                UserEmail = temp.User.Email
            };

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult UserPictures(int id)
        {
            int currentUserId = _repository.GetCurrentUserId();
            var model = from u in _repository.GetUserPictures(id)
                        select new PictureViewModel
                        {
                            Description = u.Description,
                            Name = u.Name,
                            Url = u.Url,
                            Id = u.Id,
                            Like = u.Likes.SingleOrDefault(j => j.UserId == currentUserId && j.PictureId == u.Id),
                            UserId = u.UserId,
                            UserEmail = u.User.Email,
                            Rating =
                                u.Likes.Count(j => j.Like == true) - u.Likes.Count(j => j.Like == false)
                        };
            return View(model);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}