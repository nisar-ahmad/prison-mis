using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRS.Models;

namespace PRS.Controllers
{
    public class MedicalOfficerController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /MedicalOfficer/

        public ActionResult Index()
        {
            var medicalofficers = db.MedicalOfficers.Include(m => m.Jail);
            return View(medicalofficers.ToList());
        }

        //
        // GET: /MedicalOfficer/Details/5

        public ActionResult Details(int id = 0)
        {
            MedicalOfficer medicalofficer = db.MedicalOfficers.Find(id);
            if (medicalofficer == null)
            {
                return HttpNotFound();
            }
            return View(medicalofficer);
        }

        //
        // GET: /MedicalOfficer/Create

        public ActionResult Create()
        {
            ViewBag.JailId = new SelectList(db.Jails, "JailId", "Name");
            return View();
        }

        //
        // POST: /MedicalOfficer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicalOfficer medicalofficer)
        {
            if (ModelState.IsValid)
            {
                db.MedicalOfficers.Add(medicalofficer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JailId = new SelectList(db.Jails, "JailId", "Name", medicalofficer.JailId);
            return View(medicalofficer);
        }

        //
        // GET: /MedicalOfficer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MedicalOfficer medicalofficer = db.MedicalOfficers.Find(id);
            if (medicalofficer == null)
            {
                return HttpNotFound();
            }
            ViewBag.JailId = new SelectList(db.Jails, "JailId", "Name", medicalofficer.JailId);
            return View(medicalofficer);
        }

        //
        // POST: /MedicalOfficer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MedicalOfficer medicalofficer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalofficer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JailId = new SelectList(db.Jails, "JailId", "Name", medicalofficer.JailId);
            return View(medicalofficer);
        }

        //
        // GET: /MedicalOfficer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MedicalOfficer medicalofficer = db.MedicalOfficers.Find(id);
            if (medicalofficer == null)
            {
                return HttpNotFound();
            }
            return View(medicalofficer);
        }

        //
        // POST: /MedicalOfficer/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicalOfficer medicalofficer = db.MedicalOfficers.Find(id);
            db.MedicalOfficers.Remove(medicalofficer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}