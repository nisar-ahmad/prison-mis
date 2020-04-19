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
    public class CourtTypeController : Controller
    {
        private PRSContext db = new PRSContext();

        // GET: CourtType
        public ActionResult Index()
        {
            return View(db.CourtTypes.ToList());
        }

        // GET: CourtType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourtType courtType = db.CourtTypes.Find(id);
            if (courtType == null)
            {
                return HttpNotFound();
            }
            return View(courtType);
        }

        // GET: CourtType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourtType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourtTypeId,Name,Description")] CourtType courtType)
        {
            if (ModelState.IsValid)
            {
                db.CourtTypes.Add(courtType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courtType);
        }

        // GET: CourtType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourtType courtType = db.CourtTypes.Find(id);
            if (courtType == null)
            {
                return HttpNotFound();
            }
            return View(courtType);
        }

        // POST: CourtType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourtTypeId,Name,Description")] CourtType courtType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courtType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courtType);
        }

        // GET: CourtType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourtType courtType = db.CourtTypes.Find(id);
            if (courtType == null)
            {
                return HttpNotFound();
            }
            return View(courtType);
        }

        // POST: CourtType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourtType courtType = db.CourtTypes.Find(id);
            db.CourtTypes.Remove(courtType);
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
