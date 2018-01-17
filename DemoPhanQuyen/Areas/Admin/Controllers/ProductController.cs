using DemoPhanQuyen.Areas.Admin.Models;
using DemoPhanQuyen.Areas.Admin.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoPhanQuyen.Areas.Admin.Controllers
{
    [Author]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        PhanQuyenEntities db = new PhanQuyenEntities();
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
                return View(db.Products.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Products p)
        {
            if (ModelState.IsValid)
            {
                p.UserName = Session["user"].ToString();
                db.Products.Add(p);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Product");
        }


        [HttpGet]
        public ActionResult Edit(int id, string username)
        {
            Products p = db.Products.Single(n => n.ID == id);
            username = db.Products.Find(id).UserName.ToString();
            if (Session["user"].ToString() != username && username != "SuperAdmin")
            {
                return RedirectToAction("Warning", "Home");
            }
            else
            {
                return View(p);
            }
        }
        [HttpPost]
        public ActionResult Edit(string username, Products p)
        {
            if (Session["user"].ToString() != username && username != "SuperAdmin")
            {
                RedirectToAction("Warning", "Home");
            }

            if (ModelState.IsValid)
            {
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Product", null);

        }
        //[HttpGet]
        public ActionResult Delete(int id, string username)
        {

            Products p = db.Products.Single(n => n.ID == id);
            username = db.Products.Find(id).UserName.ToString();


            if (Session["user"].ToString() != username && username != "SuperAdmin")
            {
                return RedirectToAction("Warning", "Home");
            }
            else
            {
                db.Products.Remove(p);
                db.SaveChanges();
                return RedirectToAction("Index", "Product", null);
            }
        }
    }
}