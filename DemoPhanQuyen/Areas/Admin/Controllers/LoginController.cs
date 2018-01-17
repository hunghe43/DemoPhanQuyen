using DemoPhanQuyen.Areas.Admin.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoPhanQuyen.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        PhanQuyenEntities db = new PhanQuyenEntities();
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            string user = f["loginname"].ToString();
            string pass = f.Get("password").ToString();
            NguoiDung acc = db.NguoiDung.SingleOrDefault(n => n.UserName == user && n.Password == pass);

            if (acc != null)
            {
                if (acc.UserGroupID == "GroupAdmin")
                {
                    Session["Admin"] = user;
                }
                if (acc.UserGroupID == "SuperAdmin")
                {
                    Session["SuperAdmin"] = user;
                }
                if (acc.UserGroupID == "GroupUser")
                {
                    Session["user"] = user;
                }
                Session["user"] = user;
                Session["group"] = acc.UserGroupID;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Thongbao = "Tên tài khoản hoặc mật khẩu không đúng!";
            return View();
        }
    }
}
