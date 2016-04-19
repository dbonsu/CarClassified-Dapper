using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
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
                MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>

                {
                    cfg.AddProfile(new WebAutomapping.WebAutomappingProfile());
                });

                var mapper = mapperConfig.CreateMapper();
                ContainerBuilder b = new ContainerBuilder();

                builder.RegisterType<VeryBasicEmail>().As<IVeryBasicEmail>();
                b.RegisterInstance(mapper).As<IMapper>();
                // builder.Register<ICarClassifiedContext>(c =>
                //new CarClassifiedContext(BaseSettings.ConnectionString)
                // ).InstancePerDependency();
            }
        }
    }
}
