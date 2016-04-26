using CarClassified.Common.Constants;
using CarClassified.Models.Tables;
using CarClassified.Models.Views;
using CarClassified.Web.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarClassified.Web.Utilities
{
    /// <summary>
    /// Concrete class for storing basic objects
    /// </summary>
    /// <seealso cref="CarClassified.Web.Utilities.Interfaces.ISessionUtility" />
    public class SessionUtility : ISessionUtility
    {
        private HttpSessionStateBase _session { get { return new HttpSessionStateWrapper(HttpContext.Current.Session); } }

        /// <summary>
        /// Abondons this instance.
        /// </summary>
        public void Abondon()
        {
            _session.RemoveAll();
        }

        /// <summary>
        /// Gets the poster.
        /// </summary>
        /// <returns></returns>
        public Poster GetPoster()
        {
            return _session[SessionSettings.POSTER] as Poster;
        }

        /// <summary>
        /// Gets the post with images.
        /// </summary>
        /// <returns></returns>
        public PostWithImages GetPostWithImages()
        {
            return _session[SessionSettings.WAITING_POST] as PostWithImages;
        }

        /// <summary>
        /// Sets the poster.
        /// </summary>
        /// <param name="poster">The poster.</param>
        public void SetPoster(Poster poster)
        {
            _session.Add(SessionSettings.POSTER, poster);
        }

        /// <summary>
        /// Sets the post with images.
        /// </summary>
        /// <param name="waitingPost">The waiting post.</param>
        public void SetPostWithImages(PostWithImages waitingPost)
        {
            _session.Add(SessionSettings.WAITING_POST, waitingPost);
        }
    }
}
