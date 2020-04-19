using System.Web.Optimization;

namespace BootstrapSupport
{
    public class BootstrapBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-migrate-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/jquery.validate.js",
                "~/scripts/jquery.validate.unobtrusive.js",
                "~/scripts/jquery.table2excel.js",
                "~/Scripts/jquery.validate.unobtrusive-custom-for-bootstrap.js,"
                ));

            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-datepicker.css",
                "~/Content/body.css",
                "~/Content/bootstrap-responsive.css",
                "~/Content/bootstrap-mvc-validation.css",
                "~/Content/custom.css",
                "~/Content/font.css"
                ));

            bundles.Add(new ScriptBundle("~/js/duallistbox").Include("~/Scripts/jquery.bootstrap-duallistbox.js"));
            bundles.Add(new StyleBundle("~/content/duallistbox").Include("~/Content/bootstrap-duallistbox.css"));

            bundles.Add(new ScriptBundle("~/js/scriptcam").Include("~/Scripts/scriptcam.js", "~/Scripts/swfobject.js"));

            bundles.Add(new ScriptBundle("~/bundles/typeahead").Include("~/Scripts/typeahead*"));
            bundles.Add(new StyleBundle("~/content/typeahead").Include("~/Content/typeahead.js-bootstrap.css"));
        }
    }
}