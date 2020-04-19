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
    public class JudgeTypeController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /JudgeType/

        public ActionResult Index()
        {
            return View(db.JudgeTypes.ToList());
        }

        //
        // GET: /JudgeType/Details/5

        public ActionResult Details(int id = 0)
        {
            JudgeType judgeType = db.JudgeTypes.Find(id);
            if (judgeType == null)
            {
                return HttpNotFound();
            }
            return View(judgeType);
        }

        //
        // GET: /JudgeType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /JudgeType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JudgeType judgeType)
        {
            if (ModelState.IsValid)
            {
                db.JudgeTypes.Add(judgeType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(judgeType);
        }

        //
        // GET: /JudgeType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            JudgeType judgeType = db.JudgeTypes.Find(id);
            if (judgeType == null)
            {
                return HttpNotFound();
            }
            return View(judgeType);
        }

        //
        // POST: /JudgeType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JudgeType judgeType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(judgeType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(judgeType);
        }

        //
        // GET: /JudgeType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            JudgeType judgeType = db.JudgeTypes.Find(id);
            if (judgeType == null)
            {
                return HttpNotFound();
            }
            return View(judgeType);
        }

        //
        // POST: /JudgeType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JudgeType judgeType = db.JudgeTypes.Find(id);
            db.JudgeTypes.Remove(judgeType);
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