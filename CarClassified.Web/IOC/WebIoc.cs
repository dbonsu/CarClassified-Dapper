using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using CarClassified.Common.IOCModule;
using CarClassified.DataLayer.DataAutomapping;
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
    public class WebIoc : CustomModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>

            {
                cfg.AddProfile(new WebAutomappingProfile());
                cfg.AddProfile(new DataAutomappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            builder.RegisterInstance(mapper).As<IMapper>();
            builder.RegisterType<VeryBasicEmail>().As<IVeryBasicEmail>();
        }
    }
}
