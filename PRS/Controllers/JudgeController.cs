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
    public class JudgeController : Controller
    {
        private PRSContext db = new PRSContext();

        //
        // GET: /Judge/

        /// <summary>
        /// Returns Array of Judges (ID, Name) as Json for TypeAhead
        /// </summary>
        /// <param name="id">query</param>
        /// <returns>Array of Prisoners</returns>
        public JsonResult List(string id)
        {
            var data = db.Judges.Select(o => new { o.JudgeId, o.Name }).Where(o => o.Name.StartsWith(id)).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Index()
        {
            var judges = db.Judges.Include(j => j.Court);
            return View(judges.ToList());
        }

        //
        // GET: /Judge/Details/5

        public ActionResult Details(int id = 0)
        {
            Judge judge = db.Judges.Find(id);
            if (judge == null)
            {
                return HttpNotFound();
            }
            return View(judge);
        }

        //
        // GET: /Judge/Create

        public ActionResult Create()
        {
            ViewBag.CourtId = new SelectList(db.Courts, "CourtId", "Name");
            return View();
        }

        //
        // POST: /Judge/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Judge judge)
        {
            if (ModelState.IsValid)
            {
                db.Judges.Add(judge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourtId = new SelectList(db.Courts, "CourtId", "Name", judge.CourtId);
            return View(judge);
        }

        //
        // GET: /Judge/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Judge judge = db.Judges.Find(id);
            if (judge == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourtId = new SelectList(db.Courts, "CourtId", "Name", judge.CourtId);
            return View(judge);
        }

        //
        // POST: /Judge/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Judge judge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(judge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourtId = new SelectList(db.Courts, "CourtId", "Name", judge.CourtId);
            return View(judge);
        }

        //
        // GET: /Judge/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Judge judge = db.Judges.Find(id);
            if (judge == null)
            {
                return HttpNotFound();
            }
            return View(judge);
        }

        //
        // POST: /Judge/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Judge judge = db.Judges.Find(id);
            db.Judges.Remove(judge);
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