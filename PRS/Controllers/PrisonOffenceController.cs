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
    public class PrisonOffenceController : PRSBaseController
    {

        //
        // GET: /PrisonOffence/

        public ActionResult Index()
        {
            var prisonoffences = db.PrisonOffences.Include(p => p.Prisoner).Where(o => o.PrisonerId == PrisonerId);
            return View(prisonoffences.ToList());
        }

        //
        // GET: /PrisonOffence/Details/5

        public ActionResult Details(int id = 0)
        {
            PrisonOffence prisonoffence = db.PrisonOffences.Find(id);
            if (prisonoffence == null)
            {
                return HttpNotFound();
            }
            return View(prisonoffence);
        }

        //
        // GET: /PrisonOffence/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PrisonOffence/Create

        [HttpPost]
        public ActionResult Create(PrisonOffence prisonoffence)
        {
            if (ModelState.IsValid)
            {
                db.PrisonOffences.Add(prisonoffence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ValidationError();
            return View(prisonoffence);
        }

        //
        // GET: /PrisonOffence/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PrisonOffence prisonoffence = db.PrisonOffences.Find(id);
            if (prisonoffence == null)
            {
                return HttpNotFound();
            }
            
            return View(prisonoffence);
        }

        //
        // POST: /PrisonOffence/Edit/5

        [HttpPost]
        public ActionResult Edit(PrisonOffence prisonoffence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prisonoffence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ValidationError();
            return View(prisonoffence);
        }

        //
        // GET: /PrisonOffence/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PrisonOffence prisonoffence = db.PrisonOffences.Find(id);
            if (prisonoffence == null)
            {
                return HttpNotFound();
            }
            return View(prisonoffence);
        }

        //
        // POST: /PrisonOffence/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PrisonOffence prisonoffence = db.PrisonOffences.Find(id);
            db.PrisonOffences.Remove(prisonoffence);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}