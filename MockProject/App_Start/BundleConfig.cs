using System.Web;
using System.Web.Optimization;

namespace MockProject
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {



            // Vendor scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-2.1.1.min.js"));

            // jQuery Validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            "~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.unobtrusive-ajax.js", "~/Scripts/jquery.validate.unobtrusive.js", "~/Scripts/Fix.js", "~/Scripts/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js", "~/Scripts/moment-with-locales.js", "~/Scripts/bootstrap-datetimepicker.js", "~/Scripts/autoNumeric-min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/Scripts/app/inspinia.js"));

            // Inspinia skin config script
            bundles.Add(new ScriptBundle("~/bundles/skinConfig").Include(
                      "~/Scripts/app/skin.config.min.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimScroll/jquery.slimscroll.min.js"));

            // jQuery plugins
            bundles.Add(new ScriptBundle("~/plugins/metsiMenu").Include(
                      "~/Scripts/plugins/metisMenu/metisMenu.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/pace").Include(
                      "~/Scripts/plugins/pace/pace.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/sweetalert").Include(
                      "~/Scripts/plugins/sweetalert/sweetalert.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/select2").Include(
                      "~/Scripts/plugins/select2/select2.full.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/chosen").Include(
                      "~/Scripts/plugins/chosen/chosen.jquery.js"));

            // tags input
            bundles.Add(new ScriptBundle("~/plugins/boostrap-tagsinput").Include(
                      "~/Scripts/plugins/boostrap-tagsinput/boostrap-tagsinput.js"));
            // ndv3
            bundles.Add(new ScriptBundle("~/plugins/nvd3").Include(
                      "~/Scripts/plugins/nvd3/nv.d3.js"));
            //Flot chart
            bundles.Add(new ScriptBundle("~/plugins/flot").Include(
                      "~/Scripts/plugins/flot/jquery.flot.js",
                      "~/Scripts/plugins/flot/jquery.flot.tooltip.min.js",
                      "~/Scripts/plugins/flot/jquery.flot.resize.js",
                      "~/Scripts/plugins/flot/jquery.flot.pie.js",
                      "~/Scripts/plugins/flot/jquery.flot.time.js",
                      "~/Scripts/plugins/flot/jquery.flot.spline.js"));
            // vectorMap 
            bundles.Add(new ScriptBundle("~/plugins/vectorMap").Include(
                      "~/Scripts/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      "~/Scripts/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"));

            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      "~/Content/animate.css",
                      /*"~/Content/style.css",*/ "~/Content/bootstrap-datetimepicker.css", "~/Content/fix.css",
                      "~/Content/plugin/sweetalert/sweetalert.css", "~/Content/plugin/select2/select2.min.css",
                      "~/Content/plugin/chosen/chosen.css", "~/Content/Plugin/boostrap-tagsinput/bootstrap-tagsinput.css", "~/Content/nv.d3.css"));

            // c3 Styless
            bundles.Add(new StyleBundle("~/plugins/c3Styles").Include(
                      "~/Content/plugins/c3/c3.min.css"));
            // c3 Spin
            bundles.Add(new ScriptBundle("~/plugins/c3").Include(
                      "~/Scripts/plugins/c3/c3.min.js"));

            // d3 Spin
            bundles.Add(new ScriptBundle("~/plugins/d3").Include(
                      "~/Scripts/plugins/d3/d3.min.js"));
            // e3 Spin
            bundles.Add(new ScriptBundle("~/plugins/e3").Include(
                      "~/Scripts/plugins/e3/highcharts.js"));
            // f3 Spin
            bundles.Add(new ScriptBundle("~/plugins/f3").Include(
                      "~/Scripts/plugins/f3/exporting.js"));



            // c3 Styless
            bundles.Add(new StyleBundle("~/plugins/c3Styles").Include(
                      "~/Content/plugins/c3/c3.min.css"));
            // c3 Spin
            bundles.Add(new ScriptBundle("~/plugins/c3").Include(
                      "~/Scripts/plugins/c3/c3.min.js"));

            // d3 Spin
            bundles.Add(new ScriptBundle("~/plugins/d3").Include(
                      "~/Scripts/plugins/d3/d3.min.js"));
            // e3 Spin
            bundles.Add(new ScriptBundle("~/plugins/e3").Include(
                      "~/Scripts/plugins/e3/highcharts.js"));
            // f3 Spin
            bundles.Add(new ScriptBundle("~/plugins/f3").Include(
                      "~/Scripts/plugins/f3/exporting.js"));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

        }
    }
}
