using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.SimpleDTOs;
using CarClassified.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Queries.PostingQueries
{
    public class GetPoster : IQuery<Poster>
    {
        private string _email;

        public GetPoster(string email)
        {
            _email = email;
        }

        public Poster Execute(IUnitOfWork unit)
        {
            return unit.Query<Poster>("SELECT * FROM Poster WHERE Email=@email", new { email = _email }).FirstOrDefault();
        }
    }
}
