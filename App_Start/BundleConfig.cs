using System.Web;
using System.Web.Optimization;

namespace TEST
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                //"~/Scripts/app.js",
                        "~/Scripts/app.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/bootstrap-select.js",
                       "~/Scripts/jquery-2.1.4.min.js",
                       "~/Scripts/bootstrap-toggle.min.js",
                       "~/Scripts/bootstrap-datepicker.js",
                       "~/Scripts/jquery-te-1.4.0.min.js",
                       "~/Scripts/jquery.dataTables.min.js",
                       "~/Select2/select2.js",
                        "~/Scripts/JQueryUI/jquery-ui.js"

                      ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/jquery-2.1.4.min.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/admin_style.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/bootstrap-select.css",
                      "~/Content/bootstrap-toggle.min.css",
                      "~/Content/datepicker.css",
                      "~/Content/jquery-te-1.4.0.css",
                      "~/Select2/select2.css",
                      "~/Content/jquery.dataTables.min.css",
                      "~/Content/style.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-theme.min.css",
                       "~/Content/jquery-ui.css"
                //"~/Content/bootstrap-datetimepicker.min.css",
                      ));

            bundles.Add(new StyleBundle("~/bundles/MerchantOfferSearch").Include(
                //"~/Scripts/bootstrap-datepicker.js"
                     ));

            //bundles.Add(new ScriptBundle("~/bundles/MerchantOfferAdBook").Include(
            //         "~/Scripts/jquery.unobtrusive-ajax.min.js",

            //            "~/UIScript/MerchantOfferAdBook.js"
            //        ));

        }
    }
}
