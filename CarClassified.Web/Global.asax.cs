using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using CarClassified.Web.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            WebIoc.Configure();
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
