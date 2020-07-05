using Autofac;
using Autofac.Integration.Mvc;
using MvcMusicStore.App_Start;
using MvcMusicStore.Controllers;
using MusicStoreLogger;
using NLog;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PerformanceCounterHelper;
using MvcMusicStore.PerformanceCounters;
using System.Diagnostics;
using System.Configuration;

namespace MvcMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            if(Boolean.Parse(ConfigurationManager.AppSettings["LoggerUse"]))
                DependencyResolver.SetResolver(DependencyResolverMusicStore.GetConfiguredDependencyResolver());
            ControllerCounter.InitCounter();      


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           

        }

        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            var logger = DependencyResolver.Current.GetService(typeof(MusicStoreLogger.ILogger)) as MusicStoreLogger.ILogger;
            logger?.Error(exception.ToString());
        }
    }
}
