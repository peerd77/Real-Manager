using System.Web;
using System.Web.Optimization;

namespace Real_Manager
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

            bundles.Add(new StyleBundle("~/bundles/mini-spa/style").Include(
                      "~/Content/bootstrap.css" ,
                      "~/Content/bootstrap-toggle.css" ,
                      "~/app/content/styles.css"));

            bundles.Add(new ScriptBundle("~/bundles/mini-spa/script").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-toggle.js",
                "~/Scripts/angular.js",
                "~/Scripts/angular-route.js",
                "~/app/app.js",
                "~/app/controllers/loginController.js",
                "~/app/controllers/teamsController.js",
                "~/app/controllers/playersController.js",
                "~/app/controllers/editPlayerController.js",
                "~/app/controllers/editTeamController.js"));

        }
    }
}
