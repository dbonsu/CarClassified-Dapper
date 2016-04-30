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
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Services;

namespace CarClassified.Web.ApiControllers
{
    /// <summary>
    ///Handles all posting
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
        public HttpResponseMessage Post([FromBody] PostingDetailsVM model, bool hasImage)
        {
            if (ModelState.IsValid)
            {
                Post post = _mapper.Map<Post>(model);

                Poster poster = _mapper.Map<Poster>(model);
                Vehicle vehicle = _mapper.Map<Vehicle>(model);
                _db.Execute(new CreateNewPost(post, vehicle, poster));
                if (!hasImage)
                {
                    return new HttpResponseMessage(HttpStatusCode.Created); //201
                }

                return Request.CreateResponse<string>(HttpStatusCode.OK, poster.Email); //200
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Posts the image.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        [Route("image")]
        [HttpPost]
        public IHttpActionResult PostImage(string email)
        {
            var files = HttpContext.Current.Request.Files;
            if (string.IsNullOrEmpty(email))
            {
                try
                {
                    ICollection<Image> images = null;
                    ConvertToByArray(files, out images);
                    _db.Execute(new AddImagesToPost(images, email));
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// Converts to by array.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="images">The images.</param>
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
                    image.Extension = System.IO.Path.GetExtension(httpPostFile.FileName);
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