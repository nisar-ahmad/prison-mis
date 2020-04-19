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
    public class PrisonerSubTypeController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /PrisonerSubType/

        public ActionResult Index()
        {
            var prisonersubtypes = db.PrisonerSubTypes.Include(p => p.PrisonerType);
            return View(prisonersubtypes.ToList());
        }

        //
        // GET: /PrisonerSubType/Details/5

        public ActionResult Details(int id = 0)
        {
            PrisonerSubType prisonersubtype = db.PrisonerSubTypes.Find(id);
            if (prisonersubtype == null)
            {
                return HttpNotFound();
            }
            return View(prisonersubtype);
        }

        //
        // GET: /PrisonerSubType/Create

        public ActionResult Create()
        {
            ViewBag.PrisonerTypeId = new SelectList(db.PrisonerTypes, "PrisonerTypeId", "Name");
            return View();
        }

        //
        // POST: /PrisonerSubType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrisonerSubType prisonersubtype)
        {
            if (ModelState.IsValid)
            {
                db.PrisonerSubTypes.Add(prisonersubtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PrisonerTypeId = new SelectList(db.PrisonerTypes, "PrisonerTypeId", "Name", prisonersubtype.PrisonerTypeId);
            return View(prisonersubtype);
        }

        //
        // GET: /PrisonerSubType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PrisonerSubType prisonersubtype = db.PrisonerSubTypes.Find(id);
            if (prisonersubtype == null)
            {
                return HttpNotFound();
            }
            ViewBag.PrisonerTypeId = new SelectList(db.PrisonerTypes, "PrisonerTypeId", "Name", prisonersubtype.PrisonerTypeId);
            return View(prisonersubtype);
        }

        //
        // POST: /PrisonerSubType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PrisonerSubType prisonersubtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prisonersubtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PrisonerTypeId = new SelectList(db.PrisonerTypes, "PrisonerTypeId", "Name", prisonersubtype.PrisonerTypeId);
            return View(prisonersubtype);
        }

        //
        // GET: /PrisonerSubType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PrisonerSubType prisonersubtype = db.PrisonerSubTypes.Find(id);
            if (prisonersubtype == null)
            {
                return HttpNotFound();
            }
            return View(prisonersubtype);
        }

        //
        // POST: /PrisonerSubType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrisonerSubType prisonersubtype = db.PrisonerSubTypes.Find(id);
            db.PrisonerSubTypes.Remove(prisonersubtype);
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