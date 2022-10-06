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
    public class VacationAppealsController : Controller
    {
        private Vacation_DBEntities db = new Vacation_DBEntities();

        // GET: VacationAppeals
        public ActionResult Index()
        {
            var tbl_VacationAppeals = db.Tbl_VacationAppeals.Include(t => t.Tbl_Persons);
            return View(tbl_VacationAppeals.ToList());
        }

        public ActionResult GetStatus(string mail)
        {
            var tbl_VacationAppeals = db.Tbl_VacationAppeals.Include(t => t.Tbl_Persons);
            var peracc = db.Tbl_Account.Where(o => o.Mail == mail).ToList();
            var perid = peracc.FirstOrDefault().PersonId;
            if (perid != null) {
                var OnePerson = db.Tbl_VacationAppeals.Where(o => o.PersonId == perid).ToList();
                var id = OnePerson.LastOrDefault().id;
                var model= OnePerson.Where(o => o.id == id).ToList();
                return View(model);
            }

            return View();
        }

   

        // GET: VacationAppeals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_VacationAppeals tbl_VacationAppeals = db.Tbl_VacationAppeals.Find(id);
            if (tbl_VacationAppeals == null)
            {
                return HttpNotFound();
            }
            return View(tbl_VacationAppeals);
        }

        // GET: VacationAppeals/Create
        public ActionResult Create( string mail)
        {
            var tbl_VacationAppeals = db.Tbl_VacationAppeals.Include(t => t.Tbl_Persons);
            var  peracc=db.Tbl_Account.Where(o=>o.Mail==mail).ToList();
            var perid = peracc.FirstOrDefault().PersonId;
            var personId = db.Tbl_Persons.Where(o => o.id == perid).FirstOrDefault().id;

            ViewBag.PersonId = new SelectList(db.Tbl_Persons.Where(o => o.id == personId), "id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tbl_VacationAppeals tbl_VacationAppeals,string mail)
        {
            if (ModelState.IsValid)
            {
                tbl_VacationAppeals.RegDate = DateTime.Now;
                tbl_VacationAppeals.IsActive = true;
                db.Tbl_VacationAppeals.Add(tbl_VacationAppeals);
                
                db.SaveChanges();
              return RedirectToAction("GetProfile" ,"Persons", new {mail=mail});
              
            }

            ViewBag.PersonId = new SelectList(db.Tbl_Persons, "id", "Name", tbl_VacationAppeals.PersonId);
            return View(tbl_VacationAppeals);
        }

        // GET: VacationAppeals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_VacationAppeals tbl_VacationAppeals = db.Tbl_VacationAppeals.Find(id);
            if (tbl_VacationAppeals == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonId = new SelectList(db.Tbl_Persons, "id", "Name", tbl_VacationAppeals.PersonId);
            return View(tbl_VacationAppeals);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Tbl_VacationAppeals tbl_VacationAppeals)
        {
            if (ModelState.IsValid) { 

                var EditAppeal = db.Tbl_VacationAppeals.FirstOrDefault(o => o.id == tbl_VacationAppeals.id);
                EditAppeal.Status = tbl_VacationAppeals.Status;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonId = new SelectList(db.Tbl_Persons, "id", "Name", tbl_VacationAppeals.PersonId);
            return View(tbl_VacationAppeals);
        }

        // GET: VacationAppeals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_VacationAppeals tbl_VacationAppeals = db.Tbl_VacationAppeals.Find(id);
            if (tbl_VacationAppeals == null)
            {
                return HttpNotFound();
            }
            return View(tbl_VacationAppeals);
        }

        // POST: VacationAppeals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_VacationAppeals tbl_VacationAppeals = db.Tbl_VacationAppeals.Find(id);
            db.Tbl_VacationAppeals.Remove(tbl_VacationAppeals);
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
