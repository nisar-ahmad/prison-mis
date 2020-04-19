using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PRS.Models;

namespace PRS.Controllers
{
    public class AppealController : PRSBaseController
    {
        private Appeal CreateAppeal()
        {
            var appeal = new Appeal();
            appeal.PrisonerId = PrisonerId;

            return appeal;
        }

        private void PopulateDropDowns(Appeal appeal)
        {
            ViewBag.CourtId = new SelectList(db.Courts, "CourtId", "Name", appeal.CourtId);
        }

        // GET: Appeal
        public ActionResult Index()
        {
            var appeals = db.Appeals.Include(a => a.Court).Include(a => a.Prisoner);
            return View(appeals.ToList());
        }

        // GET: Appeal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appeal appeal = db.Appeals.Find(id);
            if (appeal == null)
            {
                return HttpNotFound();
            }
            return View(appeal);
        }

        // GET: Appeal/Create
        public ActionResult Create()
        {
            var appeal = CreateAppeal();
            PopulateDropDowns(appeal);
            return View(appeal);
        }

        // POST: Appeal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppealId,PrisonerId,DateOfAppeal,CourtId,Status,DateOfResult,Description")] Appeal appeal)
        {
            if (ModelState.IsValid)
            {
                db.Appeals.Add(appeal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ValidationError();
            PopulateDropDowns(appeal);
            return View(appeal);
        }

        // GET: Appeal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appeal appeal = db.Appeals.Find(id);
            if (appeal == null)
            {
                return HttpNotFound();
            }
            PopulateDropDowns(appeal);
            return View(appeal);
        }

        // POST: Appeal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppealId,PrisonerId,DateOfAppeal,CourtId,Status,DateOfResult,Description")] Appeal appeal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appeal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ValidationError();
            PopulateDropDowns(appeal);
            return View(appeal);
        }

        // GET: Appeal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appeal appeal = db.Appeals.Find(id);
            if (appeal == null)
            {
                return HttpNotFound();
            }
            return View(appeal);
        }

        // POST: Appeal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appeal appeal = db.Appeals.Find(id);
            db.Appeals.Remove(appeal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
