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
    public class MedicalTestController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /MedicalTest/

        public ActionResult Index()
        {
            return View(db.MedicalTests.ToList());
        }

        //
        // GET: /MedicalTest/Details/5

        public ActionResult Details(int id = 0)
        {
            MedicalTest medicaltest = db.MedicalTests.Find(id);
            if (medicaltest == null)
            {
                return HttpNotFound();
            }
            return View(medicaltest);
        }

        //
        // GET: /MedicalTest/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MedicalTest/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicalTest medicaltest)
        {
            if (ModelState.IsValid)
            {
                db.MedicalTests.Add(medicaltest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medicaltest);
        }

        //
        // GET: /MedicalTest/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MedicalTest medicaltest = db.MedicalTests.Find(id);
            if (medicaltest == null)
            {
                return HttpNotFound();
            }
            return View(medicaltest);
        }

        //
        // POST: /MedicalTest/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MedicalTest medicaltest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicaltest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medicaltest);
        }

        //
        // GET: /MedicalTest/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MedicalTest medicaltest = db.MedicalTests.Find(id);
            if (medicaltest == null)
            {
                return HttpNotFound();
            }
            return View(medicaltest);
        }

        //
        // POST: /MedicalTest/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicalTest medicaltest = db.MedicalTests.Find(id);
            db.MedicalTests.Remove(medicaltest);
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