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
    public class RemissionTypeController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /RemissionType/

        public ActionResult Index()
        {
            return View(db.RemissionTypes.ToList());
        }

        //
        // GET: /RemissionType/Details/5

        public ActionResult Details(int id = 0)
        {
            RemissionType remissionType = db.RemissionTypes.Find(id);
            if (remissionType == null)
            {
                return HttpNotFound();
            }
            return View(remissionType);
        }

        //
        // GET: /RemissionType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RemissionType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RemissionType remissionType)
        {
            if (ModelState.IsValid)
            {
                db.RemissionTypes.Add(remissionType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(remissionType);
        }

        //
        // GET: /RemissionType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RemissionType remissionType = db.RemissionTypes.Find(id);
            if (remissionType == null)
            {
                return HttpNotFound();
            }
            return View(remissionType);
        }

        //
        // POST: /RemissionType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RemissionType remissionType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(remissionType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(remissionType);
        }

        //
        // GET: /RemissionType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RemissionType remissionType = db.RemissionTypes.Find(id);
            if (remissionType == null)
            {
                return HttpNotFound();
            }
            return View(remissionType);
        }

        //
        // POST: /RemissionType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RemissionType remissionType = db.RemissionTypes.Find(id);
            db.RemissionTypes.Remove(remissionType);
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