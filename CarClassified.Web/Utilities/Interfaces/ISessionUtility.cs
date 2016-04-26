using CarClassified.Models.Tables;
using CarClassified.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Web.Utilities.Interfaces
{
    /// <summary>
    /// Used for storing some basic objects
    /// </summary>
    public interface ISessionUtility
    {
        /// <summary>
        /// Sets the post with images.
        /// </summary>
        /// <param name="waitingPost">The waiting post.</param>
        void SetPostWithImages(PostWithImages waitingPost);

        /// <summary>
        /// Gets the post with images.
        /// </summary>
        /// <returns></returns>
        PostWithImages GetPostWithImages();

        /// <summary>
        /// Sets the poster.
        /// </summary>
        /// <param name="poster">The poster.</param>
        void SetPoster(Poster poster);

        /// <summary>
        /// Gets the poster.
        /// </summary>
        /// <returns></returns>
        Poster GetPoster();

        /// <summary>
        /// Abondons this instance.
        /// </summary>
        void Abondon();
    }
}
