using CrimeDetectionClass.Models.Crime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrimeDetectionClass.Controllers
{
    public class WomenController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }

               [HttpPost]
        public ActionResult Index(FormCollection frmCollection)
        {
            string Crime = frmCollection["Crime"];

            CrimeModel objCrimeModel = new CrimeModel();

            objCrimeModel.Crime = frmCollection["Crime"];
            objCrimeModel.Description = frmCollection["Description"];
            objCrimeModel.Criminal = frmCollection["CriminalName"];
            objCrimeModel.Location = frmCollection["Location"];
            CrimeDataLayer objDatalayer = new CrimeDataLayer();
            bool Response = objDatalayer.SaveCrime(objCrimeModel,1);
            ViewBag.Message = "ok";

            return View();
    
        }
    }
}