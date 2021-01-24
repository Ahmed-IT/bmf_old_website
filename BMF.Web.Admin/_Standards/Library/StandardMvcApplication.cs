using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;
using System.Globalization;
using System.Web.SessionState;
using System.Web.Optimization;
using System.Collections.Generic;
using BMF.Web.Admin;

namespace _Standards.Library
{
    public class StandardMvcApplication : System.Web.HttpApplication
    {
        #region MVC Application

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            string errorUrl = "/_Error/Index/";
            bool isAjaxCall = new HttpRequestWrapper(Context.Request).IsAjaxRequest();
            Exception exception = Server.GetLastError();
            if (isAjaxCall) errorUrl = "/_Error/IndexPV/";
            Response.Clear();
            if (exception is HttpException httpException)
            {
                Response.Clear();
                Response.Redirect($"{errorUrl}{httpException.GetHttpCode().ToString()}");
            }
            else
            {
                Response.Clear();
                Response.Redirect($"{errorUrl}505");
            }
        }

        #endregion

        #region WEB API

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiConfig.UrlPrefixRelative);
        }

        #endregion
    }
}