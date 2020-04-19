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
    public class CourtController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /Court/

        public ActionResult Index()
        {
            var courts = db.Courts.Include(c => c.CourtType).Include(c => c.District);
            return View(courts.ToList());
        }

        //
        // GET: /Court/Details/5

        public ActionResult Details(int id = 0)
        {
            Court court = db.Courts.Find(id);
            if (court == null)
            {
                return HttpNotFound();
            }
            return View(court);
        }

        //
        // GET: /Court/Create

        public ActionResult Create()
        {
            ViewBag.CourtTypeId = new SelectList(db.CourtTypes, "CourtTypeId", "Name");
            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "Name");
            return View();
        }

        //
        // POST: /Court/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Court court)
        {
            if (ModelState.IsValid)
            {
                db.Courts.Add(court);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourtTypeId = new SelectList(db.CourtTypes, "CourtTypeId", "Name", court.CourtTypeId);
            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "Name", court.DistrictId);
            return View(court);
        }

        //
        // GET: /Court/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Court court = db.Courts.Find(id);
            if (court == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourtTypeId = new SelectList(db.CourtTypes, "CourtTypeId", "Name", court.CourtTypeId);
            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "Name", court.DistrictId);
            return View(court);
        }

        //
        // POST: /Court/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Court court)
        {
            if (ModelState.IsValid)
            {
                db.Entry(court).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourtTypeId = new SelectList(db.CourtTypes, "CourtTypeId", "Name", court.CourtTypeId);
            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "Name", court.DistrictId);
            return View(court);
        }

        //
        // GET: /Court/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Court court = db.Courts.Find(id);
            if (court == null)
            {
                return HttpNotFound();
            }
            return View(court);
        }

        //
        // POST: /Court/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Court court = db.Courts.Find(id);
            db.Courts.Remove(court);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}