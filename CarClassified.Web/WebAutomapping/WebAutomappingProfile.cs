using AutoMapper;
using CarClassified.Models.Tables;
using CarClassified.Models.Views;
using CarClassified.Web.ViewModels;
using System.Collections.Generic;

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

            CreateMap<PostingDetailsVM, Poster>();

            CreateMap<PostingDetailsVM, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PosterId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Details));

            CreateMap<PostingDetailsVM, Vehicle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Listings, ListingVM>()
                .ForMember(dest => dest.PostDate, opt => opt.MapFrom(src => src.PostDate.ToString("d")));

            CreateMap<ListingDetail, ListingDetailsVM>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => RemoveDot(src.Images)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.PostDate, opt => opt.MapFrom(src => src.PostDate.ToString("d")));
        }

        private ICollection<Image> RemoveDot(ICollection<Image> images)
        {
            ICollection<Image> temp = new List<Image>();
            foreach (Image i in images)
            {
                var ex = i.Extension.Split('.')[1].Trim();
                Image t = new Image
                {
                    Body = i.Body,
                    Extension = ex
                };
                temp.Add(t);
            }

            return temp;
        }
    }
}
