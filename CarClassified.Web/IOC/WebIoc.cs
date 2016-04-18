using Autofac;
using Autofac.Integration.WebApi;
using CarClassified.Common.IOCModule;
using CarClassified.Web.Utilities;
using CarClassified.Web.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;

namespace CarClassified.Web.IOC
{
    public static class WebIoc
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            var executingAssembly = Assembly.GetExecutingAssembly();

            var domainAssemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
            //Assembly Modules

            builder.RegisterAssemblyModules<CustomModule>(domainAssemblies.ToArray());

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Components

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public class WebCustomIoc : CustomModule
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<VeryBasicEmail>().As<IVeryBasicEmail>();
            }
        }
    }
}