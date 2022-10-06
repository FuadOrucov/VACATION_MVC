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
    public class AccountController : Controller
    {
        private Vacation_DBEntities db = new Vacation_DBEntities();

        // GET: Account
        public ActionResult Index()
        {
            var tbl_Account = db.Tbl_Account.Include(t => t.Tbl_Persons);
            return View(db.Tbl_Account.Where(o => o.IsActive == true));
        }

        // GET: Account/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Account tbl_Account = db.Tbl_Account.Find(id);
            if (tbl_Account == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Account);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            ViewBag.PersonId = new SelectList(db.Tbl_Persons, "id", "Name");
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tbl_Account tbl_Account)
        {
            if (ModelState.IsValid)
            {
                tbl_Account.RegDate = DateTime.Now;
                tbl_Account.IsActive = true;
                db.Tbl_Account.Add(tbl_Account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonId = new SelectList(db.Tbl_Persons, "id", "Name", tbl_Account.PersonId);
            return View(tbl_Account);
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Account tbl_Account = db.Tbl_Account.Find(id);
            if (tbl_Account == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonId = new SelectList(db.Tbl_Persons, "id", "Name", tbl_Account.PersonId);
            return View(tbl_Account);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Tbl_Account tbl_Account)
        {
            if (ModelState.IsValid)
            {
                var EditAccount = db.Tbl_Account.FirstOrDefault(o => o.id == tbl_Account.id);
                EditAccount.PersonId = tbl_Account.PersonId;
                EditAccount.Mail = tbl_Account.Mail;
                EditAccount.Password = tbl_Account.Password;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonId = new SelectList(db.Tbl_Persons, "id", "Name", tbl_Account.PersonId);
            return View(tbl_Account);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Account tbl_Account = db.Tbl_Account.Find(id);
            if (tbl_Account == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Account);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Account tbl_Account = db.Tbl_Account.Find(id);
            db.Tbl_Account.Remove(tbl_Account);
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







       