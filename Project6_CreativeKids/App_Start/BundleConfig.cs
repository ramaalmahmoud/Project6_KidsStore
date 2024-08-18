using System.Web;
using System.Web.Optimization;
using System.Web.UI.WebControls;

namespace Project6_CreativeKids
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

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/assets/css").Include(
                      "~/Content/assets/css/animate.css",
                      "~/Content/assets/css/aosaos.css",
                      "~/Content/assets/css/bootstrap.min.css",
                      "~/Content/assets/css/font-awesome.min.css",
                      "~/Content/assets/css/ionicons.css",
                      "~/Content/assets/css/jquery.fancybox.min.css",
                      "~/Content/assets/css/jpe-icon-7-stroke.min.css",
                      "~/Content/assets/css/rangeslider.css",
                      "~/Content/assets/css/slick.css",
                      "~/Content/assets/css/slicknav.css",
                      "~/Content/assets/css/style.css",
            "~/Content/assets/css/swiper.min.css"));
            bundles.Add(new StyleBundle("~/Content/assets/js").Include(
                      "~/Content/assets/js/aos.min.js",
                      "~/Content/assets/js/bootstrap.min.js",
                      "~/Content/assets/js/custom.js",
                      "~/Content/assets/js/fancybox.min.js",
                      "~/Content/assets/js/isotope.pkgd.min.js",
                      "~/Content/assets/js/jquery.appear.js",
                      "~/Content/assets/js/jquery.countdown.min.js",
                      "~/Content/assets/js/jquery.slicknav.js",
                      "~/Content/assets/js/jquery-main.js",
                      "~/Content/assets/js/jquery-migrate.js",
                      "~/Content/assets/js/jquery-zoom.min.js",
            "~/Content/assets/js/modernizr.js",
             "~/Content/assets/js/parallax.min.js",
              "~/Content/assets/js/popper.min.js",
               "~/Content/assets/js/rangeSlider.js", "~/Content/assets/js/slick.min.js",
                "~/Content/assets/js/swiper.min.js",
                 "~/Content/assets/js/tippy.all.min.js",
                  "~/Content/assets/js/wow.min.js"

              ));


        }
    }
}
