using AutoMapper;
using CarClassified.DataLayer.Interfaces;
using CarClassified.DataLayer.Queries.AssetsQueries;
using CarClassified.Models.Tables;
using CarClassified.Web.Utilities.Interfaces;
using CarClassified.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarClassified.Web.Utilities
{
    public class Assest : IAssest
    {
        private readonly IDatabase _db;
        private readonly IMapper _mapper;

        public Assest(IDatabase db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IEnumerable<SelectListItem> GetStates()
        {
            {
                ICollection<State> statesdb = _db.Query(new GetAllStates());
                ICollection<StateVM> states = _mapper.Map<ICollection<StateVM>>(statesdb);

                var result = states.Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });

                return new SelectList(result, "Value", "Text");
            }
        }
    }
}
