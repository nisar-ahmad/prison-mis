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
    public class AdmissionController : PRSBaseController
    {
        private void PopulateDropDowns(Admission admission)
        {
            ViewBag.BarrackId = new SelectList(db.Barracks.Where(o => o.JailId == db.Setttings.FirstOrDefault().JailId), "BarrackId", "Name", admission.BarrackId);
            ViewBag.DiseaseId = new SelectList(db.Diseases, "DiseaseId", "Name", admission.DiseaseId);

            ViewBag.DecisionStatus = new SelectList(Enum.GetValues(typeof(DecisionStatus)), admission.DecisionStatus);
            ViewBag.JudgeTypeId = new SelectList(db.JudgeTypes, "JudgeTypeId", "Name", admission.JudgeTypeId);
        }

        public ActionResult Admit(int PrisonerId = 0, int AdmissionId = 0)
        {
            if (PrisonerId <= 0)
                return HttpNotFound();

            Admission admission = null;

            if (AdmissionId > 0)
                admission = db.Admissions.First(o => o.AdmissionId == AdmissionId);
            else
            {
                // Find any incomplete admissions
                admission = db.Admissions.FirstOrDefault(o => o.PrisonerId == PrisonerId && o.IsComplete == false);

                if (admission == null) // Its a new admission!
                    admission = new Admission();
            }

            PopulateDropDowns(admission);
            ViewBag.DiseaseId = null; // Don't fetch Diseases

            return View(admission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admit(Admission admission)
        {
            if (ModelState.IsValid)
            {
                if (admission.AdmissionId > 0) // Edit Existing
                    db.Entry(admission).State = EntityState.Modified;
                else
                    db.Admissions.Add(admission);

                db.SaveChanges();
                AdmissionId = admission.AdmissionId;

                return RedirectToAction("Admit", "FIR");
            }

            PopulateDropDowns(admission);
            return View(admission);
        }

        public ActionResult Release(int PrisonerId = 0, int AdmissionId = 0)
        {
            if (PrisonerId <= 0)
                return HttpNotFound();

            Admission admission = null;

            if (AdmissionId <= 0) // Get latest admission
                admission = GetLatestAdmission(PrisonerId);
            else
                admission = db.Admissions.Include(o => o.Prisoner).First(o => o.AdmissionId == AdmissionId);

            if (admission == null)
            {
                Error("Admission record not found!");
                return View();
            }

            AdmissionId = admission.AdmissionId;

            PopulateDropDowns(admission);
            ViewBag.DiseaseId = null; // Don't fetch Diseases

            admission.DateOfRelease = DateTime.Now;

            return View(admission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Release(Admission admission)
        {
            ModelState.Clear();

            if (admission.DateOfRelease == null)
                ModelState.AddModelError("DateOfRelease", "Date Of Release is Required");

            if (string.IsNullOrWhiteSpace(admission.AuthorityForRelease) == true)
                ModelState.AddModelError("AuthorityForRelease", "Authority For Release is Required");

            if (admission.JudgeTypeId == null)
                ModelState.AddModelError("JudgeTypeId", "Judge Type is Required");

            if (ModelState.IsValid) // if valid
            {
                var a = db.Admissions.Include(o => o.Prisoner).First(o => o.AdmissionId == admission.AdmissionId);

                a.DateOfRelease = admission.DateOfRelease;
                a.JudgeTypeId = admission.JudgeTypeId;
                a.DecisionStatus = admission.DecisionStatus;
                a.AuthorityForRelease = admission.AuthorityForRelease;
                a.Remarks = admission.Remarks;
                a.Prisoner.Status = PrisonerStatus.Released;

                var releaseCheckOut = db.CheckInOuts.OrderByDescending(o => o.DateOfCheckInOut).FirstOrDefault(o => o.AdmissionId == admission.AdmissionId && o.Type == CheckInOutType.Release);

                if (releaseCheckOut != null)
                    releaseCheckOut.DateOfCheckInOut = admission.DateOfRelease.Value;
                else
                {
                    releaseCheckOut = new CheckInOut();
                    db.CheckInOuts.Add(releaseCheckOut);
                }

                releaseCheckOut.PrisonerId = admission.PrisonerId;
                releaseCheckOut.AdmissionId = admission.AdmissionId;
                releaseCheckOut.PrisonerNumber = admission.PrisonerNumber;
                releaseCheckOut.JudgeTypeId = admission.JudgeTypeId;
                releaseCheckOut.Status = CheckInOutStatus.CheckOut;
                releaseCheckOut.Type = CheckInOutType.Release;
                releaseCheckOut.DateOfCheckInOut = admission.DateOfRelease.Value;
                releaseCheckOut.Authority = admission.AuthorityForRelease;
                releaseCheckOut.Description = admission.Remarks;

                db.SaveChanges();

                return RedirectToAction("Details", "Prisoner");
            }

            PopulateDropDowns(admission);
            return View(admission);
        }

        public ActionResult AdmitMedical(int PrisonerId = 0, int AdmissionId = 0)
        {
            if (PrisonerId <= 0)
                return HttpNotFound();

            Admission admission = null;

            if (AdmissionId <= 0) // Get latest admission
                admission = GetLatestAdmission(PrisonerId);
            else
                admission = db.Admissions.Include(o => o.Prisoner).First(o => o.AdmissionId == AdmissionId);

            if (admission == null)
            {
                Error("Admission record not found!");
                return View();
            }

            AdmissionId = admission.AdmissionId;

            PopulateDropDowns(admission);
            ViewBag.BarrackId = null; // Don't fetch Barracks

            return View(admission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdmitMedical(Admission admission)
        {
            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(admission.Prisoner.IdentificationMark1) == true)
                ModelState.AddModelError("Prisoner.IdentificationMark1", "Identification Mark 1 is Required");

            if (admission.Prisoner.Age <= 0)
                ModelState.AddModelError("Prisoner.Age", "Age is Required");

            if (string.IsNullOrWhiteSpace(admission.Prisoner.Weight) == true)
                ModelState.AddModelError("Prisoner.Weight", "Weight is Required");

            if (string.IsNullOrWhiteSpace(admission.HealthOnAdmission) == true)
                ModelState.AddModelError("HealthOnAdmission", "Health On Admission is Required");

            if (string.IsNullOrWhiteSpace(admission.WeightOnAdmission) == true)
                ModelState.AddModelError("WeightOnAdmission", "Weight On Admission is Required");

            if (ModelState.Count == 0) // if valid
            {
                var a = db.Admissions.Include(o => o.Prisoner).First(o => o.AdmissionId == admission.AdmissionId);

                a.Prisoner.IdentificationMark1 = admission.Prisoner.IdentificationMark1;
                a.Prisoner.IdentificationMark2 = admission.Prisoner.IdentificationMark2;
                a.Prisoner.Scar = admission.Prisoner.Scar;
                a.Prisoner.Height = admission.Prisoner.Height;
                a.Prisoner.Weight = admission.Prisoner.Weight;
                a.Prisoner.BloodGroup = admission.Prisoner.BloodGroup;
                a.Prisoner.Age = admission.Prisoner.Age;

                a.HealthOnAdmission = admission.HealthOnAdmission;
                a.WeightOnAdmission = admission.WeightOnAdmission;
                a.KnownAilment = admission.KnownAilment;
                a.DiseaseId = admission.DiseaseId;
                a.ExplainedInjuries = admission.ExplainedInjuries;
                a.UnexplainedInjuries = admission.UnexplainedInjuries;
                a.MedicalRemarks = admission.MedicalRemarks;

                db.SaveChanges();

                return RedirectToMedicalTreatments();
            }

            PopulateDropDowns(admission);
            return View(admission);
        }

        public ActionResult ReleaseMedical(int PrisonerId = 0, int AdmissionId = 0)
        {
            if (PrisonerId <= 0)
                return HttpNotFound();

            Admission admission = null;

            if (AdmissionId <= 0) // Get latest admission
                admission = GetLatestAdmission(PrisonerId);
            else
                admission = db.Admissions.Include(o => o.Prisoner).First(o => o.AdmissionId == AdmissionId);

            if (admission == null)
            {
                Error("Admission record not found!");
                return View();
            }

            AdmissionId = admission.AdmissionId;

            PopulateDropDowns(admission);
            ViewBag.BarrackId = null; // Don't fetch Barracks

            return View(admission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReleaseMedical(Admission admission)
        {
            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(admission.HealthOnRelease) == true)
                ModelState.AddModelError("HealthOnRelease", "Health On Release is Required");

            if (string.IsNullOrWhiteSpace(admission.WeightOnRelease) == true)
                ModelState.AddModelError("WeightOnRelease", "Weight On Release is Required");

            if (ModelState.Count == 0) // if valid
            {
                var a = db.Admissions.Include(o => o.Prisoner).First(o => o.AdmissionId == admission.AdmissionId);

                a.HealthOnRelease = admission.HealthOnRelease;
                a.WeightOnRelease = admission.WeightOnRelease;
                a.MedicalRemarks = admission.MedicalRemarks;

                db.SaveChanges();

                return RedirectToMedicalTreatments();
            }

            PopulateDropDowns(admission);
            return View(admission);
        }

        //
        // GET: /Admission/

        public ActionResult Index()
        {
            var admissions = db.Admissions.Include(a => a.Barrack).Include(a => a.CommunicableDisease).Include(a => a.Prisoner).Where(o => o.PrisonerId == PrisonerId).ToList();

            if (admissions != null && admissions.Count == 1)
            {
                AdmissionId = admissions[0].AdmissionId;
                return RedirectToAction("Details");
            }

            return View(admissions);
        }

        //
        // GET: /Admission/Details/5

        public ActionResult Details(int AdmissionId = 0)
        {
            Admission admission = db.Admissions.Include(o => o.FIRs).FirstOrDefault(o => o.AdmissionId == AdmissionId);

            if (admission == null)
                return HttpNotFound();

            return View(admission);
        }

        //
        // GET: /Admission/Create

        public ActionResult Create()
        {
            Admission admission = new Admission();
            PopulateDropDowns(admission);

            return View(admission);
        }

        //
        // POST: /Admission/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admission admission)
        {
            if (ModelState.IsValid)
            {
                db.Admissions.Add(admission);
                db.SaveChanges();
                AdmissionId = admission.AdmissionId;
                return RedirectToAction("Details");
            }

            PopulateDropDowns(admission);
            return View(admission);
        }

        //
        // GET: /Admission/Edit/5

        public ActionResult Edit(int AdmissionId = 0)
        {
            Admission admission = db.Admissions.Find(AdmissionId);

            if (admission == null)
                return HttpNotFound();

            PopulateDropDowns(admission);
            return View(admission);
        }

        //
        // POST: /Admission/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Admission admission)
        {
            if (ModelState.IsValid)
            {
                var admissionCheckIn = db.CheckInOuts.OrderByDescending(o => o.DateOfCheckInOut).FirstOrDefault(o => o.AdmissionId == admission.AdmissionId && (o.Type == CheckInOutType.Admission || o.Type == CheckInOutType.ReAdmission));

                if (admissionCheckIn != null)
                    admissionCheckIn.DateOfCheckInOut = admission.DateOfAdmission;

                var remandCheckIn = db.CheckInOuts.OrderByDescending(o => o.DateOfCheckInOut).FirstOrDefault(o => o.AdmissionId == admission.AdmissionId && o.Type == CheckInOutType.PhysicalRemandIn);

                if (remandCheckIn != null && admission.DateOfRemand != null)
                    remandCheckIn.DateOfCheckInOut = admission.DateOfRemand.Value;

                var releaseCheckOut = db.CheckInOuts.OrderByDescending(o => o.DateOfCheckInOut).FirstOrDefault(o => o.AdmissionId == admission.AdmissionId && o.Type == CheckInOutType.Release);

                if (releaseCheckOut != null && admission.DateOfRelease != null)
                { 
                    releaseCheckOut.DateOfCheckInOut = admission.DateOfRelease.Value;
                    releaseCheckOut.PrisonerNumber = admission.PrisonerNumber;
                    releaseCheckOut.JudgeTypeId = admission.JudgeTypeId;
                    releaseCheckOut.Authority = admission.AuthorityForRelease;
                    releaseCheckOut.Description = admission.Remarks;
                }

                db.Entry(admission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            PopulateDropDowns(admission);
            return View(admission);
        }
        //
        // GET: /Admission/Delete/5

        public ActionResult Housing(int PrisonerId = 0, int AdmissionId = 0)
        {
            if (PrisonerId <= 0)
                return HttpNotFound();

            Admission admission = null;

            if (AdmissionId <= 0) // Get latest admission
                admission = GetLatestAdmission(PrisonerId);
            else
                admission = db.Admissions.Include(o => o.Prisoner).First(o => o.AdmissionId == AdmissionId);

            if (admission == null)
            {
                Error("Admission record not found!");
                return View();
            }

            AdmissionId = admission.AdmissionId;

            PopulateDropDowns(admission);
            ViewBag.DiseaseId = null; // Don't fetch Diseases

            return View(admission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Housing(Admission admission)
        {
            var adm = db.Admissions.First(o => o.AdmissionId == admission.AdmissionId);

            adm.BarrackId = admission.BarrackId;
            adm.BlockNumber = admission.BlockNumber;
            adm.CellNumber = admission.CellNumber;

            db.SaveChanges();

            return RedirectToAction("Details", "Prisoner");
        }

        public ActionResult Delete(int AdmissionId = 0)
        {
            Admission admission = db.Admissions.Find(AdmissionId);

            if (admission == null)
                return HttpNotFound();

            return View(admission);
        }

        //
        // POST: /Admission/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int AdmissionId)
        {
            Admission admission = db.Admissions.Find(AdmissionId);
            db.Admissions.Remove(admission);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}