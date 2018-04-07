using CrimeDetectionClass.Models.Crime;
using CrimeDetectionClass.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrimeDetectionClass.Controllers
{
    public class MyReportsController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            string CurrentUser = System.Web.HttpContext.Current.User.Identity.Name;
            CrimeDataLayer objCrimeDataLayer = new CrimeDataLayer();
            List<CrimeModel> listCrime = objCrimeDataLayer.GetCrimeDetailsByUserId(UserDataLayer.GetUserIdByEmail(CurrentUser));
            return View(listCrime);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ViewStatus(int CrimeId)
        {
            CrimeDataLayer objCrimeDataLayer = new CrimeDataLayer();
            CrimeModel objCrimeModel = objCrimeDataLayer.GetCrimeDetailsbyCrimeId(CrimeId);
            return View(objCrimeModel);
        }
        
    }
}