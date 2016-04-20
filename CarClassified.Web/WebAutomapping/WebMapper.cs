using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarClassified.Web.WebAutomapping
{
    public class WebMapper
    {
        public static MapperConfiguration GetWebLayerMappings()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>

            {
                cfg.AddProfile(new WebAutomapping.WebAutomappingProfile());
            });
            return mapperConfig;
        }
    }
}