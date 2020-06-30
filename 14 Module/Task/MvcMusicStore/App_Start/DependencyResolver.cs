using Autofac;
using Autofac.Integration.Mvc;
using MusicStoreLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.App_Start
{
    public class DependencyResolverMusicStore
    {
        public static IDependencyResolver GetConfiguredDependencyResolver()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            ConfigureBindings(builder);
            var container = builder.Build();
            return new AutofacDependencyResolver(container);
        }

        private static void ConfigureBindings(ContainerBuilder builder)
        {
            builder.RegisterType<Logger>().As<ILogger>();
        }
    }
}