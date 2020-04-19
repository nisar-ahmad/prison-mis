using PRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;
using ReportManagement;
//using System.Data.Objects.SqlClient;
//using System.Data.Objects;

namespace PRS.Controllers
{
    public class ReportsController : PdfViewController
    {
        public static int PageSize = 100;

        protected PRSContext db = new PRSContext();
        
        public ActionResult GetPrisoners(IQueryable<Prisoner> prisonerQuery, PrisonerFilter filter)
        {
            var query = prisonerQuery.Select(o => new PrisonerViewModel
            {
                PrisonerId = o.PrisonerId,
                Name = o.Name,
                Parentage = o.FatherOrHusbandName,
                Status = o.Status,
                Category = o.Category,
                Class = o.Class,
                Gender = o.Gender,
                Age = o.Age,
                Height = o.Height,
                Weight = o.Weight,
                BloodGroup = o.BloodGroup,
                IdentificationMark1 = o.IdentificationMark1,
                IdentificationMark2 = o.IdentificationMark2,
                Scar = o.Scar,
                HealthOnAdmission = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).HealthOnAdmission,
                HealthOnRelease = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).HealthOnRelease,
                WeightOnAdmission = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).WeightOnAdmission,
                WeightOnRelease = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).WeightOnRelease,
                CommunicableDisease = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).CommunicableDisease.Name,
                District = o.PresentDistrict.Name,
                AdmissionId = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).AdmissionId,
                PrisonerNumber = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).PrisonerNumber,
                FIRCount = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).FIRs.Count(),
                FIRs = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).FIRs.OrderByDescending(f => f.FIRDate).FirstOrDefault().FIRNumber,
                PoliceStation = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).FIRs.OrderByDescending(f => f.FIRDate).FirstOrDefault().PoliceStation.Name,
                UnderSections = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).FIRs.OrderByDescending(f => f.FIRDate).FirstOrDefault().Sections.FirstOrDefault().Name,
                DateOfAdmission = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).DateOfAdmission,
                DateOfRelease = db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).DateOfRelease,
                TrialCourt = db.CourtHearings.OrderByDescending(a => a.DateOfHearing).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).Court.Name,
                DateOfHearing = db.CourtHearings.OrderByDescending(a => a.DateOfHearing).FirstOrDefault(a => a.PrisonerId == o.PrisonerId).DateOfHearing,
                Children = db.Children.Count(c => c.PrisonerId == o.PrisonerId)
            });

            foreach (var p in query)
            {
                var firs = db.FIRs.Where(o => o.AdmissionId == db.Admissions.OrderByDescending(a => a.DateOfAdmission).FirstOrDefault(a => a.PrisonerId == p.PrisonerId).AdmissionId);

                p.FIRs = "";
                p.PoliceStation = "";
                p.UnderSections = "";

                foreach (var fir in firs)
                {
                    p.FIRs += string.Format("{0}, ", fir.FIRNumber);
                    p.PoliceStation += string.Format("{0}, ", fir.PoliceStation.Name);

                    foreach(var section in fir.Sections)
                        p.UnderSections += string.Format("{0}, ", section.Name);

                }

                p.FIRs = p.FIRs.Remove(p.FIRs.Length - 2, 2);
                p.PoliceStation = p.PoliceStation.Remove(p.PoliceStation.Length - 2, 2);
                p.UnderSections = p.UnderSections.Remove(p.UnderSections.Length - 2, 2);
            }

            if (filter == null)
                filter = new PrisonerFilter();

            query = GetFilteredQuery(query, filter);
            query = query.OrderBy(o => o.Name);
            
            var list = query.ToPagedList(filter.Page.Value, PageSize);
            var report = new PrisonerReport(list, filter);

            PopulateFilters(filter, list);

            ViewBag.PageSize = PageSize;
            ViewBag.Count = query.Count();

            return View(report);
        }

        public IList<PrisonerViewModel> GetPrisoners(IQueryable<Admission> admissions)
        {
            var prisoners = new List<PrisonerViewModel>();

            foreach (Admission admission in admissions)
            {
                var p = new PrisonerViewModel();

                p.PrisonerId = admission.PrisonerId;
                p.PrisonerNumber = admission.PrisonerNumber;
                p.AdmissionId = admission.AdmissionId;
                p.Name = admission.Prisoner.Name;
                p.Parentage = admission.Prisoner.FatherOrHusbandName;
                p.Category = admission.Prisoner.Category;
                p.Status = admission.Prisoner.Status;

                p.DateOfAdmission = admission.DateOfAdmission;
                p.DateOfRelease = admission.DateOfRelease;
                p.HealthOnAdmission = admission.HealthOnAdmission;
                p.WeightOnAdmission = admission.WeightOnAdmission;


                p.HealthOnRelease = admission.HealthOnRelease;
                p.WeightOnRelease = admission.WeightOnRelease;
                p.District = admission.Prisoner.PresentDistrict.Name;

                var fir = admission.FIRs.Where(o => o.AdmissionId == p.AdmissionId).OrderByDescending(o => o.FIRDate).FirstOrDefault();

                if (fir != null)
                {
                    p.FIRs = fir.FIRNumber;

                    if (fir.PoliceStation != null)
                        p.PoliceStation = fir.PoliceStation.Name;

                    var section = fir.Sections.FirstOrDefault();

                    if (section != null)
                        p.UnderSections = section.Name;
                }

                var ch = db.CourtHearings.Where(o => o.AdmissionId == admission.AdmissionId).OrderByDescending(o => o.DateOfHearing).FirstOrDefault();

                if (ch != null)
                {
                    p.TrialCourt = ch.Court.Name;
                    p.DateOfHearing = ch.DateOfHearing;
                }

                var endDate = DateTime.Now;

                if (p.DateOfRelease != null)
                    endDate = p.DateOfRelease.Value;

                DateDifference period = new DateDifference(endDate, admission.DateOfAdmission);
                p.Years = period.Years;
                p.Months = period.Months;
                p.Days = period.Days;

                prisoners.Add(p);
            }

            return prisoners;
        }

        private void PopulateFilters(PrisonerFilter filter, IEnumerable<PrisonerViewModel> query)
        {
            ViewBag.Category = new SelectList(Enum.GetValues(typeof(PrisonerCategory)), filter.Category);
            ViewBag.Status = new SelectList(Enum.GetValues(typeof(PrisonerStatus)), filter.Status);
            ViewBag.Class = new SelectList(Enum.GetValues(typeof(PrisonerClass)), filter.Class);
            ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)), filter.Gender);

            ViewBag.District = new SelectList(db.Districts.Select(q => q.Name).OrderBy(o => o), filter.District);
            ViewBag.FIR = new SelectList(db.FIRs.Select(q => q.FIRNumber).OrderBy(o => o), filter.FIRNumber);
            ViewBag.Section = new SelectList(db.Sections.Select(q => q.Name).OrderBy(o => o), filter.Section);
            ViewBag.PoliceStation = new SelectList(db.PoliceStations.Select(q => q.Name).OrderBy(o => o), filter.PoliceStation);
            ViewBag.TrialCourt = new SelectList(db.Courts.Select(q => q.Name).OrderBy(o => o), filter.TrialCourt);

            ViewBag.Height = new SelectList(Lists.Heights.OrderBy(o => o), filter.Height);
            ViewBag.BloodGroup = new SelectList(Lists.BloodGroups.OrderBy(o => o), filter.BloodGroup);
            ViewBag.CommunicableDisease = new SelectList(db.Diseases.Select(q => q.Name).OrderBy(o => o), filter.CommunicableDisease); 

            ViewBag.PrisonerNumber = filter.PrisonerNumber;
            ViewBag.Name = filter.Name;
            ViewBag.Parentage = filter.Parentage;

            // Medical Info
            ViewBag.Age = filter.Age;
            ViewBag.IdentificationMark1 = filter.IdentificationMark1;
            ViewBag.Weight = filter.Weight;


            if (filter.AdmissionFrom != null)
                ViewBag.AdmissionFrom = filter.AdmissionFrom.Value.ToString("dd-MMM-yyyy");

            if (filter.AdmissionTo != null)
                ViewBag.AdmissionTo = filter.AdmissionTo.Value.ToString("dd-MMM-yyyy");

            if (filter.ReleaseFrom != null)
                ViewBag.ReleaseFrom = filter.ReleaseFrom.Value.ToString("dd-MMM-yyyy");

            if (filter.ReleaseTo != null)
                ViewBag.ReleaseTo = filter.ReleaseTo.Value.ToString("dd-MMM-yyyy");

            if (filter.HearingFrom != null)
                ViewBag.HearingFrom = filter.HearingFrom.Value.ToString("dd-MMM-yyyy");

            if (filter.HearingTo != null)
                ViewBag.HearingTo = filter.HearingTo.Value.ToString("dd-MMM-yyyy");
        }

        private IQueryable<PrisonerViewModel> GetFilteredQuery(IQueryable<PrisonerViewModel> query, PrisonerFilter filter)
        {
            if (filter.Category != null)
                query = query.Where(o => o.Category == filter.Category);

            if (filter.Status != null)
                query = query.Where(o => o.Status == filter.Status);

            if (filter.Class != null)
                query = query.Where(o => o.Class == filter.Class);

            if (filter.Gender != null)
                query = query.Where(o => o.Gender == filter.Gender);

            if (!string.IsNullOrWhiteSpace(filter.PrisonerNumber))
                query = query.Where(o => o.PrisonerNumber.Contains(filter.PrisonerNumber));

            if (filter.Age != null)
                query = query.Where(o => o.Age == filter.Age);

            if (!string.IsNullOrWhiteSpace(filter.Weight))
                query = query.Where(o => o.Weight.Contains(filter.Weight) || o.WeightOnAdmission.Contains(filter.Weight) || o.WeightOnRelease.Contains(filter.Weight));

            if (!string.IsNullOrWhiteSpace(filter.Height))
                query = query.Where(o => o.Height == filter.Height);

            if (!string.IsNullOrWhiteSpace(filter.BloodGroup))
                query = query.Where(o => o.BloodGroup == filter.BloodGroup);

            if (!string.IsNullOrWhiteSpace(filter.IdentificationMark1))
                query = query.Where(o => o.IdentificationMark1.Contains(filter.IdentificationMark1));

            if (!string.IsNullOrWhiteSpace(filter.CommunicableDisease))
                query = query.Where(o => o.CommunicableDisease == filter.CommunicableDisease);

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(o => o.Name.Contains(filter.Name));

            if (!string.IsNullOrWhiteSpace(filter.Parentage))
                query = query.Where(o => o.Parentage.Contains(filter.Parentage));

            if (!string.IsNullOrWhiteSpace(filter.District))
                query = query.Where(o => o.District == filter.District);

            if (!string.IsNullOrWhiteSpace(filter.FIRNumber))
                query = query.Where(o => o.FIRs.Contains(filter.FIRNumber));

            if (!string.IsNullOrWhiteSpace(filter.Section))
                query = query.Where(o => o.UnderSections.Contains(filter.Section));

            if (!string.IsNullOrWhiteSpace(filter.PoliceStation))
                query = query.Where(o => o.PoliceStation.Contains(filter.PoliceStation));

            if (!string.IsNullOrWhiteSpace(filter.TrialCourt))
                query = query.Where(o => o.TrialCourt == filter.TrialCourt);

            if (filter.AdmissionFrom != null)
                query = query.Where(o => o.DateOfAdmission >= filter.AdmissionFrom);

            if (filter.AdmissionTo != null)
                query = query.Where(o => o.DateOfAdmission <= filter.AdmissionTo);

            if (filter.ReleaseFrom != null)
                query = query.Where(o => o.DateOfRelease >= filter.ReleaseFrom);

            if (filter.ReleaseTo != null)
                query = query.Where(o => o.DateOfRelease <= filter.ReleaseTo);

            if (filter.HearingFrom != null)
                query = query.Where(o => o.DateOfHearing >= filter.HearingFrom);

            if (filter.HearingTo != null)
                query = query.Where(o => o.DateOfHearing <= filter.HearingTo);

            return query;
        }

        public ActionResult AllPrisoners(PrisonerCategory? Category, PrisonerStatus? Status, PrisonerClass? Class, Gender? Gender, int? Age,
                                         string District, string FIR, string Section, string Name, string Parentage, string PrisonerNumber,
                                         string PoliceStation, string TrialCourt, DateTime? AdmissionFrom,
                                         DateTime? AdmissionTo, DateTime? ReleaseFrom, DateTime? ReleaseTo,
                                         DateTime? HearingFrom, DateTime? HearingTo, int? Page = 1)
        {
            var filter = new PrisonerFilter
            {
                Category = Category,
                Status = Status,
                Class = Class,
                Gender = Gender,
                Age = Age,
                AdmissionFrom = AdmissionFrom,
                AdmissionTo = AdmissionTo,
                ReleaseFrom = ReleaseFrom,
                ReleaseTo = ReleaseTo,
                HearingFrom = HearingFrom,
                HearingTo = HearingTo,
                PoliceStation = PoliceStation,
                TrialCourt = TrialCourt,
                District = District,
                FIRNumber = FIR,
                Section = Section,
                Name = Name,
                Parentage = Parentage,
                PrisonerNumber = PrisonerNumber,
                Page = Page
            };

            var query = db.Prisoners.Include(o => o.PresentDistrict);
            return GetPrisoners(query, filter);
        }

        public ActionResult Juveniles(PrisonerCategory? Category, PrisonerStatus? Status, PrisonerClass? Class, Gender? Gender, int? Age,
                                         string District, string FIR, string Section, string Name, string Parentage, string PrisonerNumber,
                                         string PoliceStation, string TrialCourt, DateTime? AdmissionFrom,
                                         DateTime? AdmissionTo, DateTime? ReleaseFrom, DateTime? ReleaseTo,
                                         DateTime? HearingFrom, DateTime? HearingTo, int? Page = 1)
        {
            var filter = new PrisonerFilter
            {
                Category = Category,
                Status = Status,
                Class = Class,
                Gender = Gender,
                Age = Age,
                AdmissionFrom = AdmissionFrom,
                AdmissionTo = AdmissionTo,
                ReleaseFrom = ReleaseFrom,
                ReleaseTo = ReleaseTo,
                HearingFrom = HearingFrom,
                HearingTo = HearingTo,
                PoliceStation = PoliceStation,
                TrialCourt = TrialCourt,
                District = District,
                FIRNumber = FIR,
                Section = Section,
                Name = Name,
                Parentage = Parentage,
                PrisonerNumber = PrisonerNumber,
                Page = Page
            };
            var query = db.Prisoners.Include(o => o.PresentDistrict).Where(o => o.Age > 0 && o.Age < 18);
            return GetPrisoners(query, filter);
        }

        public ActionResult Females(PrisonerCategory? Category, PrisonerStatus? Status, PrisonerClass? Class, int? Age,
                                         string District, string FIR, string Section, string Name, string Parentage, string PrisonerNumber,
                                         string PoliceStation, string TrialCourt, DateTime? AdmissionFrom,
                                         DateTime? AdmissionTo, DateTime? ReleaseFrom, DateTime? ReleaseTo,
                                         DateTime? HearingFrom, DateTime? HearingTo, int? Page = 1)
        {
            var filter = new PrisonerFilter
            {
                Category = Category,
                Status = PrisonerStatus.Admitted,
                Class = Class,
                Gender = Gender.Female,
                Age = Age,
                AdmissionFrom = AdmissionFrom,
                AdmissionTo = AdmissionTo,
                ReleaseFrom = ReleaseFrom,
                ReleaseTo = ReleaseTo,
                HearingFrom = HearingFrom,
                HearingTo = HearingTo,
                PoliceStation = PoliceStation,
                TrialCourt = TrialCourt,
                District = District,
                FIRNumber = FIR,
                Section = Section,
                Name = Name,
                Parentage = Parentage,
                PrisonerNumber = PrisonerNumber,
                Page = Page
            };

            var query = db.Prisoners.Include(o => o.PresentDistrict);
            return GetPrisoners(query, filter);
        }

        public ActionResult AdmittedPrisoners(PrisonerCategory? Category, PrisonerClass? Class, Gender? Gender, int? Age,
                                         string District, string FIR, string Section, string Name, string Parentage, string PrisonerNumber,
                                         string PoliceStation, string TrialCourt, DateTime? AdmissionFrom,
                                         DateTime? AdmissionTo, DateTime? ReleaseFrom, DateTime? ReleaseTo,
                                         DateTime? HearingFrom, DateTime? HearingTo, int? Page = 1)
        {
            var filter = new PrisonerFilter
            {
                Category = Category,
                Status = PrisonerStatus.Admitted,
                Class = Class,
                Gender = Gender,
                Age = Age,
                AdmissionFrom = AdmissionFrom,
                AdmissionTo = AdmissionTo,
                ReleaseFrom = ReleaseFrom,
                ReleaseTo = ReleaseTo,
                HearingFrom = HearingFrom,
                HearingTo = HearingTo,
                PoliceStation = PoliceStation,
                TrialCourt = TrialCourt,
                District = District,
                FIRNumber = FIR,
                Section = Section,
                Name = Name,
                Parentage = Parentage,
                PrisonerNumber = PrisonerNumber,
                Page = Page
            };

            var query = db.Prisoners.Include(o => o.PresentDistrict);
            return GetPrisoners(query, filter);
        }

        public ActionResult ReleasedPrisoners(PrisonerCategory? Category,  PrisonerClass? Class, Gender? Gender, int? Age,
                                         string District, string FIR, string Section, string Name, string Parentage, string PrisonerNumber,
                                         string PoliceStation, string TrialCourt, DateTime? AdmissionFrom,
                                         DateTime? AdmissionTo, DateTime? ReleaseFrom, DateTime? ReleaseTo,
                                         DateTime? HearingFrom, DateTime? HearingTo, int? Page = 1)
        {
            var filter = new PrisonerFilter
            {
                Category = Category,
                Status = PrisonerStatus.Released,
                Class = Class,
                Gender = Gender,
                Age = Age,
                AdmissionFrom = AdmissionFrom,
                AdmissionTo = AdmissionTo,
                ReleaseFrom = ReleaseFrom,
                ReleaseTo = ReleaseTo,
                HearingFrom = HearingFrom,
                HearingTo = HearingTo,
                PoliceStation = PoliceStation,
                TrialCourt = TrialCourt,
                District = District,
                FIRNumber = FIR,
                Section = Section,
                Name = Name,
                Parentage = Parentage,
                PrisonerNumber = PrisonerNumber,
                Page = Page
            };

            var query = db.Prisoners.Include(o => o.PresentDistrict);
            return GetPrisoners(query, filter);
        }

        public ActionResult PrisonersHealth(PrisonerCategory? Category, PrisonerStatus? Status, PrisonerClass? Class, Gender? Gender, int? Age,
                                 string District, string Height, string Weight, string BloodGroup, string CommunicableDisease, string PrisonerNumber,
                                 string IdentificationMark1, string Name, DateTime? AdmissionFrom,
                                 DateTime? AdmissionTo, DateTime? ReleaseFrom, DateTime? ReleaseTo,
                                 DateTime? HearingFrom, DateTime? HearingTo, int? Page = 1)
        {
            var filter = new PrisonerFilter
            {
                Category = Category,
                Status = Status,
                Class = Class,
                Gender = Gender,
                Age = Age,
                AdmissionFrom = AdmissionFrom,
                AdmissionTo = AdmissionTo,
                ReleaseFrom = ReleaseFrom,
                ReleaseTo = ReleaseTo,
                HearingFrom = HearingFrom,
                HearingTo = HearingTo,
                District = District,
                Height = Height,
                Weight = Weight,
                BloodGroup = BloodGroup,
                IdentificationMark1 = IdentificationMark1,
                CommunicableDisease = CommunicableDisease,
                Name = Name,
                PrisonerNumber = PrisonerNumber,
                Page = Page
            };

            var query = db.Prisoners.Include(o => o.PresentDistrict);
            return GetPrisoners(query, filter);
        }

        public ActionResult Stats()
        {
            GetStats();
            return View("Index");
        }

        public ActionResult Index()
        {
            GetStats();
            return View();
        }

        private void GetStats()
        {
            int jailID = db.Setttings.FirstOrDefault().JailId;

            ViewBag.Capacity = db.Jails.FirstOrDefault(o => o.JailId == jailID).Capacity;

            ViewBag.Admitted = db.Prisoners.Count(o => o.Status == PrisonerStatus.Admitted);
            ViewBag.CheckedIn = db.Prisoners.Count(o => o.Status == PrisonerStatus.Admitted && db.CheckInOuts.Where(c => c.PrisonerId == o.PrisonerId).OrderByDescending(c => c.DateOfCheckInOut).FirstOrDefault().Status == CheckInOutStatus.CheckIn);
            ViewBag.CheckedOut = db.Prisoners.Count(o => o.Status == PrisonerStatus.Admitted && db.CheckInOuts.Where(c => c.PrisonerId == o.PrisonerId).OrderByDescending(c => c.DateOfCheckInOut).FirstOrDefault().Status == CheckInOutStatus.CheckOut);

            ViewBag.Released = db.Prisoners.Count(o => o.Status == PrisonerStatus.Released);

            ViewBag.Convicted = db.Prisoners.Count(o => o.Category == PrisonerCategory.Convicted && o.Status == PrisonerStatus.Admitted);
            ViewBag.UnderTrial = db.Prisoners.Count(o => o.Category == PrisonerCategory.UnderTrial && o.Status == PrisonerStatus.Admitted);
            ViewBag.Condemned = db.Prisoners.Count(o => o.Category == PrisonerCategory.Condemned && o.Status == PrisonerStatus.Admitted);
            ViewBag.Detainee = db.Prisoners.Count(o => o.Category == PrisonerCategory.Detainee && o.Status == PrisonerStatus.Admitted);

            ViewBag.Male = db.Prisoners.Count(o => o.Gender == Gender.Male && o.Status == PrisonerStatus.Admitted);
            ViewBag.Female = db.Prisoners.Count(o => o.Gender == Gender.Female && o.Status == PrisonerStatus.Admitted);
        }

        [HttpPost]
        public ActionResult Prisoners(PrisonerCategory? Category, Gender? Gender, PrisonerClass? Class, DateTime? AdmissionFrom, DateTime? AdmissionTo, DateTime? ReleaseFrom, DateTime? ReleaseTo, string Print)
        {
            bool isPrint = !string.IsNullOrEmpty(Print);

            return GetPrisoners(Category, Gender, Class, AdmissionFrom, AdmissionTo, ReleaseFrom, ReleaseTo, isPrint);
        }

        public ActionResult Prisoners(int? page = 1)
        {
            return GetPrisoners(null, null, null, null, null, null, null, false, page);
        }

        private ActionResult GetPrisoners(PrisonerCategory? category, Gender? gender, PrisonerClass? Class, DateTime? AdmissionFrom, DateTime? AdmissionTo, DateTime? ReleaseFrom, DateTime? ReleaseTo, bool isPrint = false, int? page = 1)
        {
            ViewBag.Category = new SelectList(Enum.GetValues(typeof(PrisonerCategory)), category);
            ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)), gender);
            ViewBag.Class = new SelectList(Enum.GetValues(typeof(PrisonerClass)), Class);

            var prisoners = db.Admissions.Where(o => o.Prisoner.Status == PrisonerStatus.Admitted);

            if (category.HasValue)
                prisoners = prisoners.Where(o => o.Prisoner.Category == category);

            if (Class.HasValue)
                prisoners = prisoners.Where(o => o.Prisoner.Class == Class);

            if (gender.HasValue)
                prisoners = prisoners.Where(o => o.Prisoner.Gender == gender);

            if (AdmissionFrom.HasValue)
            {
                ViewBag.AdmissionFrom = AdmissionFrom.Value.ToString("dd-MMM-yyyy");
                prisoners = prisoners.Where(o => o.DateOfAdmission >= AdmissionFrom);
            }

            if (AdmissionTo.HasValue)
            {
                ViewBag.AdmissionTo = AdmissionTo.Value.ToString("dd-MMM-yyyy");
                prisoners = prisoners.Where(o => o.DateOfAdmission <= AdmissionTo);
            }

            if (ReleaseFrom.HasValue)
            {
                ViewBag.ReleaseFrom = ReleaseFrom.Value.ToString("dd-MMM-yyyy");
                prisoners = prisoners.Where(o => o.DateOfRelease >= ReleaseFrom);
            }

            if (ReleaseTo.HasValue)
            {
                ViewBag.ReleaseTo = ReleaseTo.Value.ToString("dd-MMM-yyyy");
                prisoners = prisoners.Where(o => o.DateOfRelease <= ReleaseTo);
            }

            if (isPrint == true)
            {
                return ViewPdf("Prisoner Admissions", "Print", prisoners.ToList());
            }
            else
                return View(prisoners.OrderBy(o => o.Prisoner.Name).ToPagedList(page.Value, 100));
        }

        public ActionResult Convicted(int? page = 1)
        {
            return View(db.Admissions.Include(o => o.Prisoner.PresentDistrict).Where(o => o.Prisoner.Category == PrisonerCategory.Convicted && o.Prisoner.Status == PrisonerStatus.Admitted).OrderBy(o => o.Prisoner.Name).ToPagedList(page.Value, 100));
        }

        [HttpPost]
        public ActionResult Convicted(string Print)
        {
            return ViewPdf("Alphabetical List of Convicted Prisoners", "Print",
                db.Admissions.Include(o => o.Prisoner.PresentDistrict).Where(o => o.DateOfRelease == null && o.Prisoner.Category == PrisonerCategory.Convicted && o.Prisoner.Status == PrisonerStatus.Admitted).OrderBy(o => o.Prisoner.Name).ToList());
        }

        public ActionResult UnderTrial(int? page = 1)
        {
            return View(db.Admissions.Include(o => o.Prisoner.PresentDistrict).Where(o => o.DateOfRelease == null && o.Prisoner.Category == PrisonerCategory.UnderTrial && o.Prisoner.Status == PrisonerStatus.Admitted && o.DateOfRelease == null).OrderBy(o => o.Prisoner.Name).ToPagedList(page.Value, 100));
        }

        [HttpPost]
        public ActionResult UnderTrial(string Print)
        {
            return ViewPdf("Alphabetical List of Convicted Prisoners", "Print",
                db.Admissions.Include(o => o.Prisoner.PresentDistrict).Where(o => o.DateOfRelease == null && o.Prisoner.Category == PrisonerCategory.UnderTrial && o.Prisoner.Status == PrisonerStatus.Admitted).OrderBy(o => o.Prisoner.Name).ToList());
        }

        public ActionResult ReleaseDiary(DateTime? StartDate = null, DateTime? EndDate = null)
        {
            return GetReleaseDiary(StartDate, EndDate, false);
        }

        [HttpPost]
        public ActionResult ReleaseDiary(DateTime? StartDate = null, DateTime? EndDate = null, string Print = null)
        {
            return GetReleaseDiary(StartDate, EndDate, !string.IsNullOrEmpty(Print));
        }

        public ActionResult AdmissionDiary(DateTime? StartDate = null, DateTime? EndDate = null)
        {
            return GetAdmissionDiary(StartDate, EndDate, false);
        }

        [HttpPost]
        public ActionResult AdmissionDiary(DateTime? StartDate = null, DateTime? EndDate = null, string Print = null)
        {
            return GetAdmissionDiary(StartDate, EndDate, !string.IsNullOrEmpty(Print));
        }

        public ActionResult Visitors(DateTime? StartDate = null, DateTime? EndDate = null, int? BatchNumber = null)
        {
            var query = GetVisitors(StartDate, EndDate, BatchNumber, false);
            return View(query);
        }

        [HttpPost]
        public ActionResult Visitors(DateTime? StartDate = null, DateTime? EndDate = null, int? BatchNumber = null, string Print = null)
        {
            var query = GetVisitors(StartDate, EndDate, BatchNumber, !string.IsNullOrEmpty(Print));
            return View(query);
        }

        private ActionResult GetAdmissionDiary(DateTime? StartDate, DateTime? EndDate, bool isPrint = false)
        {
            if (StartDate == null)
                StartDate = DateTime.Now;

            if (EndDate == null)
                EndDate = DateTime.Now;

            var query = db.Admissions.Include(o => o.Prisoner.PresentDistrict).Include(o => o.FIRs).Include(o => o.Prisoner).Where(o => DbFunctions.TruncateTime(o.DateOfAdmission) >= DbFunctions.TruncateTime(StartDate) &&
                                            DbFunctions.TruncateTime(o.DateOfAdmission) <= DbFunctions.TruncateTime(EndDate)).OrderBy(o => o.Prisoner.Name);

            return GetPrisoners(query, StartDate, EndDate, "Admitted", isPrint);
        }

        private ActionResult GetReleaseDiary(DateTime? StartDate, DateTime? EndDate, bool isPrint = false)
        {
            if (StartDate == null)
                StartDate = DateTime.Now;

            if (EndDate == null)
                EndDate = DateTime.Now;

            var query = db.Admissions.Include(o => o.Prisoner).Where(o => DbFunctions.TruncateTime(o.DateOfRelease) >= DbFunctions.TruncateTime(StartDate) &&
                                            DbFunctions.TruncateTime(o.DateOfRelease) <= DbFunctions.TruncateTime(EndDate)).OrderBy(o => o.Prisoner.Name);

            return GetPrisoners(query, StartDate, EndDate, "Released", isPrint);
        }

        private IEnumerable<Visit> GetVisitors(DateTime? StartDate = null, DateTime? EndDate = null, int? BatchNumber = null, bool isPrint = false)
        {
            if (StartDate == null)
                StartDate = DateTime.Now;

            if (EndDate == null)
                EndDate = DateTime.Now;

            var query = db.Visits.Where(o => DbFunctions.TruncateTime(o.DateOfVisit) >= DbFunctions.TruncateTime(StartDate) &&
                            DbFunctions.TruncateTime(o.DateOfVisit) <= DbFunctions.TruncateTime(EndDate));

            if (BatchNumber != null && BatchNumber > 0)
                query = query.Where(o => o.BatchNumber == BatchNumber.Value);

            var list = query.OrderBy(o => o.DateOfVisit).ToList();

            ViewBag.StartDate = StartDate.Value.ToString("dd-MMM-yyyy");
            ViewBag.EndDate = EndDate.Value.ToString("dd-MMM-yyyy");
            ViewBag.BatchNumber = BatchNumber;
            ViewBag.Total = list.Count;

            return list;
        }

        private ActionResult GetPrisoners(IQueryable<Admission> list, DateTime? startDate = null, DateTime? endDate = null, string reportTitle = null, bool isPrint = false)
        {
            var prisoners = GetPrisoners(list);

            ViewBag.StartDate = startDate.Value.ToString("dd-MMM-yyyy");
            ViewBag.EndDate = endDate.Value.ToString("dd-MMM-yyyy");
            ViewBag.Total = prisoners.Count;

            if (isPrint == true)
            {
                string title = string.Format("Prisoners {0} between {1:dd-MMM-yyyy} and {2:dd-MMM-yyyy}", reportTitle, startDate, endDate);
                return ViewPdf(title, "PrintDiary", prisoners);
            }
            else
                return View(prisoners);
        }
    }
}
