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
    public class PositionsController : Controller
    {
        private Vacation_DBEntities db = new Vacation_DBEntities();

        public ActionResult Index()
        {
            return View(db.Tbl_Positions.Where(o=>o.IsActive==true).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Positions tbl_Positions = db.Tbl_Positions.Find(id);
            if (tbl_Positions == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Positions);
        }

        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,RegDate,IsActive,Position")] Tbl_Positions tbl_Positions)
        {
            if (ModelState.IsValid)
            {
                var regdate = DateTime.Now;
                tbl_Positions.RegDate = regdate;
                tbl_Positions.IsActive = true;
                db.Tbl_Positions.Add(tbl_Positions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Positions);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Positions tbl_Positions = db.Tbl_Positions.Find(id);
            if (tbl_Positions == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Positions);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Tbl_Positions tbl_Positions)
        {
            if (ModelState.IsValid)
            {
                var EditPosition = db.Tbl_Positions.FirstOrDefault(o => o.id == tbl_Positions.id);
                EditPosition.Position = tbl_Positions.Position;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Positions);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Positions tbl_Positions = db.Tbl_Positions.Find(id);
            if (tbl_Positions == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Positions);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var DeletePosition = db.Tbl_Positions.FirstOrDefault(o => o.id == id);
            DeletePosition.IsActive = false;
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
