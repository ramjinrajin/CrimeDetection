using CrimeDetectionClass.Models.Crime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrimeDetectionClass.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize]
        [IsUserPolice]
        public ActionResult Index()
        {
            CrimeDataLayer obj = new CrimeDataLayer();


            List<string> Queries = new List<string>
        {
            "sELECT COUNT(*) FROM tblCrime Where Location='Trivandrum'",
            "sELECT COUNT(*) FROM tblCrime Where Location='Kollam'",
            "sELECT COUNT(*) FROM tblCrime Where Location='Alappuzha'",
            "sELECT COUNT(*) FROM tblCrime Where Location='Thrissur'"
        };

            Location Location = obj.getCrimeRates(Queries);

            return View(Location);
        }
    }
}