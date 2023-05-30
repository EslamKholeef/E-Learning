using System.Web;
using System.Web.Optimization;

namespace DistanceLearning
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            


            bundles.Add(new ScriptBundle("~/Scripts/MainScripts").Include(
                      "~/Content/src/js/jquery-3.5.1.min.js",
                      "~/Content/src/js/bootstrap.min.js",
                      "~/Content/src/js/all.min.js",
                      "~/Content/src/js/wow.min.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
                      "~/Content/src/js/jquery.dataTables.min.js",

                      "~/Content/src/js/master.js"
                      ));



            bundles.Add(new StyleBundle("~/Content/MainStyles").Include(
                      "~/Content/src/css/bootstrap.min.css",
                      "~/Content/src/css/all.min.css",
                      "~/Content/src/css/animate.min.css",
                      "~/Content/src/css/hover-min.css",
                      "~/Content/src/css/Home-Page-Style.css",
                      "~/Content/src/css/jquery.dataTables.min.css",
                      "~/Content/site.css"
                      ));



            bundles.Add(new ScriptBundle("~/Scripts/SliderScripts").Include(
                    
                "~/Content/ResponsiveSlider/js/lightslider.js",
                    "~/Content/ResponsiveSlider/js/script.js"
          
          ));



            bundles.Add(new StyleBundle("~/Content/SliderStyles").Include(
                      "~/Content/ResponsiveSlider/css/style.css",
                "~/Content/ResponsiveSlider/css/lightslider.css"
                      

                      ));


        }
    }
}
