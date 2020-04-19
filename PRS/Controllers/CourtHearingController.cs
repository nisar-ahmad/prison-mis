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
    public class CourtHearingController : PRSBaseController
    {
        private CourtHearing CreateCourtHearing()
        {
            var courthearing = new CourtHearing();
            courthearing.PrisonerId = PrisonerId;
            courthearing.AdmissionId = AdmissionId;

            return courthearing;
        }

        private void PopulateDropDowns(CourtHearing courthearing)
        {
            IQueryable<FIR> firList = null;

            if (courthearing.AdmissionId > 0)
                firList = db.FIRs.Where(o => o.AdmissionId == courthearing.AdmissionId);
            else
                firList = db.FIRs.Where(o => o.Admission.PrisonerId == courthearing.PrisonerId);

            ViewBag.FIRId = new SelectList(firList, "FIRId", "FIRNumber", courthearing.FIRId);
            ViewBag.CourtId = new SelectList(db.Courts, "CourtId", "Name", courthearing.CourtId);
            ViewBag.JudgeTypeId = new SelectList(db.JudgeTypes, "JudgeTypeId", "Name", courthearing.JudgeTypeId);

            if (courthearing.JudgeId > 0)
                ViewBag.JudgeName = db.Judges.FirstOrDefault(o => o.JudgeId == courthearing.JudgeId).Name;
        }

        public JsonResult Judges(string id)
        {
            int courtId = Convert.ToInt32(id);

            var judges = db.Judges.Where(o => o.CourtId == courtId);
            return Json(new SelectList(judges.ToArray(), "JudgeId", "Name"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Admit(int PrisonerId = 0, int AdmissionId = 0, int CourtHearingId = 0)
        {
            if (PrisonerId <= 0 || AdmissionId <= 0)
                return HttpNotFound();

            CourtHearing courthearing = null;

            if (CourtHearingId > 0)
                courthearing = db.CourtHearings.First(o => o.CourtHearingId == CourtHearingId);
            else
            {
                // Find any incomplete Court Hearing
                courthearing = db.CourtHearings.FirstOrDefault(o => o.AdmissionId == AdmissionId && o.IsComplete == false);

                if (courthearing == null) // Its a new Court Hearing!
                {
                    courthearing = CreateCourtHearing();
                    courthearing.IsComplete = false;
                    courthearing.Remarks = "First Hearing";
                }
            }

            PopulateDropDowns(courthearing);
            return View(courthearing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admit(CourtHearing courthearing, string JudgeName)
        {
            if (string.IsNullOrWhiteSpace(JudgeName) == true)
            {
                ModelState.Clear();
                ModelState.AddModelError("JudgeId", "Judge Name is Required");
            }
            else if (courthearing.CourtId > 0 && string.IsNullOrWhiteSpace(JudgeName) == false && courthearing.JudgeId == 0)
            {
                Judge judge = new Judge();
                judge.CourtId = courthearing.CourtId;
                judge.Name = JudgeName;
                db.Judges.Add(judge);
                db.SaveChanges();

                courthearing.JudgeId = judge.JudgeId;
            }

            if (ModelState.IsValid)
            {
                if (courthearing.CourtHearingId > 0) // Edit Existing
                    db.Entry(courthearing).State = EntityState.Modified;
                else
                    db.CourtHearings.Add(courthearing);

                courthearing.IsComplete = true;

                var admission = db.Admissions.First(o => o.AdmissionId == courthearing.AdmissionId);
                admission.IsComplete = true;

                var fir = db.FIRs.First(o=> o.FIRId == courthearing.FIRId);
                fir.IsComplete = true;

                db.SaveChanges();
                CourtHearingId = courthearing.CourtHearingId;
                return RedirectToAction("Details", "Prisoner");
            }

            PopulateDropDowns(courthearing);
            return View(courthearing);
        }

        //
        // GET: /CourtHearing/

        public ActionResult Index()
        {
            var courthearings = db.CourtHearings.Include(c => c.FIR).Include(c => c.Court).Include(c => c.JudgeType).Include(c => c.Judge).Include(c => c.Admission).Include(c => c.Prisoner).Where(o => o.PrisonerId == PrisonerId);
            return View(courthearings.ToList());
        }

        private IQueryable<CourtHearing> GetCourtHearings(DateTime productionDate)
        {
            var courthearings = db.CourtHearings.Include(c => c.FIR).Include(c => c.Court).Include(c => c.JudgeType).Include(c => c.Judge).Include(c => c.Admission).Include(c => c.Prisoner).Where(o => o.DateOfHearing == productionDate);
            ViewBag.ProductionDate = productionDate.ToString("dd-MMM-yyyy");

            return courthearings;
        }

        public ActionResult List()
        {
            ViewBag.ProductionDate = Request["ProductionDate"];
            DateTime ProductionDate = DateTime.Parse(Request["ProductionDate"]);
            return View(GetCourtHearings(ProductionDate));
        }

        [HttpPost]
        public ActionResult List(DateTime ProductionDate)
        {
            return View(GetCourtHearings(ProductionDate));
        }

        //
        // GET: /CourtHearing/Details/5

        public ActionResult Details(int id = 0)
        {
            CourtHearing courthearing = db.CourtHearings.Find(id);

            if (courthearing == null)
                return HttpNotFound();
            
            return View(courthearing);
        }

        //
        // GET: /CourtHearing/Create
        
        public ActionResult Create()
        {
            CourtHearing courthearing = CreateCourtHearing();

            CourtHearing prevHearing = db.CourtHearings.OrderByDescending(o => o.DateOfHearing).FirstOrDefault(o => o.PrisonerId == PrisonerId);
            
            if (prevHearing != null)
            {
                courthearing.FIRId = prevHearing.FIRId;
                courthearing.JudgeId = prevHearing.JudgeId;
                courthearing.JudgeTypeId = prevHearing.JudgeTypeId;
                courthearing.CourtId = prevHearing.CourtId;
                courthearing.DateOfHearing = prevHearing.DateOfHearing.AddDays(14);
            }

            PopulateDropDowns(courthearing);
            return View(courthearing);
        }

        //
        // POST: /CourtHearing/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourtHearing courthearing, string JudgeName)
        {
            if (string.IsNullOrWhiteSpace(JudgeName) == true)
            {
                ModelState.Clear();
                ModelState.AddModelError("JudgeId", "Judge Name is Required");
            }

            if (courthearing.JudgeTypeId == null)
                ModelState.AddModelError("JudgeTypeId", "Judge Type is Required");
            else if (courthearing.CourtId > 0 && string.IsNullOrWhiteSpace(JudgeName) == false && courthearing.JudgeId == 0)
            {
                Judge judge = new Judge();
                judge.CourtId = courthearing.CourtId;
                judge.Name = JudgeName;
                db.Judges.Add(judge);
                db.SaveChanges();

                courthearing.JudgeId = judge.JudgeId;
            }

            if (ModelState.IsValid)
            {
                courthearing.AdmissionId = db.Admissions.OrderByDescending(o => o.AdmissionId).FirstOrDefault(o => o.PrisonerId == courthearing.PrisonerId).AdmissionId;
                db.CourtHearings.Add(courthearing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateDropDowns(courthearing);
            return View(courthearing);
        }

        //
        // GET: /CourtHearing/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CourtHearing courthearing = db.CourtHearings.Find(id);

            if (courthearing == null)
                return HttpNotFound();

            PopulateDropDowns(courthearing);
            return View(courthearing);
        }

        //
        // POST: /CourtHearing/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourtHearing courthearing, string JudgeName)
        {
            if (string.IsNullOrWhiteSpace(JudgeName) == true)
            {
                ModelState.Clear();
                ModelState.AddModelError("JudgeId", "Judge Name is Required");
            }

            if (courthearing.JudgeTypeId == null)
                ModelState.AddModelError("JudgeTypeId", "Judge Type is Required");
            else if (courthearing.CourtId > 0 && string.IsNullOrWhiteSpace(JudgeName) == false && courthearing.JudgeId == 0)
            {
                Judge judge = new Judge();
                judge.CourtId = courthearing.CourtId;
                judge.Name = JudgeName;
                db.Judges.Add(judge);
                db.SaveChanges();

                courthearing.JudgeId = judge.JudgeId;
            }

            if (ModelState.IsValid)
            {
                db.Entry(courthearing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateDropDowns(courthearing);
            return View(courthearing);
        }

        //
        // GET: /CourtHearing/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CourtHearing courthearing = db.CourtHearings.Find(id);

            if (courthearing == null)
                return HttpNotFound();
            
            return View(courthearing);
        }

        //
        // POST: /CourtHearing/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourtHearing courthearing = db.CourtHearings.Find(id);
            db.CourtHearings.Remove(courthearing);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}