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
    public class EarnedRemissionController : PRSBaseController
    {
        public JsonResult CalculatePDR(int Years = 0, int Months = 0, int Days = 0, int Id = 0)
        {
            var date = string.Empty;
            var admission = GetLatestAdmission();

            var fullSentenceDate = admission.DateOfReleaseWithFullSentence;

            if(fullSentenceDate != null)
            {
                var remissions = db.EarnedRemissions.Where(o => o.AdmissionId == admission.AdmissionId);

                if(Id > 0)
                    remissions = remissions.Where(o => o.EarnedRemissionId != Id);

                var remission = from e in remissions
                                group e by 1 into g
                                select new
                                {
                                    Days = g.Sum(o => o.Days),
                                    Months = g.Sum(o => o.Months),
                                    Years = g.Sum(o=> o.Years)
                                };

                var r = remission.FirstOrDefault();

                if(r != null)
                {
                    Years += r.Years;
                    Months += r.Months;
                    Days += r.Days;
                }

                DateTime pdr = fullSentenceDate.Value.AddYears(-Years).AddMonths(-Months).AddDays(-Days);
                date = pdr.ToString("dd-MMM-yyyy");
            }

            return Json(date, JsonRequestBehavior.AllowGet);
        }

        private void PopulateDropDowns(EarnedRemission earnedRemission)
        {
            ViewBag.RemissionTypeId = new SelectList(db.RemissionTypes, "RemissionTypeId", "Name", earnedRemission.RemissionTypeId);
            ViewBag.LabourType = new SelectList(Enum.GetValues(typeof(LabourType)), earnedRemission.LabourType);
        }

        //
        // GET: /EarnedRemission/

        public ActionResult Index()
        {
            var earnedRemissions = db.EarnedRemissions.Include(m => m.RemissionType).Include(m => m.Admission).Include(m => m.Prisoner).Where(o => o.PrisonerId == PrisonerId).ToList();
            return View(earnedRemissions);
        }

        //
        // GET: /EarnedRemission/Details/5

        public ActionResult Details(int id = 0)
        {
            EarnedRemission earnedRemission = db.EarnedRemissions.First(o => o.EarnedRemissionId == id);
            
            if (earnedRemission == null)
                return HttpNotFound();
            
            return View(earnedRemission);
        }

        //
        // GET: /EarnedRemission/Create

        public ActionResult Create()
        {
            var earnedRemission = new EarnedRemission();

            var admission = GetLatestAdmission();
            string date = "Enter in Admission or Court Decision";

            if (admission.DateOfReleaseWithFullSentence != null)
                date = admission.DateOfReleaseWithFullSentence.Value.ToString("dd-MMM-yyyy");

            ViewBag.DateOfReleaseWithFullSentence = date;

            PopulateDropDowns(earnedRemission);
            return View(earnedRemission);
        }

        //
        // POST: /EarnedRemission/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EarnedRemission earnedRemission)
        {
            if (ModelState.IsValid)
            {
                earnedRemission.AdmissionId = GetLatestAdmissionId(earnedRemission.PrisonerId);
                db.EarnedRemissions.Add(earnedRemission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateDropDowns(earnedRemission);
            return View(earnedRemission);
        }

        //
        // GET: /EarnedRemission/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EarnedRemission earnedRemission = db.EarnedRemissions.Find(id);
        
            if (earnedRemission == null)
                return HttpNotFound();

            var admission = GetLatestAdmission();
            string date = "Enter in Admission or Court Decision";

            if (admission.DateOfReleaseWithFullSentence != null)
                date = admission.DateOfReleaseWithFullSentence.Value.ToString("dd-MMM-yyyy");

            ViewBag.DateOfReleaseWithFullSentence = date;

            PopulateDropDowns(earnedRemission);
            return View(earnedRemission);
        }

        //
        // POST: /EarnedRemission/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EarnedRemission earnedRemission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(earnedRemission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateDropDowns(earnedRemission);
            return View(earnedRemission);
        }

        //
        // GET: /EarnedRemission/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EarnedRemission earnedRemission = db.EarnedRemissions.Include(o => o.RemissionType).First(o => o.EarnedRemissionId == id);

            if (earnedRemission == null)
                return HttpNotFound();
            
            return View(earnedRemission);
        }

        //
        // POST: /EarnedRemission/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EarnedRemission earnedRemission = db.EarnedRemissions.Find(id);
            db.EarnedRemissions.Remove(earnedRemission);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}