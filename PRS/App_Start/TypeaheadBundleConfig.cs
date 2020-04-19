using System.Web.Optimization;

//[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(PRS.App_Start.TypeaheadBundleConfig), "RegisterBundles")]

namespace PRS.App_Start
{
	public class TypeaheadBundleConfig
	{
		public static void RegisterBundles()
		{
			// Add @Scripts.Render("~/bundles/typeahead") after jQuery in your _Layout.cshtml view
			// When <compilation debug="true" />, MVC4 will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
			//BundleTable.Bundles.Add(new ScriptBundle("~/bundles/typeahead").Include("~/Scripts/typeahead*"));
		}
	}
}
