using CrimeDetectionClass.Models.Crime;
using CrimeDetectionClass.Models.SMTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CrimeDetectionClass.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Status = "Nil";
            return View("HomeLand");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(FormCollection frmCollection)
        {
            string Crime = frmCollection["Crime"];

            CrimeModel objCrimeModel = new CrimeModel();

            objCrimeModel.Crime = frmCollection["Crime"];
            objCrimeModel.Description = frmCollection["Description"];
            objCrimeModel.Criminal = frmCollection["CriminalName"];
            objCrimeModel.Location = frmCollection["Location"];
            CrimeDataLayer objDatalayer = new CrimeDataLayer();
            bool Response = objDatalayer.SaveCrime(objCrimeModel, 0);
            //string PoliceEmail = WebConfigurationManager.AppSettings["Trivandrum"].ToString();
            //string Message =string.Format("This is to notify that a crime has been registered \n Details :{0} \n Name:{1}",objCrimeModel.Description,"username");
            //SMTPProtocol.NotifyPolice(string.Format("Crime Registered {0}", DateTime.Now.ToShortDateString()), Message, PoliceEmail);

            ViewBag.Status = "success";
            ViewBag.Message = "Your crime details has been successfully sent to the department";

            return View();
        }
    }
}