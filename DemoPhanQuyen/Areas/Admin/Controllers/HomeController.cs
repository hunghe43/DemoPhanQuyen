using DemoPhanQuyen.Areas.Admin.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoPhanQuyen.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        PhanQuyenEntities db = new PhanQuyenEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index_Per_Group(string usergroupid)
        {
            usergroupid = Request.Form["SL"];
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            //if (Session["Admin"] != null || Session["Member"] != null)
            //{
            //    return RedirectToAction("Index", "Product");
            //}
            else
            {
                var viewModel = new MyViewModel
                {
                    l_role = db.Role.ToList(),
                    l_per_group = db.Group_Permission.Where(n => n.UserGroupID == usergroupid).ToList(),
                };
                return View(viewModel);
            }
        }

        public ActionResult Index_Per_User(string usergroupid)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (Session["Admin"] != null || Session["Member"] != null)
            {
                return RedirectToAction("Index", "Product");
            }
            else
            {
                var viewModel = new MyViewModel
                {
                    l_role = db.Role.ToList(),
                    l_per_group = db.Group_Permission.Where(n => n.UserGroupID == usergroupid).ToList(),
                };
                return View(viewModel);
            }
        }

        public PartialViewResult tbl_role_per_group(FormCollection f)
        {
            string usergroupid;
            if (f["SL"] == null)
            {
                usergroupid = "Admin";
            }
            else
            {
                usergroupid = f["SL"].ToString();
                ViewBag.hienthi = usergroupid;
            }
            var viewModel = new MyViewModel
            {
                l_role = db.Role.ToList(),
                l_per_group = db.Group_Permission.Where(n => n.UserGroupID == usergroupid).ToList(),
                l_usergroup = db.UserGroup.ToList()
            };

            return PartialView(viewModel);
        }

        public PartialViewResult tbl_role_per_user(FormCollection f)
        {
            string username;
            string usergroupid;
            var viewModel = new MyViewModel();
            if (f["SL"] == null)
            {
                usergroupid = "ADMIN";
            }
            else
            {
                usergroupid = f["SL"].ToString();
                ViewBag.hienthi = usergroupid;
            }
            if (f["ND"] == null)
            {
                username = "";
            }
            else
            {
                username = f["ND"].ToString();
                ViewBag.name = username;
            }
            if (f["ND"] != null && f["SL"] != null)
            {
                viewModel = new MyViewModel
                {
                    l_role = db.Role.ToList(),
                    l_per_group = db.Group_Permission.Where(n => n.UserGroupID == usergroupid).ToList(),
                    l_per_user = db.User_Permission.Where(n => n.UserName == username).ToList(),
                    l_user = db.NguoiDung.Where(n => n.UserGroupID == usergroupid).ToList(),
                    l_usergroup = db.UserGroup.ToList(),
                };
            }
            else
            {
                viewModel = new MyViewModel
                {
                    l_role = db.Role.ToList(),
                    l_per_group = db.Group_Permission.Where(n => n.UserGroupID == usergroupid).ToList(),
                    l_per_user = db.User_Permission.ToList(),
                    l_user = db.NguoiDung.Where(n => n.UserGroupID == usergroupid).ToList(),
                    l_usergroup = db.UserGroup.ToList(),
                };
            }
            //ViewBag.SL = new SelectList(db.UserGroup.ToList(), "id", "name",db.UserGroup.SingleOrDefault(n=>n.id==usergroupid).name);
            return PartialView(viewModel);
        }

        public JsonResult Save_Group(List<string> role_id, string usergroupid, List<string> role_id_uncheck)
        {
            List<NguoiDung> lnd = db.NguoiDung.Where(n => n.UserGroupID == usergroupid).ToList();

            List<Group_Permission> lst = db.Group_Permission.Where(n => n.UserGroupID == usergroupid).ToList();
            foreach (var i in role_id)
            {
                if (!lst.Any(n => n.RoleID.Contains(i)))
                {
                    Group_Permission p = new Group_Permission();
                    p.UserGroupID = usergroupid;
                    p.RoleID = i;
                    db.Group_Permission.Add(p);
                    foreach (var nd in lnd)
                    {
                        List<User_Permission> lpu = db.User_Permission.Where(n => n.UserName == nd.UserName).ToList();
                        if (!lpu.Any(n => n.RoleID.Contains(i)))
                        {
                            User_Permission pu = new User_Permission();
                            pu.RoleID = i;
                            pu.UserName = nd.UserName;
                            db.User_Permission.Add(pu);
                        }
                    }
                }

            }
            db.SaveChanges();


            if (role_id_uncheck != null)
            {
                foreach (var j in role_id_uncheck)
                {

                    if (lst.Any(n => n.RoleID.Contains(j)))
                    {
                        Group_Permission per = db.Group_Permission.Single(n => n.RoleID == j && n.UserGroupID == usergroupid);
                        foreach (var nd in lnd)
                        {
                            List<User_Permission> lperu = db.User_Permission.Where(n => n.UserName == nd.UserName && n.RoleID == j).ToList();
                            foreach (var peru in lperu)
                            {
                                db.User_Permission.Remove(peru);
                            }
                        }
                        db.Group_Permission.Remove(per);
                        db.SaveChanges();
                    }
                }
            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save_user(List<string> role_id, string usergroupid, string username, List<string> role_id_uncheck)
        {
            List<Group_Permission> lst = db.Group_Permission.Where(n => n.UserGroupID == usergroupid).ToList();
            List<User_Permission> l = db.User_Permission.Where(n => n.UserName == username).ToList();
            foreach (var i in role_id)
            {
                if (!l.Any(n => n.RoleID.Contains(i)))
                {
                    User_Permission p = new User_Permission();
                    p.UserName = username;
                    p.RoleID = i;
                    db.User_Permission.Add(p);
                }
            }
            db.SaveChanges();
            if (role_id_uncheck != null)
            {
                foreach (var j in role_id_uncheck)
                {
                    if (l.Any(n => n.RoleID.Contains(j)))
                    {
                        User_Permission per = db.User_Permission.Single(n => n.RoleID == j && n.UserName == username);
                        db.User_Permission.Remove(per);
                        db.SaveChanges();
                    }
                }
            }

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Warning()
        {
            return View();
        }
    }
}