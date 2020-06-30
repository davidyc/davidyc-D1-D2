using Autofac;
using Autofac.Integration.Mvc;
using MvcMusicStore.App_Start;
using MvcMusicStore.Controllers;
using NLog;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {  
            DependencyResolver.SetResolver(DependencyResolverMusicStore.GetConfiguredDependencyResolver());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            var logger = DependencyResolver.Current.GetService(typeof(ILogger)) as ILogger;
            logger?.Error(exception);
        }
    }
}
