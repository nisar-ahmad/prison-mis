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

namespace PRS.Controllers
{
    public class VisitController : PRSBaseController
    {
        public ActionResult Manage(int VisitorId = 0, int VisitId = 0)
        {
            if (VisitorId > 0)
            {
                var visitor = db.Visitors.Include(o => o.Visits).FirstOrDefault(o => o.VisitorId == VisitorId);

                if(visitor != null)
                {
                    var visitVM = new VisitViewModel();
                    visitor.Visits = visitor.Visits.OrderByDescending(o => o.DateOfVisit).ToList();
                    visitVM.Visitor = visitor;

                    if (VisitId > 0)
                        visitVM.Visit = db.Visits.FirstOrDefault(o => o.VisitId == VisitId);

                    visitVM.Visit.VisitorId = VisitorId;

                    return View(visitVM);
                }
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(Visit visit)
        {
            if(visit.PrisonerId > 0)
            {
                if(visit.VisitId > 0)
                {
                    var entry = db.Entry(visit);
                    entry.State = EntityState.Modified;
                }
                else
                {
                    var count = db.Visits.Count(o => DbFunctions.TruncateTime(o.DateOfVisit) == DbFunctions.TruncateTime(visit.DateOfVisit));
                    visit.SerialNumber = count + 1;
                    db.Visits.Add(visit);
                }

                db.SaveChanges();

                Success("Visit saved successfully!");
                return RedirectToAction("Manage", new { VisitorId = visit.VisitorId });
            }
            else
                ModelState.AddModelError("PrisonerId", "Invalid Prisoner");

            return View(new VisitViewModel());
        }

        public ActionResult Print(int VisitId = 0)
        {
            var visit = db.Visits.Include(o => o.Prisoner).Include(o => o.Visitor).FirstOrDefault(o => o.VisitId == VisitId);
            return View(visit);
        }

        /// <summary>
        /// Returns Array of Visitors (ID, Name) as Json for TypeAhead
        /// </summary>
        /// <param name="id">query</param>
        /// <returns>Array of Visitors</returns>
        public JsonResult List(string id)
        {
            var data = db.Prisoners.Where(o => o.Name.StartsWith(id)).Select(o => new { PrisonerId = o.PrisonerId, DisplayName = o.Name + " S/O " + o.FatherOrHusbandName}).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}