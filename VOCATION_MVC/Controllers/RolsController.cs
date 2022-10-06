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
    public class RolsController : Controller
    {
        private Vacation_DBEntities db = new Vacation_DBEntities();

        // GET: Rols
        public ActionResult Index()
        {
            return View(db.Tbl_Rols.Where(o => o.IsActive == true).ToList());
        }

        // GET: Rols/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Rols tbl_Rols = db.Tbl_Rols.Find(id);
            if (tbl_Rols == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Rols);
        }

        // GET: Rols/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rols/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tbl_Rols tbl_Rols)
        {
            if (ModelState.IsValid)
            {
                tbl_Rols.RegDate = DateTime.Now;
                tbl_Rols.IsActive = true;
                db.Tbl_Rols.Add(tbl_Rols);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Rols);
        }

        // GET: Rols/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Rols tbl_Rols = db.Tbl_Rols.Find(id);
            if (tbl_Rols == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Rols);
        }

        // POST: Rols/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,RoleName,RegDate,IsActive")] Tbl_Rols tbl_Rols)
        {
            if (ModelState.IsValid)
            {
                var EditRols = db.Tbl_Rols.FirstOrDefault(o => o.id == tbl_Rols.id);
                EditRols.RoleName = tbl_Rols.RoleName;         

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Rols);
        }

        // GET: Rols/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Rols tbl_Rols = db.Tbl_Rols.Find(id);
            if (tbl_Rols == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Rols);
        }

        // POST: Rols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Rols tbl_Rols = db.Tbl_Rols.Find(id);
            db.Tbl_Rols.Remove(tbl_Rols);
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
