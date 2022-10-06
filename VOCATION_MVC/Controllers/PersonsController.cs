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
    public class PersonsController : Controller
    {
       private  Vacation_DBEntities db = new Vacation_DBEntities();

        // GET: Persons
        public ActionResult Index()
        {
            var tbl_Persons = db.Tbl_Persons.Include(t => t.Tbl_Departments).Include(t => t.Tbl_Positions).Include(t => t.Tbl_Rols);
            return View(tbl_Persons.Where(o => o.IsActive == true).ToList());
        }

       
        public ActionResult GetProfile(string mail)
        {
            var tbl_Persons = db.Tbl_Persons.Include(t => t.Tbl_Departments).Include(t => t.Tbl_Positions).Include(t => t.Tbl_Rols).Include(t => t.Tbl_Account);
            var personid = db.Tbl_Account.Where(o => o.Mail == mail).ToList();
            return View(personid);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Persons tbl_Persons = db.Tbl_Persons.Find(id);
            if (tbl_Persons == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Persons);
        }

        // GET: Persons/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Tbl_Departments, "id", "ShortName");
            ViewBag.PositionId = new SelectList(db.Tbl_Positions, "id", "Position");
            ViewBag.RoleId = new SelectList(db.Tbl_Rols, "id", "RoleName");
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Surname,PositionId,DepartmentId,RoleId,RegDate,IsActive")] Tbl_Persons tbl_Persons)
        {
            if (ModelState.IsValid)
            {
                var regdate = DateTime.Now;
                tbl_Persons.RegDate = regdate;
                tbl_Persons.IsActive = true;
                db.Tbl_Persons.Add(tbl_Persons);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Tbl_Departments, "id", "ShortName", tbl_Persons.DepartmentId);
            ViewBag.PositionId = new SelectList(db.Tbl_Positions, "id", "Position", tbl_Persons.PositionId);
            ViewBag.RoleId = new SelectList(db.Tbl_Rols, "id", "RoleName", tbl_Persons.RoleId);
            return View(tbl_Persons);
        }

        // GET: Persons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Persons tbl_Persons = db.Tbl_Persons.Find(id);
            if (tbl_Persons == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Tbl_Departments, "id", "ShortName", tbl_Persons.DepartmentId);
            ViewBag.PositionId = new SelectList(db.Tbl_Positions, "id", "Position", tbl_Persons.PositionId);
            ViewBag.RoleId = new SelectList(db.Tbl_Rols, "id", "RoleName", tbl_Persons.RoleId);
            return View(tbl_Persons);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Tbl_Persons tbl_Persons)
        {
            if (ModelState.IsValid)
            {
                var EditUser = db.Tbl_Persons.FirstOrDefault(o => o.id == tbl_Persons.id);
                EditUser.Name=tbl_Persons.Name;
                EditUser.Surname = tbl_Persons.Surname;
                EditUser.PositionId = tbl_Persons.PositionId;
                EditUser.DepartmentId = tbl_Persons.DepartmentId;
                EditUser.RoleId = tbl_Persons.RoleId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Tbl_Departments, "id", "ShortName", tbl_Persons.DepartmentId);
            ViewBag.PositionId = new SelectList(db.Tbl_Positions, "id", "Position", tbl_Persons.PositionId);
            ViewBag.RoleId = new SelectList(db.Tbl_Rols, "id", "RoleName", tbl_Persons.RoleId);
            return View(tbl_Persons);
        }

        // GET: Persons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Persons tbl_Persons = db.Tbl_Persons.Find(id);
            if (tbl_Persons == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Persons);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {        
            var DeleteUser = db.Tbl_Persons.FirstOrDefault(o => o.id == id);
            DeleteUser.IsActive = false;
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
