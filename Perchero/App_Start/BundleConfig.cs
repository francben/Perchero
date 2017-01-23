using System.Web;
using System.Web.Optimization;

namespace Perchero
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/lib/jquery/jquery.js",
                      "~/lib/jquery-ui/jquery-ui.js",
                      "~/lib/bootstrap/js/bootstrap.js",
                      "~/lib/jquery-toggles/toggles.js",
                      //"~/lib/morrisjs/morris.js",
                      //"~/lib/raphael/raphael.js",
                      //"~/lib/flot/jquery.flot.js",
                      //"~/lib/flot/jquery.flot.resize.js",
                      //"~/lib/flot-spline/jquery.flot.spline.js",
                      //"~/lib/jquery-knob/jquery.knob.js",
                      "~/Scripts/bootstrap.file-input.js",
                      "~/Scripts/fileinput.min.js",
                      "~/Scripts/jquery.fileupload.js",
                      "~/Scripts/jquery.magnific-popup.min.js",
                      "~/Scripts/quirk.js"
                      //"~/Scripts/dashboard.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/lib/Hover/hover.css",
                      "~/lib/bootstrap/css/bootstrap.css",
                      "~/lib/fontawesome/css/font-awesome.css",
                      "~/lib/weather-icons/css/weather-icons.css",
                      "~/lib/ionicons/css/ionicons.css",
                      "~/lib/jquery-toggles/toggles-full.css",
                      "~/Content/magnific-popup.css",
                      //"~/lib/morrisjs/morris.css",
                      "~/Content/quirk.css"));
        }
    }
}
