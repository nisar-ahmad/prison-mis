using PRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRS.Controllers
{
    public class CheckInOutController : PRSBaseController
    {
        public ActionResult Index(CheckInOut checkInOut, int PrisonerId = 0)
        {
            if(checkInOut.Type == CheckInOutType.PhysicalRemandIn)
            {
                Admission admission = null;

                if (AdmissionId == 0) // Get latest admission
                {
                    admission = GetLatestAdmission(PrisonerId);

                    if (admission == null)
                    {
                        Error("Admission record not found!");
                        return View();
                    }
                    else
                        AdmissionId = admission.AdmissionId;
                }

                var routeValues = new { PrisonerId = PrisonerId, AdmissionId = AdmissionId, Remand=1 };
                return RedirectToAction("Admit", "FIR", routeValues);
            }

            return View(checkInOut);
        }

        [HttpPost]
        public ActionResult Index(CheckInOut checkInOut)
        {
            Admission admission = null;

            if (checkInOut.AdmissionId == null || checkInOut.AdmissionId <= 0) // Get latest admission
                admission = GetLatestAdmission();
            else
                admission = db.Admissions.FirstOrDefault(o => o.AdmissionId == checkInOut.AdmissionId.Value);

            if (admission == null)
            {
                Error("Admission record not found!");
                return View();
            }

            AdmissionId = admission.AdmissionId;
            checkInOut.PrisonerNumber = admission.PrisonerNumber;

            var actionResult = RedirectToAction("History");

            switch(checkInOut.Type)
            {
                case CheckInOutType.PhysicalRemandIn:
                    {
                        actionResult = RedirectToAction("Admit", "FIR");
                        break;
                    }
                case CheckInOutType.TransferOut:
                    {
                        var prisoner = db.Prisoners.FirstOrDefault(o => o.PrisonerId == admission.PrisonerId);
                        prisoner.Status = PrisonerStatus.Transferred;
                        break;
                    }
            }

            db.CheckInOuts.Add(checkInOut);
            db.SaveChanges();

            return actionResult;
        }

        public ActionResult History(int PrisonerId = 0)
        {
            var list = db.CheckInOuts.Where(o => o.PrisonerId == PrisonerId).OrderBy(o => o.DateOfCheckInOut).ToList();
            return View(list);
        }

    }
}
