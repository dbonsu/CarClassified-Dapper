using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using CarClassified.Common.IOCModule;
using CarClassified.Web.Utilities;
using CarClassified.Web.Utilities.Interfaces;
using CarClassified.Web.WebAutomapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;

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
            //Register controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly())
               .InstancePerRequest();

            //Components

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        public class WebCustomIoc : CustomModule
        {
            protected override void Load(ContainerBuilder builder)
            {
                var mapper = WebMapper.GetWebLayerMappings().CreateMapper();
                builder.RegisterType<VeryBasicEmail>().As<IVeryBasicEmail>();
                builder.RegisterInstance(mapper).As<IMapper>();
            }
        }
    }
}