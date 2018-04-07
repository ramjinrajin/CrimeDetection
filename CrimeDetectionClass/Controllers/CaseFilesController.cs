using CrimeDetectionClass.Models.Crime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrimeDetectionClass.Controllers
{
    public class CaseFilesController : Controller
    {
        [HttpGet]
        [Authorize]
        [IsUserPolice]
        public ActionResult Index()
        {
            CrimeDataLayer objCrimeDataLayer = new CrimeDataLayer();
            List<CrimeModel> listCrime = objCrimeDataLayer.GetCrimeDetails();
            return View(listCrime);
        }

        [HttpGet]
        [Authorize]
        [IsUserPolice]
        public ActionResult UpdateStatus(int CrimeId)
        {
            CrimeDataLayer objCrimeDataLayer = new CrimeDataLayer();
            CrimeModel objCrimeModel = objCrimeDataLayer.GetCrimeDetailsbyCrimeId(CrimeId);
            return View(objCrimeModel);
        }

        [HttpPost]
        [Authorize]
        [IsUserPolice]
        public ActionResult UpdateStatus(int CrimeId, FormCollection frmCollection)
        {
            string Status = frmCollection["Status"].ToString();
            string Comment = frmCollection["Comment"].ToString();
            CrimeDataLayer objCrimeDataLayer = new CrimeDataLayer();
            bool Result = objCrimeDataLayer.UpdateCrimeStatus(CrimeId, Status, Comment);
            CrimeModel objCrimeModel = objCrimeDataLayer.GetCrimeDetailsbyCrimeId(CrimeId);
            if (Result)
            {
                ViewBag.Status = "success";
                ViewBag.Message = "Your status and comments has been updated successfully";
            }
            else
            {
                ViewBag.Status = "error";
                ViewBag.Message = "internal error occured";
            }

            return View(objCrimeModel);
        }
    }
}