using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplicationThuPhi.App_Start;

namespace WebApplicationThuPhi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User;
            SimpleConfigurationPrincipal.SetAuthenticatedPrincipal(user);
        }
        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    if (HttpContext.Current.Request.IsSecureConnection.Equals(false) &&
        //        HttpContext.Current.Request.IsLocal.Equals(false))
        //    {
        //        Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"] +
        //                          HttpContext.Current.Request.RawUrl);
        //    }
        //}
    }
}
