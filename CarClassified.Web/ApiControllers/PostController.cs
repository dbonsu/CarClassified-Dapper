using AutoMapper;
using CarClassified.DataLayer.Commands.PostingCommands;
using CarClassified.DataLayer.Interfaces;
using CarClassified.DataLayer.Queries.AssetsQueries;
using CarClassified.Models.Tables;
using CarClassified.Models.Views;
using CarClassified.Web.Utilities.Interfaces;
using CarClassified.Web.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Services;

namespace CarClassified.Web.ApiControllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/post")]
    public class PostController : ApiController
    {
        private IDatabase _db;
        private IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostController"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="mapper">The mapper.</param>
        public PostController(IDatabase db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="hasImage">if set to <c>true</c> [has image].</param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] PostDetailsVM model, bool hasImage)
        {
            if (ModelState.IsValid)
            {
                //maps parts
                //update userdetails
                //create post and return id
                //create vehicle ..done
                Post post = _mapper.Map<Post>(model);
                post.StateId = 13;//remove after testing
                Poster poster = _mapper.Map<Poster>(model);
                Vehicle vehicle = _mapper.Map<Vehicle>(model);

                if (!hasImage)
                {
                    //_db.Execute(new CreateNewPost(post, vehicle, poster));
                    //Thread.CurrentPrincipal = null
                    return new HttpResponseMessage(HttpStatusCode.Created); //201
                }

                return new HttpResponseMessage(HttpStatusCode.OK); //200
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [Route("image")]
        [HttpPost]
        public IHttpActionResult PostImage()
        {
            //var userName = Thread.CurrentPrincipal.Identity.Name;

            var files = HttpContext.Current.Request.Files;
            try
            {
                ICollection<Image> images = null;
                ConvertToByArray(files, out images);
                // _db.Execute(new AddImagesToPost(images, "der@d.com"));
                //Thread.CurrentPrincipal.Identity = null;
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok();
        }

        private void ConvertToByArray(HttpFileCollection files, out ICollection<Image> images)
        {
            images = new List<Image>();
            try
            {
                foreach (string k in files.Keys)
                {
                    HttpPostedFile httpPostFile = files.Get(k);
                    int length = httpPostFile.ContentLength;
                    byte[] postedFile = new byte[length];
                    var image = new Image();
                    httpPostFile.InputStream.Read(postedFile, 0, length);
                    image.Body = postedFile;
                    image.Extenstion = System.IO.Path.GetExtension(httpPostFile.FileName);
                    images.Add(image);
                }
            }
            catch (Exception ex)
            {
                //add logging
            }
        }
    }
}
