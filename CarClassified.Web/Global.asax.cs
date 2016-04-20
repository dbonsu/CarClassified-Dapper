using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using CarClassified.Common.IOCModule;
using CarClassified.Web.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace CarClassified.Web
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutofacConfiguration();
            // AutoMapperConfiguration();
        }

        private void AutofacConfiguration()
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

        private void AutoMapperConfiguration()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>

            {
                cfg.AddProfile(new WebAutomapping.WebAutomappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            ContainerBuilder b = new ContainerBuilder();
            b.RegisterInstance(mapper).As<IMapper>();
        }
    }
}
