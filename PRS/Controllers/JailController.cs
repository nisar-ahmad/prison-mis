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
    public class JailController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /Jail/

        public ActionResult Index()
        {
            var jails = db.Jails.Include(j => j.District);
            return View(jails.ToList());
        }

        //
        // GET: /Jail/Details/5

        public ActionResult Details(int id = 0)
        {
            Jail jail = db.Jails.Find(id);
            if (jail == null)
            {
                return HttpNotFound();
            }
            return View(jail);
        }

        //
        // GET: /Jail/Create

        public ActionResult Create()
        {
            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "Name");
            return View();
        }

        //
        // POST: /Jail/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Jail jail)
        {
            if (ModelState.IsValid)
            {
                db.Jails.Add(jail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "Name", jail.DistrictId);
            return View(jail);
        }

        //
        // GET: /Jail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Jail jail = db.Jails.Find(id);
            if (jail == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "Name", jail.DistrictId);
            return View(jail);
        }

        //
        // POST: /Jail/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Jail jail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "Name", jail.DistrictId);
            return View(jail);
        }

        //
        // GET: /Jail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Jail jail = db.Jails.Find(id);
            if (jail == null)
            {
                return HttpNotFound();
            }
            return View(jail);
        }

        //
        // POST: /Jail/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jail jail = db.Jails.Find(id);
            db.Jails.Remove(jail);
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