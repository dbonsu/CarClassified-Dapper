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
    public class GetPosterVerification : IQuery<VerificationDTO>
    {
        private string _email;

        public GetPosterVerification(string email)
        {
            _email = email;
        }

        public VerificationDTO Execute(IUnitOfWork unit)
        {
            return unit.Query<VerificationDTO>("SELECT Email,IsVerified FROM Poster WHERE Email=@email", new { email = _email }).FirstOrDefault();
        }
    }
}