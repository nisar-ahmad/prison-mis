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
    public class SettingsController : Controller
    {
        private PRSContext db = new PRSContext();

        private void PopulateDropDowns(Settings settings)
        {
            ViewBag.JailId = new SelectList(db.Jails, "JailId", "Name", settings.JailId);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "ProvinceId", "Name", settings.ProvinceId);
            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "Name", settings.DistrictId);
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", settings.CityId);
        }

        //
        // GET: /Settings/

        public ActionResult Index()
        {
            var setttings = db.Setttings.Include(s => s.Jail);
            return View(setttings.ToList());
        }

        //
        // GET: /Settings/Details/5

        public ActionResult Details(int id = 0)
        {
            Settings settings = db.Setttings.Find(id);
            if (settings == null)
            {
                return HttpNotFound();
            }
            return View(settings);
        }

        //
        // GET: /Settings/Create

        public ActionResult Create()
        {
            ViewBag.JailId = new SelectList(db.Jails, "JailId", "Name");
            return View();
        }

        //
        // POST: /Settings/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Settings settings)
        {
            if (ModelState.IsValid)
            {
                db.Setttings.Add(settings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateDropDowns(settings);
            return View(settings);
        }

        //
        // GET: /Settings/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Settings settings = db.Setttings.Find(id);
            if (settings == null)
            {
                return HttpNotFound();
            }

            PopulateDropDowns(settings);
            return View(settings);
        }

        //
        // POST: /Settings/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Settings settings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(settings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateDropDowns(settings);
            return View(settings);
        }

        //
        // GET: /Settings/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Settings settings = db.Setttings.Find(id);
            if (settings == null)
            {
                return HttpNotFound();
            }
            return View(settings);
        }

        //
        // POST: /Settings/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Settings settings = db.Setttings.Find(id);
            db.Setttings.Remove(settings);
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