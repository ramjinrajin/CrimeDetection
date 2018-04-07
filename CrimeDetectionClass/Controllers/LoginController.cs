using CrimeDetectionClass.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
        public ActionResult Index(FormCollection objForm)
        {
            UserDataLayer objUserDataLayer = new UserDataLayer();
            bool isChecked = false;
            if (Request.Form["remember"] != null)
            {
                isChecked = true;
            }

            if (objUserDataLayer.AuthenticateUser(Request.Form["USer name"].ToString(), Request.Form["password"].ToString()))
            {
                FormsAuthentication.SetAuthCookie(Request.Form["USer name"].ToString(), isChecked);
                Session["IsPolice"] = objUserDataLayer.GetIsPoliceStatus(Request.Form["USer name"].ToString());
                if (Request.QueryString["returnUrl"] != "" && Request.QueryString["returnUrl"] != null)
                {
                    string sdf = Request.QueryString["returnUrl"];
                    return Redirect(Request.QueryString["returnUrl"]);
                }
                FormsAuthentication.RedirectFromLoginPage(Request.Form["USer name"].ToString(), isChecked);
                ViewBag.UserName = System.Web.HttpContext.Current.User.Identity.Name;
                return RedirectToAction("index", "Home");
            }

            else
            {


                ViewBag.Status = "unauthorized";
                ViewBag.Message = "Your crime details has been successfully sent to the department";
                return View();
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["IsPolice"] = false;
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}