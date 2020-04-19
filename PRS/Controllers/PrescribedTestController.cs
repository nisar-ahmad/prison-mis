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
    public class PrescribedTestController : PRSBaseController
    {
        private void PopulateDropDowns(PrescribedTest prescribedtest)
        {
            ViewBag.MedicalTestId = new SelectList(db.MedicalTests, "MedicalTestId", "Name", prescribedtest.MedicalTestId);
        }

        //
        // GET: /PrescribedTest/

        public ActionResult Index()
        {
            var prescribedtests = db.PrescribedTests.Include(p => p.MedicalTest).Include(p => p.MedicalTreatment).Include(p => p.Admission).Include(p => p.Prisoner).Where(o=> o.PrisonerId == PrisonerId);
            return View(prescribedtests.ToList());
        }

        //
        // GET: /PrescribedTest/Details/5

        public ActionResult Details(int id = 0)
        {
            PrescribedTest prescribedtest = db.PrescribedTests.Find(id);

            if (prescribedtest == null)
                return HttpNotFound();
            
            return View(prescribedtest);
        }

        //
        // GET: /PrescribedTest/Create

        public ActionResult Create()
        {
            var prescribedTest = new PrescribedTest();
            PopulateDropDowns(prescribedTest);

            return View(prescribedTest);
        }

        //
        // POST: /PrescribedTest/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrescribedTest prescribedtest)
        {
            if (ModelState.IsValid)
            {
                db.PrescribedTests.Add(prescribedtest);
                db.SaveChanges();
                return RedirectToAction("Details", "MedicalTreatment");
            }

            PopulateDropDowns(prescribedtest);
            return View(prescribedtest);
        }

        //
        // GET: /PrescribedTest/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PrescribedTest prescribedtest = db.PrescribedTests.Find(id);
            
            if (prescribedtest == null)
                return HttpNotFound();
            
            PopulateDropDowns(prescribedtest);
            return View(prescribedtest);
        }

        //
        // POST: /PrescribedTest/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PrescribedTest prescribedtest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescribedtest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "MedicalTreatment");
            }

            PopulateDropDowns(prescribedtest);
            return View(prescribedtest);
        }

        //
        // GET: /PrescribedTest/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PrescribedTest prescribedtest = db.PrescribedTests.Find(id);
            
            if (prescribedtest == null)
                return HttpNotFound();
            
            return View(prescribedtest);
        }

        //
        // POST: /PrescribedTest/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrescribedTest prescribedtest = db.PrescribedTests.Find(id);
            db.PrescribedTests.Remove(prescribedtest);
            db.SaveChanges();
            return RedirectToAction("Details", "MedicalTreatment");
        }
    }
}