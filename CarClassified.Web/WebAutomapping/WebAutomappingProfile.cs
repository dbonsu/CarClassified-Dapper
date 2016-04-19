using AutoMapper;
using CarClassified.Models.Tables;
using CarClassified.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarClassified.Web.WebAutomapping
{
    public class WebAutomappingProfile : Profile
    {
        protected override void Configure()
        {
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<PosterVM, Poster>();
            //    cfg.CreateMap<Poster, PosterVM>();
            //    cfg.CreateMap<State, StateVM>();
            //});
            //config.CreateMapper();
            CreateMap<PosterVM, Poster>();
            CreateMap<Poster, PosterVM>();
            CreateMap<State, StateVM>();
        }
    }
}
