using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrimeDetectionClass.Controllers
{
    public class LoginController : Controller
    {
          [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}