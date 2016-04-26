using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.DataAutomapping
{
    /// <summary>
    /// Automapping in common data layer
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class DataAutomappingProfile : Profile
    {
        protected override void Configure()
        {
            // sample CreateMap<PosterVM, Poster>();
        }
    }
}
