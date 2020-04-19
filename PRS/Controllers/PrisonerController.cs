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
//using System.Data.Objects;
using System.Web;
using System.Text;
using System.Collections.Generic;

namespace PRS.Controllers
{
    public class PrisonerController : PRSBaseController
    {
        public JsonResult PrisonerSubTypes(string id)
        {
            int prisonerTypeId = Convert.ToInt32(id);

            var subTypes = db.PrisonerSubTypes.Where(o => o.PrisonerTypeId == prisonerTypeId);
            return Json(new SelectList(subTypes.ToArray(), "PrisonerSubTypeId", "Name"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Provinces(string id)
        {
            int countryId = Convert.ToInt32(id);

            var provinces = db.Provinces.Where(o => o.CountryId == countryId);
            return Json(new SelectList(provinces.ToArray(), "ProvinceId", "Name"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Districts(string id)
        {
            int provinceId = Convert.ToInt32(id);

            var districts = db.Districts.Where(o => o.ProvinceId == provinceId);
            return Json(new SelectList(districts.ToArray(), "DistrictId", "Name"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cities(string id)
        {
            int districtId = Convert.ToInt32(id);

            var cities = db.Cities.Where(o => o.DistrictId == districtId);
            return Json(new SelectList(cities.ToArray(), "CityId", "Name"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PoliceStations(string id, string cityId = null)
        {
            int districtId = Convert.ToInt32(id);
            IQueryable<PoliceStation> ps = null;

            if (string.IsNullOrEmpty(cityId))
                ps = db.PoliceStations.Where(o => o.DistrictId == districtId);
            else
            {
                int cityID = Convert.ToInt32(cityId);
                ps = db.PoliceStations.Where(o => o.DistrictId == districtId && o.CityId == cityID);
            }

            return Json(new SelectList(ps.ToArray(), "PoliceStationId", "Name"), JsonRequestBehavior.AllowGet);
        }

        private void PopulateDropDowns(Prisoner p)
        {
            ViewBag.Status = new SelectList(Enum.GetValues(typeof(PrisonerStatus)), p.Status);
            ViewBag.Category = new SelectList(Enum.GetValues(typeof(PrisonerCategory)), p.Category);
            ViewBag.Class = new SelectList(Enum.GetValues(typeof(PrisonerClass)), p.Class);
            ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)), p.Gender);
            ViewBag.MaritalStatus = new SelectList(Enum.GetValues(typeof(MaritalStatus)), p.MaritalStatus);
            ViewBag.NarcoticsStatus = new SelectList(Enum.GetValues(typeof(NarcoticsStatus)), p.NarcoticsStatus);
            ViewBag.ApprovalStatus = new SelectList(Enum.GetValues(typeof(ApprovalStatus)), p.ApprovalStatus);
            ViewBag.IdentityDocumentType = new SelectList(Enum.GetValues(typeof(IdentityDocumentType)), p.IdentityDocumentType);

            ViewBag.PrisonerTypeId = new SelectList(db.PrisonerTypes, "PrisonerTypeId", "Name", p.PrisonerTypeId);
            ViewBag.PrisonerSubTypeId = new SelectList(db.PrisonerSubTypes.Where(o => o.PrisonerTypeId == p.PrisonerTypeId), "PrisonerSubTypeId", "Name", p.PrisonerSubTypeId);

            ViewBag.ReligionId = new SelectList(db.Religions, "ReligionId", "Name", p.ReligionId);
            ViewBag.OccupationId = new SelectList(db.Occupations, "OccupationId", "Name", p.OccupationId);
            ViewBag.NationalityCountryId = new SelectList(db.Countries, "CountryId", "Name", p.NationalityCountryId);

            // PRESENT ADDRESS
            ViewBag.PresentCountryId = new SelectList(db.Countries, "CountryId", "Name", p.PresentCountryId);
            ViewBag.PresentProvinceId = new SelectList(db.Provinces.Where(o => o.CountryId == p.PresentCountryId), "ProvinceId", "Name", p.PresentProvinceId);
            ViewBag.PresentDistrictId = new SelectList(db.Districts.Where(o => o.ProvinceId == p.PresentProvinceId), "DistrictId", "Name", p.PresentDistrictId);
            ViewBag.PresentCityId = new SelectList(db.Cities.Where(o => o.DistrictId == p.PresentDistrictId), "CityId", "Name", p.PresentCityId);

            if (p.PresentCityId > 0)
                ViewBag.PresentPoliceStationId = new SelectList(db.PoliceStations.Where(o => o.CityId == p.PresentCityId), "PoliceStationId", "Name", p.PresentPoliceStationId);
            else
                ViewBag.PresentPoliceStationId = new SelectList(db.PoliceStations.Where(o => o.DistrictId == p.PresentDistrictId), "PoliceStationId", "Name", p.PresentPoliceStationId);

            // PERMANENT ADDRESS
            ViewBag.PermanentCountryId = new SelectList(db.Countries, "CountryId", "Name", p.PermanentCountryId);
            ViewBag.PermanentProvinceId = new SelectList(db.Provinces.Where(o => o.CountryId == p.PermanentCountryId), "ProvinceId", "Name", p.PermanentProvinceId);
            ViewBag.PermanentDistrictId = new SelectList(db.Districts.Where(o => o.ProvinceId == p.PermanentProvinceId), "DistrictId", "Name", p.PermanentDistrictId);
            ViewBag.PermanentCityId = new SelectList(db.Cities.Where(o => o.DistrictId == p.PermanentDistrictId), "CityId", "Name", p.PermanentCityId);

            if (p.PermanentCityId > 0)
                ViewBag.PermanentPoliceStationId = new SelectList(db.PoliceStations.Where(o => o.CityId == p.PermanentCityId), "PoliceStationId", "Name", p.PermanentPoliceStationId);
            else
                ViewBag.PermanentPoliceStationId = new SelectList(db.PoliceStations.Where(o => o.DistrictId == p.PermanentDistrictId), "PoliceStationId", "Name", p.PermanentPoliceStationId);

            // EDUCATION LEVELS
            var eduLevels = db.EducationLevels.ToList();

            ViewBag.FormalEducationLevelId = new SelectList(eduLevels.Where(o => o.EducationType == EducationType.Formal), "EducationLevelId", "Name", p.FormalEducationLevelId);
            ViewBag.TechnicalEducationLevelId = new SelectList(eduLevels.Where(o => o.EducationType == EducationType.Technical), "EducationLevelId", "Name", p.TechnicalEducationLevelId);
            ViewBag.ReligiousEducationLevelId = new SelectList(eduLevels.Where(o => o.EducationType == EducationType.Religious), "EducationLevelId", "Name", p.ReligiousEducationLevelId);

            ViewBag.NextOfKinRelationTypeId = new SelectList(db.RelationTypes, "RelationTypeId", "Name", p.NextOfKinRelationTypeId);
        }

        public ActionResult Photo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Photo(string formfield, int PrisonerId)
        {
            base.PrisonerId = PrisonerId;

            string base64 = formfield.Substring(formfield.IndexOf(',') + 1);
            base64 = base64.Trim('\0');
            byte[] bytes = Convert.FromBase64String(base64);

            using (var ms = new MemoryStream(bytes))
            {
                Image photo = Image.FromStream(ms);
                photo.Save(Server.MapPath(string.Format("~/Photos/{0}_front.png", PrisonerId)), ImageFormat.Png);
            }

            return RedirectToAction("Details", new { PrisonerId = PrisonerId, Ticks = DateTime.Now.Ticks });
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Fingerprint(string Id)
        {
            var admissionId = Convert.ToInt32(Id);
            var prisonerId = db.Admissions.Select(o => new { o.AdmissionId, o.PrisonerId }).Single(o => o.AdmissionId == admissionId).PrisonerId;
            Image photo = Image.FromStream(Request.InputStream);
            photo.Save(Server.MapPath(string.Format("~/Fingerprints/{0}_finger_1.png", prisonerId)), ImageFormat.Png);

            return null;
        }

        public ActionResult Search()
        {
            switch (Request["NextView"])
            {
                case "Admit":
                    ViewBag.Title = "NEW ADMISSION";
                    break;
                case "AdmitMedical":
                    ViewBag.Title = "HEALTH ON NEW ADMISSION";
                    break;
                default:
                    ViewBag.Title = "SEARCH PRISONER BY CNIC";
                    break;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Search(string CNIC)
        {
            Prisoner prisoner = db.Prisoners.FirstOrDefault(o => o.CNIC == CNIC);

            if (prisoner != null)
                PrisonerId = prisoner.PrisonerId;

            string nextView = Request["NextView"];

            if (nextView == "AdmitMedical")
            {
                if (prisoner == null)
                {
                    Error("Prisoner not found!");
                    return View();
                }

                // Get latest admission
                var admission = GetLatestAdmission(PrisonerId);

                if (admission == null)
                {
                    Error("Admission record not found!");
                    return View();
                }

                AdmissionId = admission.AdmissionId;

                return RedirectToAction("AdmitMedical", "Admission");
            }
            else
                return RedirectToAction("Admit", "Prisoner", new { PrisonerId = PrisonerId, CNIC = CNIC });
        }

        public ActionResult SearchPrisoner()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchPrisoner(string ComputerNumber, string PrisonerNumber, string PrisonerCNIC, string Name)
        {
            ComputerNumber = ComputerNumber.Trim();
            PrisonerNumber = PrisonerNumber.Trim();
            PrisonerCNIC = PrisonerCNIC.Trim();
            Name = Name.Trim();

            Prisoner prisoner = null;
            Admission admission = null;
            long admissionId = 0;

            var query = db.Prisoners.Include(o => o.PresentDistrict).OrderBy(o => o.Name)
                .Select(o => new PrisonerViewModel
                {
                    PrisonerId = o.PrisonerId,
                    Name = o.Name,
                    CNIC = o.CNIC,
                    Parentage = o.FatherOrHusbandName,
                    Age = o.Age,
                    Category = o.Category,
                    District = o.PresentDistrict.Name,
                    AdmissionId = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).AdmissionId,
                    PrisonerNumber = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).PrisonerNumber,
                    FIRs = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).FIRs.FirstOrDefault().FIRNumber,
                    PoliceStation = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).FIRs.FirstOrDefault().PoliceStation.Name,
                    UnderSections = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).FIRs.FirstOrDefault().Sections.FirstOrDefault().Name,
                    DateOfAdmission = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).DateOfAdmission,
                    TrialCourt = db.CourtHearings.OrderByDescending(a => a.DateOfHearing).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).Court.Name,
                    DateOfHearing = db.CourtHearings.OrderByDescending(a => a.DateOfHearing).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).DateOfHearing,
                    Children = db.Children.Count(c => c.PrisonerId == o.PrisonerId)
                });

            if (long.TryParse(ComputerNumber.Trim(), out admissionId) == true)
                admission = db.Admissions.Include(o => o.Prisoner).FirstOrDefault(o => o.AdmissionId == admissionId);
            else if (string.IsNullOrWhiteSpace(PrisonerNumber) == false)
            {
                var prisoners = query.Where(a => a.PrisonerNumber.Contains(PrisonerNumber)).ToList();
                ViewBag.Count = query.Count();
                return View(prisoners);
            }
            else if (string.IsNullOrWhiteSpace(PrisonerCNIC) == false)
            {
                var prisoners = query.Where(o => o.CNIC.Contains(PrisonerCNIC)).ToList();
                ViewBag.Count = query.Count();
                return View(prisoners);
            }
            else if (string.IsNullOrWhiteSpace(Name) == false)
            {
                var prisoners = query.Where(o => o.Name.Contains(Name)).ToList();
                ViewBag.Count = query.Count();
                return View(prisoners);
            }            

            if (admission != null)
                prisoner = admission.Prisoner;

            if (prisoner == null)
            {
                Error("Prisoner not found!");
                return View();
            }
            else
            {
                PrisonerId = prisoner.PrisonerId;

                string nextView = Request["NextView"];

                if (nextView == "Medical")
                    return RedirectToMedicalTreatments();

                return RedirectToAction("Details", "Prisoner");
            }
        }

        public ActionResult Admit(int PrisonerId = 0, string CNIC = null)
        {
            Prisoner prisoner = db.Prisoners.Find(PrisonerId);

            if (prisoner == null)
            {
                prisoner = CreateNewPrisoner();
                prisoner.CNIC = CNIC;
            }

            PopulateDropDowns(prisoner);
            return View(prisoner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admit(Prisoner prisoner)
        {
            if (ModelState.IsValid)
            {
                prisoner.Status = PrisonerStatus.Admitted;

                if (prisoner.PrisonerId > 0) // Edit Existing
                {
                    var entry = db.Entry(prisoner);
                    entry.State = EntityState.Modified;
                    entry.Property(o => o.FMD1).IsModified = false;
                }
                else
                    db.Prisoners.Add(prisoner);

                db.SaveChanges();
                PrisonerId = prisoner.PrisonerId;

                return RedirectToAction("Admit", "FIR");
                //return RedirectToAction("Admit", "Admission");
            }

            PopulateDropDowns(prisoner);
            return View(prisoner);
        }

        //
        // GET: /Prisoner/

        public ActionResult Index(int? page = 1)
        {
            ViewBag.PageSize = ReportsController.PageSize;

            page = page ?? 1;
            var prisoners = db.Prisoners.Include(p => p.Jail).Include(p => p.PrisonerType).Include(p => p.PrisonerSubType).Include(p => p.Religion).Include(p => p.Occupation).Include(p => p.Nationality).Include(p => p.PresentCountry).Include(p => p.PresentProvince).Include(p => p.PresentDistrict).Include(p => p.PresentCity).Include(p => p.PresentPoliceStation).Include(p => p.PermanentCountry).Include(p => p.PermanentProvince).Include(p => p.PermanentDistrict).Include(p => p.PermanentCity).Include(p => p.PermanentPoliceStation).Include(p => p.FormalEducation).Include(p => p.TechnicalEducation).Include(p => p.ReligiousEducation);
            return View(prisoners.OrderBy(o => o.Name).ToPagedList(page.Value, ReportsController.PageSize));
        }

        /// <summary>
        /// Returns Array of Prisoners (ID, Name) as Json for TypeAhead
        /// </summary>
        /// <param name="id">query</param>
        /// <returns>Array of Prisoners</returns>
        public JsonResult List(string id)
        {
            var data = db.Prisoners.Select(o => new { o.PrisonerId, o.Name }).Where(o => o.Name.StartsWith(id)).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Prisoner/Details/5

        public ActionResult Details(int PrisonerId = 0)
        {
            Prisoner prisoner = db.Prisoners.Find(PrisonerId);

            if (prisoner == null)
                return View("Empty");

            return View(prisoner);
        }

        private Prisoner CreateNewPrisoner()
        {
            var prisoner = new Prisoner();
            
            var settings = db.Setttings.First();

            prisoner.JailId = settings.JailId;
            prisoner.PresentProvinceId = prisoner.PermanentProvinceId = settings.ProvinceId;
            prisoner.PresentDistrictId = prisoner.PermanentDistrictId = settings.DistrictId;
            prisoner.PresentCityId = prisoner.PermanentCityId = settings.CityId;

            prisoner.ApprovalStatus = ApprovalStatus.Pending;

            PopulateDropDowns(prisoner);

            return prisoner;
        }

        //
        // GET: /Prisoner/Create

        public ActionResult Create()
        {
            var prisoner = CreateNewPrisoner();
            return View(prisoner);
        }

        //
        // POST: /Prisoner/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Prisoner prisoner)
        {
            if (ModelState.IsValid)
            {
                db.Prisoners.Add(prisoner);
                db.SaveChanges();
                PrisonerId = prisoner.PrisonerId;
                return RedirectToAction("Details");
            }

            PopulateDropDowns(prisoner);
            return View(prisoner);
        }

        //
        // GET: /Prisoner/Edit/5

        public ActionResult Edit(int PrisonerId = 0)
        {
            Prisoner prisoner = db.Prisoners.Find(PrisonerId);

            if (prisoner == null)
                return View("Empty");

            PopulateDropDowns(prisoner);
            return View(prisoner);
        }

        //
        // POST: /Prisoner/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Prisoner prisoner)
        {
            if (ModelState.IsValid)
            {
                var entry = db.Entry(prisoner);
                entry.State = EntityState.Modified;
                entry.Property(o => o.FMD1).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            PopulateDropDowns(prisoner);
            return View(prisoner);
        }

        //
        // GET: /Prisoner/Delete/5

        public ActionResult Delete(int PrisonerId = 0)
        {
            Prisoner prisoner = db.Prisoners.Find(PrisonerId);

            if (prisoner == null)
                return View("Empty");

            return View(prisoner);
        }

        //
        // POST: /Prisoner/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int PrisonerId = 0)
        {
            Prisoner prisoner = db.Prisoners.Find(PrisonerId);
            db.Prisoners.Remove(prisoner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}