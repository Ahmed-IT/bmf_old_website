using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _Standards.Library.Helpers;

namespace _Standards.Library.AdminLTETheme
{
    public class _ThemeController : Controller
    {
        //private DataFunctions _DataFunctions;

        //public ActionResult _M(string ID)
        //{
        //    List<_SystemFeature> TheFeatures = new List<_SystemFeature>();
        //    try
        //    {
        //        _DataFunctions = new DataFunctions();
        //        ID = _DataFunctions.Get_URLClean(ID);
        //        TheFeatures = _DataFunctions.GetAll_SystemFeatures_SidebarMenu(ID);
        //    }
        //    catch (Exception LogError)
        //    {
        //        TheFeatures = null;
        //    }
        //    return PartialView(FixedTerms.System_PartialView_LeftSideBar_Menu_Index, TheFeatures);
        //}

        public ActionResult _S(string ID)
        {
            return PartialView(FixedTerms.System_PartialView_LeftSideBar_Profile);
        }

        //public ActionResult _B(string ID)
        //{
        //    _SystemFeature Path = null;
        //    try
        //    {
        //        _DataFunctions = new DataFunctions();
        //        Path = _DataFunctions.Get_SystemFeaturePath(ID);
        //    }
        //    catch (Exception LogError)
        //    {

        //    }
        //    return PartialView(FixedTerms.System_PartialView_Breadcrumb, Path);
        //}
    }
}