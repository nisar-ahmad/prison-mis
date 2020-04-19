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
    public class MedicalTreatmentController : PRSBaseController
    {
        private void PopulateDropDowns(MedicalTreatment medicaltreatment)
        {
            ViewBag.DiseaseId = new SelectList(db.Diseases, "DiseaseId", "Name", medicaltreatment.DiseaseId);
            ViewBag.MedicalOfficerId = new SelectList(db.MedicalOfficers.Where(o=> o.JailId == (db.Setttings.FirstOrDefault().JailId)), "MedicalOfficerId", "Name", medicaltreatment.MedicalOfficerId);
        }

        //
        // GET: /MedicalTreatment/

        public ActionResult Index()
        {
            var medicaltreatments = db.MedicalTreatments.Include(m => m.Disease).Include(m => m.MedicalOfficer).Include(m => m.Admission).Include(m => m.Prisoner).Where(o => o.PrisonerId == PrisonerId).ToList();

            //if(medicaltreatments.Count == 1)
            //{
            //    MedicalTreatmentId = medicaltreatments[0].MedicalTreatmentId;
            //    RedirectToAction("Details");
            //}
            //else
                return View(medicaltreatments);
        }

        //
        // GET: /MedicalTreatment/Details/5

        public ActionResult Details(int MedicalTreatmentId = 0)
        {
            MedicalTreatment medicaltreatment = db.MedicalTreatments.Include(o => o.MedicalOfficer).First( o => o.MedicalTreatmentId == MedicalTreatmentId);
            
            if (medicaltreatment == null)
                return HttpNotFound();
            
            return View(medicaltreatment);
        }

        //
        // GET: /MedicalTreatment/Create

        public ActionResult Create()
        {
            var medicaltreatment = new MedicalTreatment();
            PopulateDropDowns(medicaltreatment);
            return View(medicaltreatment);
        }

        //
        // POST: /MedicalTreatment/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicalTreatment medicaltreatment)
        {
            if (ModelState.IsValid)
            {
                medicaltreatment.AdmissionId = db.Admissions.OrderByDescending(o => o.AdmissionId).FirstOrDefault(o => o.PrisonerId == medicaltreatment.PrisonerId).AdmissionId;
                db.MedicalTreatments.Add(medicaltreatment);
                db.SaveChanges();
                MedicalTreatmentId = medicaltreatment.MedicalTreatmentId;
                return RedirectToAction("Details");
            }

            PopulateDropDowns(medicaltreatment);
            return View(medicaltreatment);
        }

        //
        // GET: /MedicalTreatment/Edit/5

        public ActionResult Edit(int MedicalTreatmentId = 0)
        {
            MedicalTreatment medicaltreatment = db.MedicalTreatments.Find(MedicalTreatmentId);
        
            if (medicaltreatment == null)
                return HttpNotFound();

            PopulateDropDowns(medicaltreatment);
            return View(medicaltreatment);
        }

        //
        // POST: /MedicalTreatment/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MedicalTreatment medicaltreatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicaltreatment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            PopulateDropDowns(medicaltreatment);
            return View(medicaltreatment);
        }

        //
        // GET: /MedicalTreatment/Delete/5

        public ActionResult Delete(int MedicalTreatmentId = 0)
        {
            MedicalTreatment medicaltreatment = db.MedicalTreatments.Include(o => o.MedicalOfficer).First(o => o.MedicalTreatmentId == MedicalTreatmentId);

            if (medicaltreatment == null)
                return HttpNotFound();
            
            return View(medicaltreatment);
        }

        //
        // POST: /MedicalTreatment/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int MedicalTreatmentId)
        {
            MedicalTreatment medicaltreatment = db.MedicalTreatments.Find(MedicalTreatmentId);
            db.MedicalTreatments.Remove(medicaltreatment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}