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
    public class VacationDaysController : Controller
    {
        private Vacation_DBEntities db = new Vacation_DBEntities();

        // GET: VacationDays
        public ActionResult Index()
        {
            var tbl_VacationDays = db.Tbl_VacationDays.Include(t => t.Tbl_Positions);
            return View(tbl_VacationDays.ToList());
        }

        // GET: VacationDays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_VacationDays tbl_VacationDays = db.Tbl_VacationDays.Find(id);
            if (tbl_VacationDays == null)
            {
                return HttpNotFound();
            }
            return View(tbl_VacationDays);
        }

        // GET: VacationDays/Create
        public ActionResult Create()
        {
            ViewBag.PositionId = new SelectList(db.Tbl_Positions, "id", "Position");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Tbl_VacationDays tbl_VacationDays)
        {
            if (ModelState.IsValid)
            {
                tbl_VacationDays.RegDate = DateTime.Now;
                db.Tbl_VacationDays.Add(tbl_VacationDays);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PositionId = new SelectList(db.Tbl_Positions, "id", "Position", tbl_VacationDays.PositionId);
            return View(tbl_VacationDays);
        }

        // GET: VacationDays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_VacationDays tbl_VacationDays = db.Tbl_VacationDays.Find(id);
            if (tbl_VacationDays == null)
            {
                return HttpNotFound();
            }
            ViewBag.PositionId = new SelectList(db.Tbl_Positions, "id", "Position", tbl_VacationDays.PositionId);
            return View(tbl_VacationDays);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Tbl_VacationDays tbl_VacationDays)
        {
            if (ModelState.IsValid)
            {
                var EditVacaionDays = db.Tbl_VacationDays.FirstOrDefault(o => o.id == tbl_VacationDays.id);
                EditVacaionDays.RegDate = EditVacaionDays.RegDate;
                EditVacaionDays.PositionId = tbl_VacationDays.PositionId;
                EditVacaionDays.DayCount = tbl_VacationDays.DayCount;
                EditVacaionDays.Note = tbl_VacationDays.Note;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PositionId = new SelectList(db.Tbl_Positions, "id", "Position", tbl_VacationDays.PositionId);
            return View(tbl_VacationDays);
        }

        // GET: VacationDays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_VacationDays tbl_VacationDays = db.Tbl_VacationDays.Find(id);
            if (tbl_VacationDays == null)
            {
                return HttpNotFound();
            }
            return View(tbl_VacationDays);
        }

        // POST: VacationDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_VacationDays tbl_VacationDays = db.Tbl_VacationDays.Find(id);
            db.Tbl_VacationDays.Remove(tbl_VacationDays);
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

