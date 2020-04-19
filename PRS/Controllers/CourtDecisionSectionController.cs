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
    public class CourtDecisionSectionController : PRSBaseController
    {
        public JsonResult CalculateDateOfRelease(int PrisonerId = 0)
        {
           var date = CalculateSentence(PrisonerId);
           return Json(date, JsonRequestBehavior.AllowGet);
        }

        private CourtDecisionSection CreateCourtDecisionSection()
        {
            var c = new CourtDecisionSection();
            c.CourtDecisionId = Convert.ToInt32(Request["CourtDecisionId"]);
            c.FIR = db.CourtDecisions.Include(o => o.FIR.Sections).FirstOrDefault(o => o.CourtDecisionId == c.CourtDecisionId).FIR;
            c.FIRId = c.FIR.FIRId;
            c.Is382BApplied = true;
            return c;
        }

        private void PopulateDropDowns(CourtDecisionSection c)
        {
            ViewBag.SectionId = new SelectList(c.FIR.Sections, "SectionId", "Name", c.SectionId);
            ViewBag.SentenceType = new SelectList(Enum.GetValues(typeof(SentenceType)), c.SentenceType);
            ViewBag.FurtherSentenceType = new SelectList(Enum.GetValues(typeof(SentenceType)), c.FurtherSentenceType);
            ViewBag.CourtDecisionType = new SelectList(Enum.GetValues(typeof(CourtDecisionType)).Cast<CourtDecisionType>()
                                                       .Except(new [] { CourtDecisionType.NotApplicable } ), c.CourtDecisionType);
            ViewBag.SectionDecisionType = new SelectList(Enum.GetValues(typeof(SectionDecisionType)), c.SectionDecisionType);
        }

        //
        // GET: /CourtDecisionSection/

        public ActionResult Index()
        {
            var cs = db.CourtDecisionSections.Include(c => c.FIR).Include(c => c.Section).Where(o => o.CourtDecision.PrisonerId == PrisonerId);
            return View(cs.ToList());
        }

        //
        // GET: /CourtDecisionSection/Details/5

        public ActionResult Details(int id = 0)
        {
            CourtDecisionSection c = db.CourtDecisionSections.Find(id);

            if (c == null)
                return HttpNotFound();
            
            return View(c);
        }

        //
        // GET: /CourtDecisionSection/Create
        
        public ActionResult Create()
        {
            CourtDecisionSection c = CreateCourtDecisionSection();
            PopulateDropDowns(c);
            return View(c);
        }

        //
        // POST: /CourtDecisionSection/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourtDecisionSection c)
        {
            if (ModelState.IsValid)
            {
                //c.AdmissionId = db.Admissions.OrderByDescending(o => o.AdmissionId).FirstOrDefault(o => o.PrisonerId == c.PrisonerId).AdmissionId;
                db.CourtDecisionSections.Add(c);
                db.SaveChanges();
                CalculateDateOfRelease();

                return RedirectToAction("Details", "CourtDecision", new { id = c.CourtDecisionId, PrisonerId = PrisonerId });
            }

            PopulateDropDowns(c);
            return View(c);
        }

        //
        // GET: /CourtDecisionSection/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CourtDecisionSection c = db.CourtDecisionSections.Find(id);

            if (c == null)
                return HttpNotFound();

            PopulateDropDowns(c);
            return View(c);
        }

        //
        // POST: /CourtDecisionSection/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourtDecisionSection c)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
                CalculateDateOfRelease();

                return RedirectToAction("Details", "CourtDecision", new { id = c.CourtDecisionId, PrisonerId = PrisonerId });
            }

            PopulateDropDowns(c);
            return View(c);
        }

        //
        // GET: /CourtDecisionSection/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CourtDecisionSection c = db.CourtDecisionSections.Find(id);

            if (c == null)
                return HttpNotFound();
            
            return View(c);
        }

        //
        // POST: /CourtDecisionSection/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourtDecisionSection c = db.CourtDecisionSections.Find(id);
            db.CourtDecisionSections.Remove(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}