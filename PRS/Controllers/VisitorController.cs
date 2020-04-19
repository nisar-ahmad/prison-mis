using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PRS.Models;
using PagedList;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using System.Web;
using System.Text;
using System.Collections.Generic;
using System.Dynamic;

namespace PRS.Controllers
{
    public class VisitorController : PRSBaseController
    {
        private Visitor CreateNewVisitor(string CNIC)
        {
            var visitor = new Visitor();
            var settings = db.Setttings.First();

            visitor.JailId = settings.JailId;
            visitor.PresentDistrictId = settings.DistrictId;
            visitor.PresentCityId = settings.CityId;
            visitor.CNIC = CNIC;

            return visitor;
        }

        private void PopulateDropDowns(Visitor p)
        {
            ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)), p.Gender);
            ViewBag.VisitorType = new SelectList(Enum.GetValues(typeof(VisitorType)), p.VisitorType);

            ViewBag.OccupationId = new SelectList(db.Occupations, "OccupationId", "Name", p.OccupationId);
            ViewBag.NationalityCountryId = new SelectList(db.Countries, "CountryId", "Name", p.NationalityCountryId);
            ViewBag.RelationTypeId = new SelectList(db.RelationTypes, "RelationTypeId", "Name", p.RelationTypeId);

            // PRESENT ADDRESS
            ViewBag.PresentCountryId = new SelectList(db.Countries, "CountryId", "Name", p.PresentCountryId);
            ViewBag.PresentProvinceId = new SelectList(db.Provinces.Where(o => o.CountryId == p.PresentCountryId), "ProvinceId", "Name", p.PresentProvinceId);
            ViewBag.PresentDistrictId = new SelectList(db.Districts.Where(o => o.ProvinceId == p.PresentProvinceId), "DistrictId", "Name", p.PresentDistrictId);
            ViewBag.PresentCityId = new SelectList(db.Cities.Where(o => o.DistrictId == p.PresentDistrictId), "CityId", "Name", p.PresentCityId);
        }

        // GET: /Visitor/
        public ActionResult Index(int? page = 1)
        {
            ViewBag.PageSize = ReportsController.PageSize;

            page = page ?? 1;
            var visitors = db.Visitors.Include(p => p.Jail).Include(p => p.VisitorType).Include(p => p.Occupation).Include(p => p.Nationality).Include(p => p.PresentCountry).Include(p => p.PresentProvince).Include(p => p.PresentDistrict).Include(p => p.PresentCity);
            return View(visitors.OrderBy(o => o.Name).ToPagedList(page.Value, ReportsController.PageSize));
        }

        public ActionResult Search()
        {
            var visitor = new Visitor();
            return View(visitor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string CNIC)
        {
            if(!string.IsNullOrWhiteSpace(CNIC))
            {
                var visitor = db.Visitors.FirstOrDefault(o => o.CNIC.Equals(CNIC));

                object routeValues = null;

                if (visitor != null)
                    routeValues = new { CNIC = CNIC, VisitorId = visitor.VisitorId };
                else
                    routeValues = new { CNIC = CNIC };

                return RedirectToAction("Register", routeValues);
            }

            Error("Invalid CNIC. Please enter a valid CNIC!");
            return Search();
        }

        // GET: /Visitor/Edit/5
        public ActionResult Register(int VisitorId = 0, string CNIC = null)
        {
            Visitor visitor = null;

            if (VisitorId > 0)
            {
                visitor = db.Visitors.Find(VisitorId);

                if (visitor == null)
                {
                    Error("Visitor not found!");
                    return RedirectToAction("Search");
                }
            }
            else
                visitor = CreateNewVisitor(CNIC);

            PopulateDropDowns(visitor);
            return View(visitor);
        }

        // POST: /Visitor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                if (visitor.VisitorId > 0)
                {
                    var entry = db.Entry(visitor);
                    entry.State = EntityState.Modified;
                }
                else
                {
                    db.Visitors.Add(visitor);
                    Success("Visitor registered successfully!");
                }

                db.SaveChanges();
                return RedirectToAction("Manage", "Visit", new { VisitorId = visitor.VisitorId });
            }

            PopulateDropDowns(visitor);
            return View(visitor);
        }

        /// <summary>
        /// Returns Array of Visitors (ID, Name) as Json for TypeAhead
        /// </summary>
        /// <param name="id">query</param>
        /// <returns>Array of Visitors</returns>
        public JsonResult List(string id)
        {
            var data = db.Visitors.Select(o => new { o.VisitorId, o.Name }).Where(o => o.Name.StartsWith(id)).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}