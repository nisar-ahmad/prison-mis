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
    public class MedicalCheckupController : PRSBaseController
    {
        //
        // GET: /MedicalCheckup/

        public ActionResult Index()
        {
            var medicalcheckups = db.MedicalCheckups.Include(m => m.MedicalTreatment);
            return View(medicalcheckups.ToList());
        }

        //
        // GET: /MedicalCheckup/Details/5

        public ActionResult Details(int id = 0)
        {
            MedicalCheckup medicalcheckup = db.MedicalCheckups.Find(id);
            if (medicalcheckup == null)
            {
                return HttpNotFound();
            }
            return View(medicalcheckup);
        }

        //
        // GET: /MedicalCheckup/Create

        public ActionResult Create()
        {
            var medicalcheckup = new MedicalCheckup();
            return View(medicalcheckup);
        }

        //
        // POST: /MedicalCheckup/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicalCheckup medicalcheckup)
        {
            if (ModelState.IsValid)
            {
                db.MedicalCheckups.Add(medicalcheckup);
                db.SaveChanges();
                return RedirectToAction("Details", "MedicalTreatment");
            }

            ViewBag.MedicalTreatmentId = new SelectList(db.MedicalTreatments, "MedicalTreatmentId", "Diagnosis", medicalcheckup.MedicalTreatmentId);
            return View(medicalcheckup);
        }

        //
        // GET: /MedicalCheckup/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MedicalCheckup medicalcheckup = db.MedicalCheckups.Find(id);
            if (medicalcheckup == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicalTreatmentId = new SelectList(db.MedicalTreatments, "MedicalTreatmentId", "Diagnosis", medicalcheckup.MedicalTreatmentId);
            return View(medicalcheckup);
        }

        //
        // POST: /MedicalCheckup/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MedicalCheckup medicalcheckup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalcheckup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "MedicalTreatment");
            }
            ViewBag.MedicalTreatmentId = new SelectList(db.MedicalTreatments, "MedicalTreatmentId", "Diagnosis", medicalcheckup.MedicalTreatmentId);
            return View(medicalcheckup);
        }

        //
        // GET: /MedicalCheckup/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MedicalCheckup medicalcheckup = db.MedicalCheckups.Find(id);
            if (medicalcheckup == null)
            {
                return HttpNotFound();
            }
            return View(medicalcheckup);
        }

        //
        // POST: /MedicalCheckup/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicalCheckup medicalcheckup = db.MedicalCheckups.Find(id);
            db.MedicalCheckups.Remove(medicalcheckup);
            db.SaveChanges();
            return RedirectToAction("Details", "MedicalTreatment");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}