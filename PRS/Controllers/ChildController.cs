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
    public class ChildController : PRSBaseController
    {
        private Child CreateChild()
        {
            var child = new Child();
            child.PrisonerId = PrisonerId;
            child.AdmissionId = AdmissionId;

            return child;
        }
        //
        // GET: /Child/

        public ActionResult Index()
        {
            var children = db.Children.Where(o => o.PrisonerId == PrisonerId);
            return View(children.ToList());
        }

        //
        // GET: /Child/Details/5

        public ActionResult Details(int id = 0)
        {
            Child child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        //
        // GET: /Child/Create

        public ActionResult Create()
        {
            var child = CreateChild();
            ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)), child.Gender);
            return View(child);
        }

        //
        // POST: /Child/Create

        [HttpPost]
        public ActionResult Create(Child child)
        {
            if (ModelState.IsValid)
            {
                child.PrisonerId = PrisonerId;
                child.AdmissionId = AdmissionId;

                db.Children.Add(child);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)), child.Gender);
            return View(child);
        }

        //
        // GET: /Child/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Child child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)), child.Gender);
            return View(child);
        }

        //
        // POST: /Child/Edit/5

        [HttpPost]
        public ActionResult Edit(Child child)
        {
            if (ModelState.IsValid)
            {
                db.Entry(child).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)), child.Gender);
            return View(child);
        }

        //
        // GET: /Child/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Child child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }

            return View(child);
        }

        //
        // POST: /Child/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Child child = db.Children.Find(id);
            db.Children.Remove(child);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}