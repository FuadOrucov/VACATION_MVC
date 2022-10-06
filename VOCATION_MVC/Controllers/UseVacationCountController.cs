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
    public class UseVacationCountController : Controller
    {
        private Vacation_DBEntities db = new Vacation_DBEntities();
       
        public ActionResult Index()
        {
            var tbl_UseVacationCount = db.Tbl_UseVacationCount.Include(t => t.Tbl_Persons);
            return View(tbl_UseVacationCount.ToList());
        }

        public ActionResult GetOneUseDays(string mail)
        {

            var tbl_UseVacationCount = db.Tbl_UseVacationCount.Include(t => t.Tbl_Persons);
            var peracc = db.Tbl_Account.Where(o => o.Mail == mail).ToList();
            var perid = peracc.FirstOrDefault().PersonId;
            var OnePerson = db.Tbl_UseVacationCount.Where(o => o.PersonId == perid).ToList();
            return View(OnePerson);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_UseVacationCount tbl_UseVacationCount = db.Tbl_UseVacationCount.Find(id);
            if (tbl_UseVacationCount == null)
            {
                return HttpNotFound();
            }
            return View(tbl_UseVacationCount);
        }

        // POST: UseVacationCount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_UseVacationCount tbl_UseVacationCount = db.Tbl_UseVacationCount.Find(id);
            db.Tbl_UseVacationCount.Remove(tbl_UseVacationCount);
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
