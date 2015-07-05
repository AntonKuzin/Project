using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace test.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index(Exception exception)
        {
            ViewBag.Text = exception.Message;
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View();

        }

        public ActionResult NotFoundPage()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();

        }

    }
}
