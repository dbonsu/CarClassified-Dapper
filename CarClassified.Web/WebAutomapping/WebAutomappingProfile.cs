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
            CreateMap<PosterVM, Poster>();

            CreateMap<Poster, PosterVM>()
                .ForMember(dest => dest.UserStates, opt => opt.Ignore());
            CreateMap<State, StateVM>();
        }
    }
}
