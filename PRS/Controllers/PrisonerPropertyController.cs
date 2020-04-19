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
    public class PrisonerPropertyController : PRSBaseController
    {
        //
        // GET: /PrisonerProperty/

        public ActionResult Index()
        {
            var prisonerproperties = db.PrisonerProperties.Include(p => p.Prisoner).Where(o => o.PrisonerId == PrisonerId);
            return View(prisonerproperties.ToList());
        }

        //
        // GET: /PrisonerProperty/Details/5

        public ActionResult Details(int id = 0)
        {
            PrisonerProperty prisonerproperty = db.PrisonerProperties.Find(id);
            if (prisonerproperty == null)
            {
                return HttpNotFound();
            }
            return View(prisonerproperty);
        }

        //
        // GET: /PrisonerProperty/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PrisonerProperty/Create

        [HttpPost]
        public ActionResult Create(PrisonerProperty prisonerproperty)
        {
            if (ModelState.IsValid)
            {
                db.PrisonerProperties.Add(prisonerproperty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ValidationError();
            return View(prisonerproperty);
        }

        //
        // GET: /PrisonerProperty/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PrisonerProperty prisonerproperty = db.PrisonerProperties.Find(id);
            if (prisonerproperty == null)
            {
                return HttpNotFound();
            }

            return View(prisonerproperty);
        }

        //
        // POST: /PrisonerProperty/Edit/5

        [HttpPost]
        public ActionResult Edit(PrisonerProperty prisonerproperty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prisonerproperty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ValidationError();
            return View(prisonerproperty);
        }

        //
        // GET: /PrisonerProperty/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PrisonerProperty prisonerproperty = db.PrisonerProperties.Find(id);
            if (prisonerproperty == null)
            {
                return HttpNotFound();
            }
            return View(prisonerproperty);
        }

        //
        // POST: /PrisonerProperty/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PrisonerProperty prisonerproperty = db.PrisonerProperties.Find(id);
            db.PrisonerProperties.Remove(prisonerproperty);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}