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
    public class BarrackController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /Barrack/

        public ActionResult Index()
        {
            var barracks = db.Barracks.Include(b => b.Jail);
            return View(barracks.ToList());
        }

        //
        // GET: /Barrack/Details/5

        public ActionResult Details(int id = 0)
        {
            Barrack barrack = db.Barracks.Find(id);
            if (barrack == null)
            {
                return HttpNotFound();
            }
            return View(barrack);
        }

        //
        // GET: /Barrack/Create

        public ActionResult Create()
        {
            ViewBag.JailId = new SelectList(db.Jails, "JailId", "Name");
            return View();
        }

        //
        // POST: /Barrack/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Barrack barrack)
        {
            if (ModelState.IsValid)
            {
                db.Barracks.Add(barrack);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JailId = new SelectList(db.Jails, "JailId", "Name", barrack.JailId);
            return View(barrack);
        }

        //
        // GET: /Barrack/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Barrack barrack = db.Barracks.Find(id);
            if (barrack == null)
            {
                return HttpNotFound();
            }
            ViewBag.JailId = new SelectList(db.Jails, "JailId", "Name", barrack.JailId);
            return View(barrack);
        }

        //
        // POST: /Barrack/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Barrack barrack)
        {
            if (ModelState.IsValid)
            {
                db.Entry(barrack).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JailId = new SelectList(db.Jails, "JailId", "Name", barrack.JailId);
            return View(barrack);
        }

        //
        // GET: /Barrack/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Barrack barrack = db.Barracks.Find(id);
            if (barrack == null)
            {
                return HttpNotFound();
            }
            return View(barrack);
        }

        //
        // POST: /Barrack/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Barrack barrack = db.Barracks.Find(id);
            db.Barracks.Remove(barrack);
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