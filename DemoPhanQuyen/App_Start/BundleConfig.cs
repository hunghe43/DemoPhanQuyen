using System.Web;
using System.Web.Optimization;

namespace DemoPhanQuyen
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Styles/Admin").Include(
                      "~/Assets/Admin/css/bootstrap.min.css",
                      "~/Assets/Admin/css/sb-admin.css",
                      "~/Assets/Admin/css/plugins/morris.css",
                      "~/Assets/Admin/font-awesome/css/font-awesome.min.css"));
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/Scripts/Admin").Include(
                      "~/Assets/Admin/js/jquery.js",
                      "~/Assets/Admin/js/bootstrap.min.js",
                      "~/Assets/Admin/js/plugins/morris/raphael.min.js",
                      "~/Assets/Admin/js/plugins/morris/morris.min.js",
                      "~/Assets/Admin/js/plugins/morris/morris-data.js"));
        }
    }
}
