using System.Web;
using System.Web.Optimization;

namespace BMF.Web.Admin
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Page with DataTable
            bundles.Add(new StyleBundle("~/bundles/DataTables_Styles").Include(
                "~/Content/_plugins/DataTables/dataTables.bootstrap.css", // Default DataTable Style
                "~/Content/_plugins/DataTables/Extensions/Filter/jquery.dataTables.yadcf.css", // Filter Style
                "~/Content/_plugins/DataTables/Extensions/Buttons/buttons.dataTables.min.css", // Buttons Style
                "~/Content/_plugins/DataTables/Extensions/Selection/select.dataTables.css" // Selection Style
                ));

            bundles.Add(new ScriptBundle("~/bundles/DataTables_Scripts").Include(
                "~/Scripts/_plugins/DataTables/jquery.dataTables.js", // Default DataTable Script
                "~/Scripts/_plugins/DataTables/dataTables.bootstrap.js", // DataTable BootStrap Script
                "~/Scripts/_plugins/DataTables/Extensions/Buttons/buttons.colVis.js", // Column Filters
                "~/Scripts/_plugins/DataTables/Extensions/Buttons/dataTables.buttons.js", // Buttons
                "~/Scripts/_plugins/DataTables/Extensions/Selection/dataTables.select.js", // Select
                "~/Scripts/_plugins/DataTables/Extensions/Filter/jquery.dataTables.yadcf.js", // Filters
                                                                                              // Export
                "~/Scripts/_plugins/DataTables/Extensions/Buttons/jszip.js",
                "~/Scripts/_plugins/DataTables/Extensions/Buttons/pdfmake.js",
                "~/Scripts/_plugins/DataTables/Extensions/Buttons/vfs_fonts.js",
                "~/Scripts/_plugins/DataTables/Extensions/Buttons/buttons.html5.js",
                "~/Scripts/_plugins/DataTables/Extensions/Buttons/buttons.print.js",
                "~/Scripts/_plugins/DataTables/dataTables.helpers.js" // Custom Initializer
                ));
            #endregion

            #region Page with iCheck
            bundles.Add(new StyleBundle("~/bundles/iCheck_css").Include(
                "~/Content/_plugins/iCheck/square/blue.css" // iCheck
                ));

            bundles.Add(new ScriptBundle("~/bundles/iCheck_js").Include(
                "~/Scripts/_plugins/iCheck/icheck.js" // iCheck
                ));
            #endregion

            #region Page with Date Range Picker
            bundles.Add(new StyleBundle("~/bundles/DateRangePicker_css").Include(
                "~/Content/_plugins/DateRangePicker/daterangepicker.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/DateRangePicker_js").Include(
                "~/Scripts/_plugins/DateRangePicker/daterangepicker.js"
                ));
            #endregion

            #region Page with Selectize
            bundles.Add(new StyleBundle("~/bundles/Selectize_css").Include(
                "~/Content/_plugins/Selectize/selectize.css",
                "~/Content/_plugins/Selectize/selectize.bootstrap3.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Selectize_js").Include(
                "~/Scripts/_plugins/Selectize/selectize.js"
                ));
            #endregion
        }
    }
}
