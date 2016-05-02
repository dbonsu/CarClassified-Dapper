using Autofac;
using AutoMapper;
using CarClassified.Common.IOCModule;
using CarClassified.DataLayer.DataAutomapping;
using CarClassified.Web.Utilities;
using CarClassified.Web.Utilities.Interfaces;
using CarClassified.Web.WebAutomapping;

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
            builder.RegisterType<Assest>().As<IAssest>();
        }
    }
}
