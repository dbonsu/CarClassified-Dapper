using CarClassified.DataLayer.Interfaces;
using CarClassified.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.DataLayer.Commands.PostingCommands
{
    public class AddImagesToPost : ICommand
    {
        private ICollection<Image> _images;
        private string _userName;

        public AddImagesToPost(ICollection<Image> images, string userName)
        {
            _images = images;
            _userName = userName;
        }

        public void Execute(IUnitOfWork unit)
        {
            string getPostId = @"SELECT p.Id FROM Post p JOIN Poster po ON po.Id = p.PosterId
                                WHERE po.Email=@email AND p.IsActive =1";
            string saveImage = @"INSERT INTO Image(Body)Values(@Body);SELECT CAST(SCOPE_IDENTITY() as int)";
            string savePostImageJoin = @"INSERT INTO PostImage(PostId,ImageId)VALUES(@PostId,@ImageId)";
            int postId = unit.Query<int>(getPostId, new { email = _userName }).FirstOrDefault();
            if (postId > 0)
            {
                List<int> imageIds = new List<int>();
                //save images and retrieve their id
                foreach (Image i in _images)
                {
                    var imageId = unit.Query<int>(saveImage, i).Single();
                    imageIds.Add(imageId);
                }

                foreach (int id in imageIds)
                {
                    unit.Execute(savePostImageJoin, new { PostId = postId, ImageId = id });
                }
            }
        }
    }
}
