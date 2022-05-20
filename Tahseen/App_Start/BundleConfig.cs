using System.Web;
using System.Web.Optimization;

namespace Tahseen
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Content/bootstrap/js/bootstrap.min.js",
            //          "~/Content/bootstrap/js/bootstrap.bundle.min.js",
            //          "~/Content/bootstrap/js/bootstrap.esm.min.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap/css/bootstrap.rtl.min.css",
            //          "~/Content/bootstrap/css/bootstrap-utilities.rtl.min.css",
            //          "~/Content/bootstrap/css/bootstrap-reboot.rtl.min.css",
            //          "~/Content/bootstrap/css/bootstrap-grid.rtl.min.css",
            //          "~/Content/site.css"));
        }
    }
}
