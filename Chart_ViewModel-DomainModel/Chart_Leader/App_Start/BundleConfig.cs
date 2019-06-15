using System.Web;
using System.Web.Optimization;

namespace Chart_Leader
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/DataTables/jquery-ui-1.12.1.min.js",
                        "~/Scripts/DataTables/jquery.dataTables.min.js",
                        "~/Scripts/notify.min.js",
                        "~/Scripts/Script_Site.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"


                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/themes/base/jquery-ui.min.css",
                      "~/Content/DataTables/css/jquery.dataTables.min.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.min.css",
                      "~/Content/animate.css",
                      "~/Content/hover.css",
                      "~/Content/site.css"));
        }
    }
}
