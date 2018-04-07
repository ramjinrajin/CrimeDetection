using CrimeDetectionClass.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CrimeDetectionClass.Controllers
{
    public class IsUserPoliceAttribute : ActionFilterAttribute
    {
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string UserName = System.Web.HttpContext.Current.User.Identity.Name;
            UserDataLayer objUserDataLayer = new UserDataLayer();
            bool IsPolice = objUserDataLayer.GetIsPoliceStatus(UserName);

            if (IsPolice)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult(string.Format("/Error/Index"));
            }


        }
    }
}
