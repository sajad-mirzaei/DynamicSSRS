using System.Web.Optimization;

namespace SampleWebForm
{
    public class BundleConfig
    {
        // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Assets/Scripts/WebForms/WebForms.js",
                            "~/Assets/Scripts/WebForms/WebUIValidation.js",
                            "~/Assets/Scripts/WebForms/MenuStandards.js",
                            "~/Assets/Scripts/WebForms/Focus.js",
                            "~/Assets/Scripts/WebForms/GridView.js",
                            "~/Assets/Scripts/WebForms/DetailsView.js",
                            "~/Assets/Scripts/WebForms/TreeView.js",
                            "~/Assets/Scripts/WebForms/WebParts.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Assets/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Assets/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Assets/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Assets/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Assets/Scripts/modernizr-*"));
        }
    }
}