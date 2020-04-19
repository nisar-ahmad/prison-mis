using PRS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NavigationRoutes;
using PRS.Models;

namespace PRS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //RenderNavigation();
        }

        public static void RenderNavigation()
        {
            //RouteCollection routes = RouteTable.Routes;

            //routes.MapNavigationRoute<HomeController>("Home", c => c.Index());

            //routes.MapNavigationRoute("Prisoners", "Prisoners", "Prisoner", null);
            ////.AddChildRoute<PrisonerController>("Prisoners", c => c.Index())
            ////.AddChildRoute<PrisonerAdmissionController>("Admissions", c => c.Index())
            ////.AddChildRoute<CourtHearingController>("Hearings", c => c.Index())
            ////.AddChildRoute<MedicalExamController>("Health", c => c.Index())
            ////.AddChildRoute<PrisonerPropertyController>("Belongings", c => c.Index())
            ////.AddChildRoute<PrisonOffenceController>("Discipline", c => c.Index())
            ////.AddChildRoute<ExternalContactController>("External Contacts", c => c.Index());

            //routes.MapNavigationRoute("Reports", "Reports", "Reports", null)
            //    .AddChildRoute<ReportsController>("Statistics", c => c.Stats())
            //    .AddChildRoute<ReportsController>("Release Diary", c => c.ReleaseDiary())
            //    .AddChildRoute<ReportsController>("Prisoner Admissions", c => c.Prisoners(1))
            //    .AddChildRoute<ReportsController>("Convicted Prisoners", c => c.Convicted(1))
            //    .AddChildRoute<ReportsController>("Under Trial Prisoners", c => c.UnderTrial(1))
            //    .AddChildRoute<ReportsController>("Serious Offence", c => c.Serious(1))
            //    .AddChildRoute<ReportsController>("Non-Serious Offence", c => c.NonSerious(1));

            //routes.MapNavigationRoute("Setup", "Setup", "Account", null)
            //    .AddChildRoute<AccountController>("Users", c => c.Index())
            //    .AddChildRoute<ClassificationController>("Classifications", c => c.Index())
            //    .AddChildRoute<CourtController>("Courts", c => c.Index())
            //    .AddChildRoute<PrisonController>("Prison", c => c.Index());
        }
    }
}