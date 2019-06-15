using API_Project.Repository;
using Chart_Leader.Infrastrucutre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Chart_Leader
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutomapperWebProfile.Run();
            UnityConfig.RegisterComponents();
            //HtmlHelper.ClientValidationEnabled = true; HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
            //   BundleTable.EnableOptimizations = true;
        }
    }
}
