using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VOCATION_MVC.Models;

namespace VOCATION_MVC.Controllers
{
  
    public class DepartmentsController : Controller
    {
        private Vacation_DBEntities db = new Vacation_DBEntities();

        
        public ActionResult Index()
        {
            return View(db.Tbl_Departments.Where(o => o.IsActive == true).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Departments tbl_Departments = db.Tbl_Departments.Find(id);
            if (tbl_Departments == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Departments);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Tbl_Departments tbl_Departments)
        {
            if (ModelState.IsValid)
            {
                tbl_Departments.RegDate = DateTime.Now;
                tbl_Departments.IsActive = true;
                db.Tbl_Departments.Add(tbl_Departments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Departments);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Departments tbl_Departments = db.Tbl_Departments.Find(id);
            if (tbl_Departments == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Departments);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tbl_Departments tbl_Departments)
        {
            if (ModelState.IsValid)
            {
                var EditDepartment = db.Tbl_Departments.FirstOrDefault(o => o.id == tbl_Departments.id);
                EditDepartment.ShortName = tbl_Departments.ShortName;
                EditDepartment.FullName = tbl_Departments.FullName;
                EditDepartment.Note = tbl_Departments.Note;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Departments);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Departments tbl_Departments = db.Tbl_Departments.Find(id);
            if (tbl_Departments == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Departments);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var DeleteDepartment = db.Tbl_Departments.Find(id);
            DeleteDepartment.IsActive = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
