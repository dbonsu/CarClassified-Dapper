using AutoMapper;
using CarClassified.Models.Tables;
using CarClassified.Models.Views;
using CarClassified.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarClassified.Web.WebAutomapping
{
    /// <summary>
    /// Automapping for web layer
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class WebAutomappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<PosterVM, Poster>();

            CreateMap<Poster, PosterVM>()
                .ForMember(dest => dest.UserStates, opt => opt.Ignore());
            CreateMap<State, StateVM>();

            CreateMap<PostDetailsVM, Poster>();

            CreateMap<PostDetailsVM, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PosterId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Details));

            CreateMap<PostDetailsVM, Vehicle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.BodyId, opt => opt.MapFrom(src => src.BodyStyleId));

            CreateMap<Listings, ListingVM>()
                .ForMember(dest => dest.PostDate, opt => opt.MapFrom(src => src.PostDate.ToString("d")));
        }
    }
}
