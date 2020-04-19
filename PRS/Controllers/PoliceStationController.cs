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
    public class PoliceStationController : Controller
    {
        private PRSContext db = new PRSContext();

        public JsonResult List(string id)
        {
            var data = db.PoliceStations.Select(o => new { o.PoliceStationId, o.Name }).Where(o => o.Name.StartsWith(id)).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /PoliceStation/

        public ActionResult Index()
        {
            var policestations = db.PoliceStations.Include(p => p.District).Include(p => p.City);
            return View(policestations.ToList());
        }

        //
        // GET: /PoliceStation/Details/5

        public ActionResult Details(int id = 0)
        {
            PoliceStation policestation = db.PoliceStations.Find(id);
            if (policestation == null)
            {
                return HttpNotFound();
            }
            return View(policestation);
        }

        //
        // GET: /PoliceStation/Create

        public ActionResult Create()
        {
            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "Name");
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name");
            return View();
        }

        //
        // POST: /PoliceStation/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PoliceStation policestation)
        {
            if (ModelState.IsValid)
            {
                db.PoliceStations.Add(policestation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "Name", policestation.DistrictId);
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", policestation.CityId);
            return View(policestation);
        }

        //
        // GET: /PoliceStation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PoliceStation policestation = db.PoliceStations.Find(id);
            if (policestation == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "Name", policestation.DistrictId);
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", policestation.CityId);
            return View(policestation);
        }

        //
        // POST: /PoliceStation/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PoliceStation policestation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(policestation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "Name", policestation.DistrictId);
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", policestation.CityId);
            return View(policestation);
        }

        //
        // GET: /PoliceStation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PoliceStation policestation = db.PoliceStations.Find(id);
            if (policestation == null)
            {
                return HttpNotFound();
            }
            return View(policestation);
        }

        //
        // POST: /PoliceStation/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PoliceStation policestation = db.PoliceStations.Find(id);
            db.PoliceStations.Remove(policestation);
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