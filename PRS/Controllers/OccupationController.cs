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
    public class OccupationController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /Occupation/

        public ActionResult Index()
        {
            return View(db.Occupations.ToList());
        }

        //
        // GET: /Occupation/Details/5

        public ActionResult Details(int id = 0)
        {
            Occupation occupation = db.Occupations.Find(id);
            if (occupation == null)
            {
                return HttpNotFound();
            }
            return View(occupation);
        }

        //
        // GET: /Occupation/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Occupation/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Occupation occupation)
        {
            if (ModelState.IsValid)
            {
                db.Occupations.Add(occupation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(occupation);
        }

        //
        // GET: /Occupation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Occupation occupation = db.Occupations.Find(id);
            if (occupation == null)
            {
                return HttpNotFound();
            }
            return View(occupation);
        }

        //
        // POST: /Occupation/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Occupation occupation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(occupation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(occupation);
        }

        //
        // GET: /Occupation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Occupation occupation = db.Occupations.Find(id);
            if (occupation == null)
            {
                return HttpNotFound();
            }
            return View(occupation);
        }

        //
        // POST: /Occupation/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Occupation occupation = db.Occupations.Find(id);
            db.Occupations.Remove(occupation);
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