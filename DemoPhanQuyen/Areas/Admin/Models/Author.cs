using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoPhanQuyen.Areas.Admin.Models
{
    public class Author : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);

            EF.UserDAO us = new EF.UserDAO();
            List<string> ls = us.GetUserPermission((string)(HttpContext.Current.Session["user"]));
            //string[] listpermission = { "Student-Delete", "Student-Edit" };
            string actionname = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "-" +
                filterContext.ActionDescriptor.ActionName;
            if (!ls.Contains(actionname))
            {
                filterContext.Result = new RedirectResult("~/Admin/Home/Warning");
            }
        }
    }
}