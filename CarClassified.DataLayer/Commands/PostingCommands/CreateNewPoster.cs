using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Tables;
using System;

namespace CarClassified.DataLayer.Commands.PostingCommands
{
    /// <summary>
    /// Creates a new poster(user) object
    /// </summary>
    /// <seealso cref="CarClassified.DataLayer.Interfaces.ICommand" />
    public class CreateNewPoster : ICommand
    {
        private Poster _poster;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewPoster"/> class.
        /// </summary>
        /// <param name="poster">The poster.</param>
        public CreateNewPoster(Poster poster)
        {
            _poster = poster;
        }

        /// <summary>
        /// Executes the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        public void Execute(IUnitOfWork unit)
        {
            _poster.Id = Guid.NewGuid();
            var sql = @"INSERT INTO Poster(Id,Email,Phone,FirstName,LastName,IsVerified,StateId)
                    VALUES(@Id,@Email,@Phone,@FirstName,@LastName,@IsVerified,@StateId)";
            unit.Execute(sql, _poster);
        }
    }
}
