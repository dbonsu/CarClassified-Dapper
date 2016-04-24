using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Commands.PostingCommands
{
    public class CreateNewPost : ICommand
    {
        private Post _post;
        private Poster _poster;
        private Vehicle _vehicle;

        public CreateNewPost(Post post, Vehicle vehicle, Poster poster)
        {
            _post = post;
            _vehicle = vehicle;
            _poster = poster;
        }

        public void Execute(IUnitOfWork unit)
        {
            _poster.IsVerified = true;
            string updateUser = @"UPDATE Poster SET Phone=@Phone, FirstName=@FirstName,
                                LastName=@LastName, IsVerified=@IsVerified WHERE Id=@Id";

            unit.Execute(updateUser, _poster);

            _post.IsActive = true;
            _post.PostDate = DateTime.Now;
            //_post.StateId = _poster.StateId;
            string createPost = @"INSERT INTO Post(Title,Body,IsActive, Location,PosterId,StateId,PostDate)
                                Values(@Title,@Body,@IsActive,@Location,@PosterId,@StateId,@PostDate);
                                SELECT CAST(SCOPE_IDENTITY() as int)";
            int postId = unit.Query<int>(createPost, _post).Single();

            //if select select fails
            _vehicle.PostId = postId;

            string createVehicle = @"INSERT INTO Vehicle(ColorId,TransmissionId,BodyId,ConditionId,FuelId,MakeId,ModelId,CylinderId,PostId,Year)
                            Values(@ColorId,@TransmissionId,@BodyId,@ConditionId,@FuelId,@MakeId,@ModelId,@CylinderId,@PostId,@Year)";

            unit.Execute(createVehicle, _vehicle);
            //TODO:Images
        }
    }
}