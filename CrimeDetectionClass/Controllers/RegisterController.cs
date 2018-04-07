using CrimeDetectionClass.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrimeDetectionClass.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection frmCollection)
        {
            UserModel objUserModel = new UserModel
            {
                EmailId = frmCollection["EmailId"].ToString(),
                Password = frmCollection["Password"].ToString(),
                ContactNo = frmCollection["contact"].ToString(),
                District = frmCollection["dist"].ToString()
            };

            UserDataLayer objData = new UserDataLayer();

            try
            {
                ViewBag.Status = "success";
                ViewBag.Message = objData.UserRegistration(objUserModel);
                ViewBag.Message = "You are successfully registered to crime detection";
            }
            catch (Exception)
            {

                ViewBag.Status = "error";
            }
     

           
            
            return View();
        }
    }
}