﻿using Autofac;
using Autofac.Integration.Mvc;
using MusicStoreLogger;
using MvcMusicStore.PerformanceCounters;
using PerformanceCounterHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.App_Start
{
    public class DependencyResolverMusicStore
    {
        public static IDependencyResolver GetConfiguredDependencyResolver(bool needLog)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            ConfigureBindings(builder, needLog);
            var container = builder.Build();   
            return new AutofacDependencyResolver(container);
        }

        private static void ConfigureBindings(ContainerBuilder builder, bool needLog)
        {
            builder.RegisterType<Logger>().WithParameter("logON", needLog).As<ILogger>();            
        }
    }
}