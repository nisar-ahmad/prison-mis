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
    public class CrimeTypeController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /CrimeType/

        public ActionResult Index()
        {
            return View(db.CrimeTypes.ToList());
        }

        //
        // GET: /CrimeType/Details/5

        public ActionResult Details(int id = 0)
        {
            CrimeType crimetype = db.CrimeTypes.Find(id);
            if (crimetype == null)
            {
                return HttpNotFound();
            }
            return View(crimetype);
        }

        //
        // GET: /CrimeType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CrimeType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CrimeType crimetype)
        {
            if (ModelState.IsValid)
            {
                db.CrimeTypes.Add(crimetype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(crimetype);
        }

        //
        // GET: /CrimeType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CrimeType crimetype = db.CrimeTypes.Find(id);
            if (crimetype == null)
            {
                return HttpNotFound();
            }
            return View(crimetype);
        }

        //
        // POST: /CrimeType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CrimeType crimetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crimetype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(crimetype);
        }

        //
        // GET: /CrimeType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CrimeType crimetype = db.CrimeTypes.Find(id);
            if (crimetype == null)
            {
                return HttpNotFound();
            }
            return View(crimetype);
        }

        //
        // POST: /CrimeType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CrimeType crimetype = db.CrimeTypes.Find(id);
            db.CrimeTypes.Remove(crimetype);
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