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
    public class DiseaseController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /Disease/

        public ActionResult Index()
        {
            return View(db.Diseases.ToList());
        }

        //
        // GET: /Disease/Details/5

        public ActionResult Details(int id = 0)
        {
            Disease disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        //
        // GET: /Disease/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Disease/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Disease disease)
        {
            if (ModelState.IsValid)
            {
                db.Diseases.Add(disease);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(disease);
        }

        //
        // GET: /Disease/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Disease disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        //
        // POST: /Disease/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Disease disease)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disease).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(disease);
        }

        //
        // GET: /Disease/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Disease disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        //
        // POST: /Disease/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Disease disease = db.Diseases.Find(id);
            db.Diseases.Remove(disease);
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