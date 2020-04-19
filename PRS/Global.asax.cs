using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using BootstrapSupport;
using System.Data.Entity;
using System.Web.Optimization;
using PRS.Models;

namespace PRS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BootstrapBundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = false;

            Database.SetInitializer<PRSContext>(new PRSInitializer());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;

            if (httpException != null)
            {
                string action = "Index";

                //switch (httpException.GetHttpCode())
                //{
                //    case 404:
                //        // page not found
                //        action = "HttpError404";
                //        break;
                //    case 500:
                //        // server error
                //        action = "HttpError500";
                //        break;
                //    default:
                //        action = "General";
                //        break;
                //}

                // clear error on server
                Server.ClearError();

                Response.Redirect(String.Format("~/Error/{0}?message={1}", action, exception.Message));
            }
        }
    }
}