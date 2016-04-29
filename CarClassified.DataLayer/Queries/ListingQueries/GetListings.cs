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
            string sqlTop20 = @"SELECT TOP 20 p.Id, p.PostDate, p.Title, p.Price,
                                v.Year, v.Make, V.Model, v.Miles from Post p
                                JOIN Vehicle v ON v.PostId = p.Id
                                WHERE p.IsActive =1";

            string queryWithState = @"SELECT p.Id, p.PostDate, p.Title, p.Price,
                                    v.Year, v.Make, V.Model, v.Miles from Post p
                                    JOIN Vehicle v ON v.PostId = p.Id
                                    JOIN Poster po ON po.Id = p.PosterId
                                    JOIN State s ON s.Id = po.StateId
                                    WHERE p.IsActive =1 AND s.Id =@sId ";

            return _stateId > 0 ? unit.Query<Listings>(queryWithState, new { sId = _stateId }).ToList() : unit.Query<Listings>(sqlTop20).ToList();
        }
    }
}
