using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Tables;
using System;
using System.Linq;

namespace CarClassified.DataLayer.Commands.PostingCommands
{
    /// <summary>
    /// Creates a new post -listing
    /// </summary>
    /// <seealso cref="CarClassified.DataLayer.Interfaces.ICommand" />
    public class CreateNewPost : ICommand
    {
        private Post _post;
        private Poster _poster;
        private Vehicle _vehicle;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewPost"/> class.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <param name="vehicle">The vehicle.</param>
        /// <param name="poster">The poster.</param>
        public CreateNewPost(Post post, Vehicle vehicle, Poster poster)
        {
            _post = post;
            _vehicle = vehicle;
            _poster = poster;
        }

        /// <summary>
        /// Executes the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        public void Execute(IUnitOfWork unit)
        {
            _poster.IsVerified = true;
            string updateUser = @"UPDATE Poster SET Phone=@Phone, FirstName=@FirstName,
                                LastName=@LastName, IsVerified=@IsVerified WHERE Id=@Id";

            unit.Execute(updateUser, _poster);

            _post.IsActive = true;
            _post.PostDate = DateTime.Now;

            string createPost = @"INSERT INTO Post(Title,Body,IsActive, Location,PosterId,PostDate,Price)
                                Values(@Title,@Body,@IsActive,@Location,@PosterId,@PostDate,@Price);
                                SELECT CAST(SCOPE_IDENTITY() as int)";
            int postId = unit.Query<int>(createPost, _post).Single();

            _vehicle.PostId = postId;

            string createVehicle = @"INSERT INTO Vehicle(Color,Transmission,BodyStyle,Condition,Fuel,Make,Model,Cylinder,PostId,Year,Miles)
                            Values(@Color,@Transmission,@BodyStyle,@Condition,@Fuel,@Make,@Model,@Cylinder,@PostId,@Year,@Miles)";

            unit.Execute(createVehicle, _vehicle);
        }
    }
}
