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
    public class PrisonerTypeController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /PrisonerType/

        public ActionResult Index()
        {
            return View(db.PrisonerTypes.ToList());
        }

        //
        // GET: /PrisonerType/Details/5

        public ActionResult Details(int id = 0)
        {
            PrisonerType prisonertype = db.PrisonerTypes.Find(id);
            if (prisonertype == null)
            {
                return HttpNotFound();
            }
            return View(prisonertype);
        }

        //
        // GET: /PrisonerType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PrisonerType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrisonerType prisonertype)
        {
            if (ModelState.IsValid)
            {
                db.PrisonerTypes.Add(prisonertype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prisonertype);
        }

        //
        // GET: /PrisonerType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PrisonerType prisonertype = db.PrisonerTypes.Find(id);
            if (prisonertype == null)
            {
                return HttpNotFound();
            }
            return View(prisonertype);
        }

        //
        // POST: /PrisonerType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PrisonerType prisonertype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prisonertype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prisonertype);
        }

        //
        // GET: /PrisonerType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PrisonerType prisonertype = db.PrisonerTypes.Find(id);
            if (prisonertype == null)
            {
                return HttpNotFound();
            }
            return View(prisonertype);
        }

        //
        // POST: /PrisonerType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrisonerType prisonertype = db.PrisonerTypes.Find(id);
            db.PrisonerTypes.Remove(prisonertype);
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