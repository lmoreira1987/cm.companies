using System.Web;
using System.Web.Optimization;

namespace GServiceManagerMVC
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/_JQuery/jquery-{version}.js"
                        , "~/Scripts/_JQuery/jquery-1.11.2.intellisense.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/_Bootstrap/bootstrap.js"
                , "~/Scripts/_Bootstrap/bootbox.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/_Layout/site.css"
                , "~/Content/_Bootstrap/bootstrap.css"
                , "~/Content/_Bootstrap/simple-sidebar.css"
                , "~/Content/_JQuery/style.css"));

            bundles.Add(new StyleBundle("~/css/Login").Include(
                "~/Content/_Bootstrap/bootstrap.css"
                , "~/Content/Login/style.css"
                , "~/Content/Login/style-responsive.css"
                , "~/Content/Login/style-default.css"
                , "~/Content/Login/Login.css"));
        }
    }
}