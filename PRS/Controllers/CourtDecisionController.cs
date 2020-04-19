using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRS.Models;
using System.Collections.Specialized;

namespace PRS.Controllers
{
    public class NameValuePair
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class CourtDecisionController : PRSBaseController
    {
        private CourtDecision CreateCourtDecision()
        {
            var courtdecision = new CourtDecision();
            courtdecision.Is382BApplied = true;
            courtdecision.PrisonerId = PrisonerId;
            courtdecision.AdmissionId = AdmissionId;

            return courtdecision;
        }

        private void PopulateDropDowns(CourtDecision courtdecision)
        {
            IQueryable<FIR> firList = null;
            IQueryable<CourtHearing> courtHearingList = null;

            if (courtdecision.AdmissionId > 0)
            {
                firList = db.FIRs.Where(o => o.AdmissionId == courtdecision.AdmissionId);
                courtHearingList = db.CourtHearings.Where(o => o.AdmissionId == courtdecision.AdmissionId);
            }
            else
            {
                firList = db.FIRs.Where(o => o.Admission.PrisonerId == courtdecision.PrisonerId);
                courtHearingList = db.CourtHearings.Where(o => o.Admission.PrisonerId == courtdecision.PrisonerId);
            }

            if (courtHearingList != null)
            {
                var list = new List<NameValuePair>();

                foreach (var c in courtHearingList)
                {
                    var nameValuePair = new NameValuePair { Value = c.CourtHearingId, Name = string.Format("{0} - {1:dd-MMM-yyyy} - {2}", c.FIR.FIRNumber, c.DateOfHearing, c.Court.Name) };

                    if (c.JudgeType != null)
                        nameValuePair.Name += string.Format(" - {0}", c.JudgeType.Name);

                    list.Add(nameValuePair);
                }

                ViewBag.FIRId = new SelectList(firList, "FIRId", "FIRNumber", courtdecision.FIRId);
                ViewBag.CourtHearingId = new SelectList(list, "Value", "Name", courtdecision.CourtHearingId);
            }

            ViewBag.DecisionStatus = new SelectList(Enum.GetValues(typeof(DecisionStatus)), courtdecision.DecisionStatus);
            ViewBag.CourtDecisionType = new SelectList(Enum.GetValues(typeof(CourtDecisionType)), courtdecision.CourtDecisionType);
        }

        //
        // GET: /CourtDecision/

        public ActionResult Index()
        {
            var courtdecisions = db.CourtDecisions.Include(c => c.FIR).Include(c => c.CourtHearing).Include(c => c.Admission).Include(c => c.Prisoner).Where(o => o.PrisonerId == PrisonerId);
            return View(courtdecisions.ToList());
        }

        //
        // GET: /CourtDecision/Details/5

        public ActionResult Details(int id = 0)
        {
            CourtDecision courtdecision = db.CourtDecisions.Find(id);

            if (courtdecision == null)
                return HttpNotFound();
            
            return View(courtdecision);
        }

        //
        // GET: /CourtDecision/Create
        
        public ActionResult Create()
        {
            var admission = GetLatestAdmission(); //db.Admissions.OrderByDescending(o => o.AdmissionId).FirstOrDefault(o => o.PrisonerId == courtdecision.PrisonerId);

            CourtDecision courtdecision = CreateCourtDecision();
            courtdecision.DateOfUnderTrialStart = admission.DateOfAdmission;

            if (admission.DateOfReleaseWithFullSentence != null)
                ViewBag.DateOfReleaseWithFullSentence = admission.DateOfReleaseWithFullSentence.Value.ToString("dd-MMM-yyy");
            else
                ViewBag.DateOfReleaseWithFullSentence = "";

            PopulateDropDowns(courtdecision);
            return View(courtdecision);
        }

        //
        // POST: /CourtDecision/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourtDecision courtdecision, DateTime? DateOfReleaseWithFullSentence)
        {
            if (ModelState.IsValid)
            {
                var admission = db.Admissions.Include(o => o.Prisoner).OrderByDescending(o => o.AdmissionId).FirstOrDefault(o => o.PrisonerId == courtdecision.PrisonerId);
                admission.DateOfReleaseWithFullSentence = DateOfReleaseWithFullSentence;

                courtdecision.AdmissionId = admission.AdmissionId;

                if (courtdecision.DecisionStatus == DecisionStatus.Sentenced)
                    admission.Prisoner.Category = PrisonerCategory.Convicted;

                db.CourtDecisions.Add(courtdecision);
                CalculateSentence();
                db.SaveChanges();

                return RedirectToAction("Details", new { id = courtdecision.CourtDecisionId, PrisonerId = PrisonerId });
            }

            PopulateDropDowns(courtdecision);
            return View(courtdecision);
        }

        //
        // GET: /CourtDecision/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CourtDecision courtdecision = db.CourtDecisions.Find(id);

            if (courtdecision == null)
                return HttpNotFound();

            var admission = db.Admissions.OrderByDescending(o => o.AdmissionId).FirstOrDefault(o => o.PrisonerId == courtdecision.PrisonerId);

            if (admission.DateOfReleaseWithFullSentence != null)
                ViewBag.DateOfReleaseWithFullSentence = admission.DateOfReleaseWithFullSentence.Value.ToString("dd-MMM-yyyy");
            else
                ViewBag.DateOfReleaseWithFullSentence = "";

            PopulateDropDowns(courtdecision);
            return View(courtdecision);
        }

        //
        // POST: /CourtDecision/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourtDecision courtdecision, DateTime? DateOfReleaseWithFullSentence)
        {
            if (ModelState.IsValid)
            {
                var admission = db.Admissions.Include(o => o.Prisoner).OrderByDescending(o => o.AdmissionId).FirstOrDefault(o => o.PrisonerId == courtdecision.PrisonerId);
                admission.DateOfReleaseWithFullSentence = DateOfReleaseWithFullSentence;

                if (courtdecision.DecisionStatus == DecisionStatus.Sentenced)
                    admission.Prisoner.Category = PrisonerCategory.Convicted;
            
                db.Entry(courtdecision).State = EntityState.Modified;                
                db.SaveChanges();
                CalculateSentence();
                return RedirectToAction("Details", new { id = courtdecision.CourtDecisionId, PrisonerId = PrisonerId });
            }

            PopulateDropDowns(courtdecision);
            return View(courtdecision);
        }

        //
        // GET: /CourtDecision/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CourtDecision courtdecision = db.CourtDecisions.Find(id);

            if (courtdecision == null)
                return HttpNotFound();
            
            return View(courtdecision);
        }

        //
        // POST: /CourtDecision/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourtDecision courtdecision = db.CourtDecisions.Find(id);
            db.CourtDecisions.Remove(courtdecision);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}