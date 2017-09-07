using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PsdH5ShowWebApp.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(int id)
        {
            ViewBag.errormsg = Request["msg"];
            return View();
        }
    }
}