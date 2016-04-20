using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Commands.PostingCommands
{
    public class CreateNewPoster : ICommand
    {
        private Poster _poster;

        public CreateNewPoster(Poster poster)
        {
            _poster = poster;
        }

        public void Execute(IUnitOfWork unit)
        {
            _poster.Id = Guid.NewGuid();
            var sql = @"INSERT INTO Poster(Id,Email,Phone,FirstName,LastName,IsVerified,StateId)
                    VALUES(@Id,@Email,@Phone,@FirstName,@LastName,@IsVerified,@StateId)";
            unit.Execute(sql, _poster);
        }
    }
}