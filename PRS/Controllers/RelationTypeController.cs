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
    public class RelationTypeController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /RelationType/

        public ActionResult Index()
        {
            return View(db.RelationTypes.ToList());
        }

        //
        // GET: /RelationType/Details/5

        public ActionResult Details(int id = 0)
        {
            RelationType relationType = db.RelationTypes.Find(id);
            if (relationType == null)
            {
                return HttpNotFound();
            }
            return View(relationType);
        }

        //
        // GET: /RelationType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RelationType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RelationType relationType)
        {
            if (ModelState.IsValid)
            {
                db.RelationTypes.Add(relationType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(relationType);
        }

        //
        // GET: /RelationType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RelationType relationType = db.RelationTypes.Find(id);
            if (relationType == null)
            {
                return HttpNotFound();
            }
            return View(relationType);
        }

        //
        // POST: /RelationType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RelationType relationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relationType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(relationType);
        }

        //
        // GET: /RelationType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RelationType relationType = db.RelationTypes.Find(id);
            if (relationType == null)
            {
                return HttpNotFound();
            }
            return View(relationType);
        }

        //
        // POST: /RelationType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RelationType relationType = db.RelationTypes.Find(id);
            db.RelationTypes.Remove(relationType);
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