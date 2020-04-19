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
    public class FIRController : PRSBaseController
    {
        private void PopulateDropDowns(FIR fir)
        {
            var settings = db.Setttings.First();

            int provinceId = settings.ProvinceId;
            int districtId = settings.DistrictId;

            if (fir.PoliceStationId > 0)
            {
                var dist = db.PoliceStations.Include(o => o.District.Province).First(o => o.PoliceStationId == fir.PoliceStationId).District;
                districtId = dist.DistrictId;
                provinceId = dist.ProvinceId;
            }

            ViewBag.ProvinceId = new SelectList(db.Provinces.Where(o => o.CountryId == 157), "ProvinceId", "Name", provinceId);
            ViewBag.DistrictId = new SelectList(db.Districts.Where(o => o.ProvinceId == provinceId), "DistrictId", "Name", districtId);

            if (fir.PoliceStationId > 0)
                ViewBag.PSName = db.PoliceStations.FirstOrDefault(o => o.PoliceStationId == fir.PoliceStationId).Name;

            ViewBag.DecisionStatus = new SelectList(Enum.GetValues(typeof(DecisionStatus)), fir.DecisionStatus);
            ViewBag.FIRJudgeTypeId = new SelectList(db.JudgeTypes, "JudgeTypeId", "Name", fir.JudgeTypeId);
        }

        private void PopulateDropDowns(CourtHearing courthearing)
        {
            ViewBag.CourtId = new SelectList(db.Courts, "CourtId", "Name", courthearing.CourtId);
            ViewBag.CourtHearingJudgeTypeId = new SelectList(db.JudgeTypes, "JudgeTypeId", "Name", courthearing.JudgeTypeId);

            if (courthearing.JudgeId > 0)
                ViewBag.JudgeName = db.Judges.FirstOrDefault(o => o.JudgeId == courthearing.JudgeId).Name;
        }

        public JsonResult FIRSections(int firId = 0)
        {
            List<object> sectionList = new List<object>();

            ICollection<Section> firSections = null;

            if (firId > 0)
                firSections = db.FIRs.Include(o => o.Sections).FirstOrDefault(o => o.FIRId == firId).Sections;

            foreach (Section s in db.Sections.Include(o => o.Act).ToList())
            {
                sectionList.Add(new { Value = s.SectionId, Text = s.Act.ShortName + " " + s.Name.Trim(), Selected = (firSections != null && firSections.Contains(s) ? "selected" : null) });
            }

            return Json(sectionList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Admit(int PrisonerId = 0, int AdmissionId = 0, int FIRId = 0)
        {
            if (PrisonerId <= 0)
                return HttpNotFound();

            AdmissionFIR admFIR = new AdmissionFIR(PrisonerId, AdmissionId);

            Admission admission = null;

            if (AdmissionId > 0)
            {
                admission = db.Admissions.First(o => o.AdmissionId == AdmissionId);
                admFIR.FIRs = db.FIRs.Include(o => o.Sections).Include(o => o.JudgeType).Where(o => o.AdmissionId == AdmissionId).ToList();

                if (Request["Remand"] == "1" && admission.DateOfRemand == null)
                    admission.DateOfRemand = DateTime.Now;
            }
            else // Its a new admission!
                admission = new Admission();

            admFIR.Admission = admission;

            PopulateDropDowns(admFIR.FIR);
            PopulateDropDowns(admFIR.CourtHearing);

            if (Request["Remand"] == "1")
                ViewBag.Title = "Remand Return";
            else
                ViewBag.Title = "Admission Step 2 of 2";

            return View(admFIR);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admit(AdmissionFIR admFIR, int[] sectionList, string PSName, string JudgeName, int DistrictId = 0, string FinishButton = "")
        {
            if (FinishButton != "")
            {
                if (Request["Remand"] == "1" && admFIR.Admission.DateOfRemand == null)
                    ModelState.AddModelError("DateOfRemand", "Date of Remand is Required");

                var count = db.FIRs.Count(o => o.AdmissionId == AdmissionId);

                if(count == 0)
                    ModelState.AddModelError("FIRs", "No FIR Added!");

                if(ModelState.IsValid)
                {
                    var admission = db.Admissions.First(o => o.AdmissionId == AdmissionId);
                    admission.PrisonerNumber = admFIR.Admission.PrisonerNumber;
                    admission.DateOfAdmission = admFIR.Admission.DateOfAdmission;

                    int aCount = db.Admissions.Count(o => o.PrisonerId == PrisonerId);

                    var c = new CheckInOut();

                    c.Status = CheckInOutStatus.CheckIn;
                    c.AdmissionId = AdmissionId;
                    c.PrisonerId = PrisonerId;
                    c.PrisonerNumber = admFIR.Admission.PrisonerNumber;

                    var cp = db.CourtHearings.Include(o => o.Judge).OrderByDescending(o => o.DateOfCourtOrder).FirstOrDefault(o => o.AdmissionId == AdmissionId);

                    c.JudgeTypeId = cp.JudgeTypeId;
                    c.Authority = cp.Judge.Name;
                    c.DateOfCheckInOut = admFIR.Admission.DateOfAdmission;

                    if (Request["Remand"] == "1")
                    {
                        admission.DateOfRemand = admFIR.Admission.DateOfRemand;

                        c.Type = CheckInOutType.PhysicalRemandIn;
                        c.DateOfCheckInOut = admission.DateOfRemand.Value;
                    }
                    else if (aCount > 1)
                        c.Type = CheckInOutType.ReAdmission;
                    else
                        c.Type = CheckInOutType.Admission;

                    db.CheckInOuts.Add(c);
                    db.SaveChanges();

                    Remand = 0;
                    return RedirectToAction("Create", "FIR");
                }
            }
            else
            {
                if (Request["Remand"] == "1" && admFIR.Admission.DateOfRemand == null)
                    ModelState.AddModelError("DateOfRemand", "Date of Remand is Required");

                if (string.IsNullOrWhiteSpace(PSName) == true)
                    ModelState.AddModelError("PoliceStationId", "Police Station is Required");

                if (sectionList == null || sectionList.Length == 0)
                    ModelState.AddModelError("UnderSections", "FIR Under Sections are Required");

                if (string.IsNullOrWhiteSpace(JudgeName) == true)
                    ModelState.AddModelError("JudgeId", "Judge Name is Required");

                if (admFIR.CourtHearing.JudgeTypeId == null)
                    ModelState.AddModelError("JudgeTypeId", "Judge Type is Required");
            }

            if (ModelState.IsValid)
            {
                if (admFIR.Admission.AdmissionId == 0)
                {
                    admFIR.Admission.PrisonerId = PrisonerId;
                    db.Admissions.Add(admFIR.Admission);
                    db.SaveChanges();

                    AdmissionId = admFIR.Admission.AdmissionId;
                    admFIR.FIR.AdmissionId = AdmissionId;
                }
                else
                {
                    var admission = db.Admissions.First(o => o.AdmissionId == admFIR.Admission.AdmissionId);
                    admission.PrisonerNumber = admFIR.Admission.PrisonerNumber;
                    admission.DateOfAdmission = admFIR.Admission.DateOfAdmission;

                    if (Request["Remand"] == "1")
                        admission.DateOfRemand = admFIR.Admission.DateOfRemand;
                }

                if (DistrictId > 0 && string.IsNullOrWhiteSpace(PSName) == false && admFIR.FIR.PoliceStationId == 0)
                {
                    PoliceStation ps = new PoliceStation();
                    ps.DistrictId = DistrictId;
                    ps.Name = PSName;
                    db.PoliceStations.Add(ps);
                    admFIR.FIR.PoliceStationId = ps.PoliceStationId;
                }

                if (admFIR.CourtHearing.CourtId > 0 && string.IsNullOrWhiteSpace(JudgeName) == false && admFIR.CourtHearing.JudgeId == 0)
                {
                    Judge judge = new Judge();
                    judge.CourtId = admFIR.CourtHearing.CourtId;
                    judge.Name = JudgeName;
                    db.Judges.Add(judge);
                    admFIR.CourtHearing.JudgeId = judge.JudgeId;
                }

                admFIR.FIR.Sections = db.Sections.Where(o => sectionList.Contains(o.SectionId)).ToList();

                db.FIRs.Add(admFIR.FIR);

                admFIR.CourtHearing.AdmissionId = AdmissionId;
                admFIR.CourtHearing.PrisonerId = PrisonerId;
                admFIR.CourtHearing.FIRId = admFIR.FIR.FIRId;
                db.CourtHearings.Add(admFIR.CourtHearing);

                db.SaveChanges();
                return RedirectToAction("Admit");
            }

            if (AdmissionId > 0)
                admFIR.FIRs = db.FIRs.Include(o => o.Sections).Include(o => o.JudgeType).Where(o => o.AdmissionId == AdmissionId).ToList();

            if (sectionList != null && sectionList.Length > 0)
                admFIR.FIR.Sections = db.Sections.Where(o => sectionList.Contains(o.SectionId)).ToList();

            PopulateDropDowns(admFIR.FIR);
            PopulateDropDowns(admFIR.CourtHearing);

            if (Request["Remand"] == "1")
                ViewBag.Title = "Remand Return";
            else
                ViewBag.Title = "Admission Step 2 of 2";

            return View(admFIR);
        }

        public ActionResult Index()
        {
            var firs = db.FIRs.Include(f => f.PoliceStation).Include(o => o.JudgeType).Include(f => f.Admission).Where(o => o.AdmissionId == AdmissionId);
            return View(firs.ToList());
        }

        //
        // GET: /FIR/Details/5

        public ActionResult Details(int id = 0)
        {
            FIR fir = db.FIRs.Find(id);

            if (fir == null)
                return HttpNotFound();

            return View(fir);
        }

        //
        // GET: /FIR/Create

        public ActionResult Create(int id = 0)
        {
            if (PrisonerId <= 0)
                return HttpNotFound();

            if(AdmissionId <= 0)
                AdmissionId = GetLatestAdmissionId(PrisonerId);

            AdmissionFIR admFIR = new AdmissionFIR(PrisonerId, AdmissionId);

            if(id > 0) // Edit
            {
                admFIR.FIR = db.FIRs.Find(id);
                admFIR.CourtHearing = db.CourtHearings.First(o=> o.FIRId == id);

                ViewBag.Mode = "EDIT";
            }
            else
                ViewBag.Mode = "NEW";

            admFIR.FIRs = db.FIRs.Include(o => o.Sections).Include(o => o.JudgeType).Where(o => o.AdmissionId == AdmissionId).ToList();

            PopulateDropDowns(admFIR.FIR);
            PopulateDropDowns(admFIR.CourtHearing);

            return View(admFIR);
        }

        //
        // POST: /FIR/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdmissionFIR admFIR, int[] sectionList, string PSName, string JudgeName, int DistrictId = 0)
        {
            if (string.IsNullOrWhiteSpace(PSName) == true)
                ModelState.AddModelError("PoliceStationId", "Police Station is Required");

            if (sectionList == null || sectionList.Length == 0)
                ModelState.AddModelError("UnderSections", "FIR Under Sections are Required");

            if (string.IsNullOrWhiteSpace(JudgeName) == true)
                ModelState.AddModelError("JudgeId", "Judge Name is Required");

            if (admFIR.CourtHearing.JudgeTypeId == null)
                ModelState.AddModelError("JudgeTypeId", "Judge Type is Required");

            if(admFIR.FIR.DecisionStatus != DecisionStatus.Pending)
            {
                if (admFIR.FIR.DecisionDate == null)
                    ModelState.AddModelError("DecisionDate", string.Format("Decision Date is required when Decision Status is '{0}'", admFIR.FIR.DecisionStatus));

                if (admFIR.FIR.JudgeTypeId == null)
                    ModelState.AddModelError("DecisionJudgeTypeId", string.Format("Decision Judge Type is required when Decision Status is '{0}'", admFIR.FIR.DecisionStatus));

                if (string.IsNullOrWhiteSpace(admFIR.FIR.DecisionAuthority) == true)
                    ModelState.AddModelError("DecisionAuthority", string.Format("Decision Authority Name is required when Decision Status is '{0}'", admFIR.FIR.DecisionStatus));
            }

            if (ModelState.IsValid)
            {
                if (DistrictId > 0 && string.IsNullOrWhiteSpace(PSName) == false && admFIR.FIR.PoliceStationId == 0)
                {
                    PoliceStation ps = new PoliceStation();
                    ps.DistrictId = DistrictId;
                    ps.Name = PSName;
                    db.PoliceStations.Add(ps);
                    admFIR.FIR.PoliceStationId = ps.PoliceStationId;
                }

                if (admFIR.CourtHearing.CourtId > 0 && string.IsNullOrWhiteSpace(JudgeName) == false && admFIR.CourtHearing.JudgeId == 0)
                {
                    Judge judge = new Judge();
                    judge.CourtId = admFIR.CourtHearing.CourtId;
                    judge.Name = JudgeName;
                    db.Judges.Add(judge);
                    admFIR.CourtHearing.JudgeId = judge.JudgeId;
                }

                if(admFIR.FIR.FIRId > 0) // Edit
                {
                    db.Entry(admFIR.FIR).State = EntityState.Modified;
                    db.Entry(admFIR.CourtHearing).State = EntityState.Modified;

                    var sections = db.Sections.Where(o => sectionList.Contains(o.SectionId));

                    var FIR = db.FIRs.Include(i => i.Sections).FirstOrDefault(o => o.FIRId == admFIR.FIR.FIRId);
                    var firSections = FIR.Sections.ToList();

                    foreach (Section s in firSections)
                    {
                        if (sectionList == null || sectionList.Length == 0 || sectionList.Contains(s.SectionId) == false)
                            FIR.Sections.Remove(s);
                    }

                    foreach (Section ss in sections)
                    {
                        if (FIR.Sections.Contains(ss) == false)
                            FIR.Sections.Add(ss);
                    }

                    ViewBag.Mode = "EDIT";
                }
                else
                {
                    admFIR.FIR.Sections = db.Sections.Where(o => sectionList.Contains(o.SectionId)).ToList();

                    db.FIRs.Add(admFIR.FIR);
                    db.CourtHearings.Add(admFIR.CourtHearing);

                    ViewBag.Mode = "NEW";
                }

                admFIR.CourtHearing.AdmissionId = AdmissionId;
                admFIR.CourtHearing.PrisonerId = PrisonerId;
                admFIR.CourtHearing.FIRId = admFIR.FIR.FIRId;

                db.SaveChanges();
                return RedirectToAction("Create", new { id="", PrisonerId = PrisonerId, AdmissionId = AdmissionId });
            }

            admFIR.FIRs = db.FIRs.Include(o => o.Sections).Include(o => o.JudgeType).Where(o => o.AdmissionId == AdmissionId).ToList();

            PopulateDropDowns(admFIR.FIR);
            PopulateDropDowns(admFIR.CourtHearing);

            return View(admFIR);
        }

        //
        // GET: /FIR/Edit/5

        public ActionResult Edit(int id = 0)
        {
            FIR fir = db.FIRs.Find(id);

            if (fir == null)
                return HttpNotFound();

            PopulateDropDowns(fir);
            return View(fir);
        }

        //
        // POST: /FIR/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FIR fir, int[] sectionList, string PSName, int DistrictId = 0)
        {
            if (string.IsNullOrWhiteSpace(PSName) == true)
            {
                ModelState.Clear();
                ModelState.AddModelError("PoliceStationId", "Police Station is Required");
            }

            if (sectionList == null || sectionList.Length == 0)
                ModelState.AddModelError("UnderSections", "FIR Under Sections are Required");

            if (fir.JudgeTypeId == null)
                ModelState.AddModelError("JudgeTypeId", "Judge Type is Required");

            if (ModelState.IsValid)
            {
                if (DistrictId > 0 && string.IsNullOrWhiteSpace(PSName) == false && fir.PoliceStationId == 0)
                {
                    PoliceStation ps = new PoliceStation();
                    ps.DistrictId = DistrictId;
                    ps.Name = PSName;
                    db.PoliceStations.Add(ps);
                    db.SaveChanges();

                    fir.PoliceStationId = ps.PoliceStationId;
                }

                db.Entry(fir).State = EntityState.Modified;

                var sections = db.Sections.Where(o => sectionList.Contains(o.SectionId));

                var FIR = db.FIRs.Include(i => i.Sections).FirstOrDefault(o => o.FIRId == fir.FIRId);
                var firSections = FIR.Sections.ToList();

                foreach (Section s in firSections)
                {
                    if (sectionList == null || sectionList.Length == 0 || sectionList.Contains(s.SectionId) == false)
                        FIR.Sections.Remove(s);
                }

                foreach (Section ss in sections)
                {
                    if (FIR.Sections.Contains(ss) == false)
                        FIR.Sections.Add(ss);
                }

                db.SaveChanges();
                return RedirectToAction("Details", "Admission");
            }

            PopulateDropDowns(fir);
            return View(fir);
        }

        //
        // GET: /FIR/Delete/5

        public ActionResult Delete(int id = 0)
        {
            FIR fir = db.FIRs.Find(id);

            if (fir == null)
                return HttpNotFound();

            return View(fir);
        }

        //
        // POST: /FIR/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FIR fir = db.FIRs.Find(id);
            db.FIRs.Remove(fir);
            db.SaveChanges();
            return RedirectToAction("Details", "Admission");
        }
    }
}