using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Queries.ListingQueries
{
    public class GetListings : IQuery<ICollection<Listings>>
    {
        private int _stateId;

        public GetListings()
        {
        }

        public GetListings(int stateId)
        {
            _stateId = stateId;
        }

        public ICollection<Listings> Execute(IUnitOfWork unit)
        {
            string sql = @"SELECT TOP 10 v.Year,p.Id,p.Title, p.Location, p.PostDate, m.Name AS Make, mo.Name AS Model FROM Post p
                            JOIN Vehicle v ON p.Id = v.PostId
                            JOIN Make m ON m.Id = v.MakeId
                            JOIN Model mo ON mo.Id  = v.ModelId
                            JOIN Poster po ON po.Id = p.PosterId
                            WHERE p.IsActive =1";

            string queryWithState = @"select v.Year, p.Id,p.Title, p.Location, p.PostDate, m.Name as Make, mo.Name as Model, s.Name from Post p
                                    Join Vehicle v on p.Id = v.PostId
                                    join Make m on m.Id = v.MakeId
                                    join Model mo on mo.Id  = v.ModelId
                                    join Poster po on po.Id = p.PosterId
                                    join dbo.State s on s.Id =po.StateId
                                    where p.IsActive =1 and s.Id =@sId";

            return _stateId > 0 ? unit.Query<Listings>(queryWithState, new { sId = _stateId }).ToList() : unit.Query<Listings>(sql).ToList();
            //if (_stateId > 0)
            //{
            //    return unit.Query<Listings>(queryWithState, new { sId = _stateId }).ToList();
            //}
            //return unit.Query<Listings>(sql).ToList();
        }
    }
}
