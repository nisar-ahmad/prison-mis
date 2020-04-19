using PRS.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PRS.Controllers
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Role[] roles)
            : base()
        {
            Roles = String.Join(",", roles);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
         
            //If its an unauthorized/timed out ajax request go to top window and redirect to logon.
            if (filterContext.Result is HttpUnauthorizedResult && filterContext.HttpContext.Request.IsAjaxRequest())
                filterContext.Result = new JavaScriptResult() { Script = "top.location='/Error;" };
 
            //If authorization results in HttpUnauthorizedResult, redirect to error page instead of Logon page.
            if(filterContext.Result is HttpUnauthorizedResult)
                filterContext.Result = new RedirectResult("~/Error/Unauthorized");
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
 
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            //Bypass role check if user is SuperAdmin, prevents having to add SuperAdmin role across the whole project.
            if (httpContext.User.IsInRole(Role.Admin.ToString()) || httpContext.User.IsInRole(Role.SuperAdmin.ToString()))
                return true;
 
            //If no roles are supplied to the attribute just check that the user is logged in.
            if(string.IsNullOrWhiteSpace(Roles))
                return true;
 
            //Check to see if any of the authorized roles fits into any assigned roles only if roles have been supplied.
            if (Roles.Split(",".ToCharArray()).Any(httpContext.User.IsInRole))
                return true;
 
            return false;
        }
    }

    public class PRSBaseController : BootstrapBaseController
    {
        protected PRSContext db = new PRSContext();
        protected int PrisonerId = 0;
        protected int AdmissionId = 0;
        protected int MedicalTreatmentId = 0;
        protected int FIRId = 0;
        protected int CourtHearingId = 0;
        protected int Remand = 0;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            db.UserName = User.Identity.Name;

            if (string.IsNullOrWhiteSpace(Request["PrisonerId"]) == false)
            {
                PrisonerId = Convert.ToInt32(Request["PrisonerId"]);

                // Get latest admission
                var admission = db.Admissions.Include(o => o.Prisoner).Select(o => new { o.AdmissionId, o.PrisonerNumber, o.DateOfAdmission, o.PrisonerId, o.Prisoner.Name, o.Prisoner.DateOfBirth, o.Prisoner.Age, o.Prisoner.FMD1, o.Prisoner.Gender, o.Prisoner.Status }).OrderByDescending(o => o.AdmissionId).FirstOrDefault(o => o.PrisonerId == PrisonerId);

                string name = null;
                string number = null;
                string fmd = null;
                Gender gender = Gender.Male;
                string admissionId = "";
                int age = 0;
                PrisonerStatus status = PrisonerStatus.Admitted;

                DateTime? dob = null;

                if (admission != null)
                {
                    admissionId = admission.AdmissionId.ToString();
                    name = admission.Name;
                    fmd = admission.FMD1;
                    number = admission.PrisonerNumber;
                    dob = admission.DateOfBirth;
                    gender = admission.Gender;
                    age = admission.Age;
                    status = admission.Status;
                }
                else
                {
                    var prisoner = db.Prisoners.Select(o => new { o.PrisonerId, o.Name, o.DateOfBirth, o.Age, o.FMD1, o.Gender, o.Status }).FirstOrDefault(o => o.PrisonerId == PrisonerId);

                    if (prisoner != null)
                    {
                        name = prisoner.Name;
                        fmd = prisoner.FMD1;
                        dob = prisoner.DateOfBirth;
                        gender = prisoner.Gender;
                        age = prisoner.Age;
                        status = prisoner.Status;
                    }
                }

                ViewBag.ComputerNumber = admissionId;
                ViewBag.PrisonerName = name;
                ViewBag.Fingerprint = (fmd != null ? "Captured" : "NOT Captured");
                ViewBag.PrisonerNumber = number;
                ViewBag.PrisonerGender = gender;
                ViewBag.PrisonerStatus = status;

                if (dob != null)
                { 
                    DateDifference period = new DateDifference(DateTime.Now, dob.Value);
                    ViewBag.PrisonerAge = period.Years;
                }
                else
                    ViewBag.PrisonerAge = age;
            }

            if (string.IsNullOrWhiteSpace(Request["AdmissionId"]) == false)
                AdmissionId = Convert.ToInt32(Request["AdmissionId"]);

            if (string.IsNullOrWhiteSpace(Request["FIRId"]) == false)
                FIRId = Convert.ToInt32(Request["FIRId"]);

            if (string.IsNullOrWhiteSpace(Request["CourtHearingId"]) == false)
                CourtHearingId = Convert.ToInt32(Request["CourtHearingId"]);

            if (string.IsNullOrWhiteSpace(Request["MedicalTreatmentId"]) == false)
                MedicalTreatmentId = Convert.ToInt32(Request["MedicalTreatmentId"]);

            if (string.IsNullOrWhiteSpace(Request["Remand"]) == false)
                Remand = Convert.ToInt32(Request["Remand"]);
        }

        public Admission GetLatestAdmission(int prisonerId = 0)
        {
            if (prisonerId == 0)
                prisonerId = PrisonerId;

            return db.Admissions.OrderByDescending(o => o.DateOfAdmission).FirstOrDefault(o => o.PrisonerId == prisonerId);
        }

        public int GetLatestAdmissionId(int prisonerId = 0)
        {
            if (prisonerId == 0)
                prisonerId = PrisonerId;

            return db.Admissions.Where(o => o.PrisonerId == prisonerId).OrderByDescending(o => o.DateOfAdmission).Select(o => o.AdmissionId).FirstOrDefault();
        }

        private object GetRouteValues()
        {
            if (MedicalTreatmentId > 0)
                return new { PrisonerId = PrisonerId, AdmissionId = AdmissionId, MedicalTreatmentId = MedicalTreatmentId };
            else if (CourtHearingId > 0)
                return new { PrisonerId = PrisonerId, AdmissionId = AdmissionId, CourtHearingId = CourtHearingId, FIRId = FIRId };
            else if (FIRId > 0)
                return new { PrisonerId = PrisonerId, AdmissionId = AdmissionId, FIRId = FIRId };
            else if(Remand > 0)
                return new { PrisonerId = PrisonerId, AdmissionId = AdmissionId, Remand = Remand };
            else if (AdmissionId > 0)
                return new { PrisonerId = PrisonerId, AdmissionId = AdmissionId };
            else if (PrisonerId > 0)
                return new { PrisonerId = PrisonerId };
            else
                return null;
        }

        protected string CalculateSentence(int PrisonerId = 0)
        {
            if (PrisonerId == 0)
                PrisonerId = this.PrisonerId;

            var date = string.Empty;
            var admission = GetLatestAdmission(PrisonerId);

            var firDecisions = db.CourtDecisions.Include(i => i.CourtDecisionSections).Where(o => o.AdmissionId == admission.AdmissionId);
            var admissionDate = admission.DateOfAdmission;

            var maxReleaseDate = admissionDate;

            var concurrentFIRDate = admissionDate;
            var consecutiveFIRDate = admissionDate;
            var otherFIRDate = admissionDate;

            foreach (var f in firDecisions)
            {
                if (f.DateOfSentenceStart == null)
                    break;

                var sentenceStartDate = f.DateOfSentenceStart.Value;
                var concurrentDate = sentenceStartDate;
                var consecutiveDate = sentenceStartDate;

                foreach (var s in f.CourtDecisionSections)
                {
                    var years = s.SentenceYears;
                    var months = s.SentenceMonths;
                    var days = s.SentenceDays;

                    if (!s.IsFinePaid)
                    {
                        years += s.FurtherSentenceYears;
                        months += s.FurtherSentenceMonths;
                        days += s.FurtherSentenceDays;
                    }

                    if (s.CourtDecisionType == CourtDecisionType.Concurrent)
                    {
                        var endDate = sentenceStartDate.AddYears(years).AddMonths(months).AddDays(days);

                        if (endDate > concurrentDate)
                            concurrentDate = endDate;
                    }
                    else
                        consecutiveDate = consecutiveDate.AddYears(years).AddMonths(months).AddDays(days);
                }

                var sentenceEndDate = consecutiveDate > concurrentDate ? consecutiveDate : concurrentDate;
                var sentencePeriod = sentenceEndDate - sentenceStartDate;

                var utStartDate = f.DateOfUnderTrialStart ?? admissionDate;
                var utEndDate = f.DateOfUnderTrialEnd ?? sentenceStartDate;

                if (f.Is382BApplied)
                {
                    var utPeriod = utEndDate - utStartDate;

                    if (utPeriod > sentencePeriod)
                        utPeriod = sentencePeriod;

                    sentencePeriod -= utPeriod;
                }

                if (f.CourtDecisionType == CourtDecisionType.Consecutive)
                    consecutiveFIRDate = consecutiveFIRDate.Add(sentencePeriod);
                else
                {
                    var startDate = utStartDate;

                    if (f.CourtDecisionType == CourtDecisionType.NotApplicable)
                        startDate = sentenceStartDate;

                    var endDate = startDate.Add(sentencePeriod);

                    if (endDate > concurrentFIRDate)
                        concurrentFIRDate = endDate;
                }
            }

            var releaseDate = consecutiveFIRDate > concurrentFIRDate ? consecutiveFIRDate : concurrentFIRDate;

            if (releaseDate > admissionDate)
            {
                admission.DateOfReleaseWithFullSentence = releaseDate;
                db.SaveChanges();

                date = releaseDate.ToString("dd-MMM-yyyy");
            }

            return date;
        }

        protected new RedirectToRouteResult RedirectToAction(string actionName, string controllerName)
        {
            var routeValues = GetRouteValues();
            return base.RedirectToAction(actionName, controllerName, routeValues);
        }

        protected RedirectToRouteResult RedirectToMedicalTreatments()
        {
            var routeValues = new { PrisonerId = PrisonerId, UserType = "Medical" };
            return base.RedirectToAction("Index", "MedicalTreatment", routeValues);
        }

        protected new RedirectToRouteResult RedirectToAction(string actionName)
        {
            var routeValues = GetRouteValues();
            return base.RedirectToAction(actionName, routeValues);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public void ValidationError()
        {
            Error("Please enter valid data! مکمل فارم پر کریں");
        }
    }
}