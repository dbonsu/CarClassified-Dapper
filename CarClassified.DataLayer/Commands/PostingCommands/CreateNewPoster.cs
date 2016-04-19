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
            var sql = @"INSERT INTO Poster(Email,Phone,FirstName,LastName,IsVerified,StatedId)
                    VALUES(@Email,@Phone,@FirstName,@LastName,@IsVerified,@StatedId)";
            unit.Execute(sql, _poster);
        }
    }
}
