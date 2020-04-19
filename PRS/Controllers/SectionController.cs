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
    public class SectionController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /Section/

        public ActionResult Index()
        {
            var sections = db.Sections.Include(s => s.Act).Include(s => s.CrimeType);
            return View(sections.ToList());
        }

        //
        // GET: /Section/Details/5

        public ActionResult Details(int id = 0)
        {
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        //
        // GET: /Section/Create

        public ActionResult Create()
        {
            ViewBag.ActId = new SelectList(db.Acts, "ActId", "Name");
            ViewBag.CrimeTypeId = new SelectList(db.CrimeTypes, "CrimeTypeId", "Name");
            return View();
        }

        //
        // POST: /Section/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Section section)
        {
            if (ModelState.IsValid)
            {
                db.Sections.Add(section);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActId = new SelectList(db.Acts, "ActId", "Name", section.ActId);
            ViewBag.CrimeTypeId = new SelectList(db.CrimeTypes, "CrimeTypeId", "Name", section.CrimeTypeId);
            return View(section);
        }

        //
        // GET: /Section/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActId = new SelectList(db.Acts, "ActId", "Name", section.ActId);
            ViewBag.CrimeTypeId = new SelectList(db.CrimeTypes, "CrimeTypeId", "Name", section.CrimeTypeId);
            return View(section);
        }

        //
        // POST: /Section/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Section section)
        {
            if (ModelState.IsValid)
            {
                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActId = new SelectList(db.Acts, "ActId", "Name", section.ActId);
            ViewBag.CrimeTypeId = new SelectList(db.CrimeTypes, "CrimeTypeId", "Name", section.CrimeTypeId);
            return View(section);
        }

        //
        // GET: /Section/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        //
        // POST: /Section/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Section section = db.Sections.Find(id);
            db.Sections.Remove(section);
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