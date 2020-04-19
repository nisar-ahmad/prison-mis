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
    public class ActController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /Act/

        public ActionResult Index()
        {
            return View(db.Acts.ToList());
        }

        //
        // GET: /Act/Details/5

        public ActionResult Details(int id = 0)
        {
            Act act = db.Acts.Find(id);
            if (act == null)
            {
                return HttpNotFound();
            }
            return View(act);
        }

        //
        // GET: /Act/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Act/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Act act)
        {
            if (ModelState.IsValid)
            {
                db.Acts.Add(act);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(act);
        }

        //
        // GET: /Act/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Act act = db.Acts.Find(id);
            if (act == null)
            {
                return HttpNotFound();
            }
            return View(act);
        }

        //
        // POST: /Act/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Act act)
        {
            if (ModelState.IsValid)
            {
                db.Entry(act).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(act);
        }

        //
        // GET: /Act/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Act act = db.Acts.Find(id);
            if (act == null)
            {
                return HttpNotFound();
            }
            return View(act);
        }

        //
        // POST: /Act/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Act act = db.Acts.Find(id);
            db.Acts.Remove(act);
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