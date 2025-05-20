using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Core.Services.DeferredDeepLinking
{
    /// <summary>
    /// Represents a deferred deep link
    /// </summary>
    public class DeferredLink
    {
        /// <summary>
        /// Slug representing the page in an app.
        /// Note: I am not _really_ sure what these are supposed to look like, 
        /// but this seemed fair
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Metadata associated with a deep link.
        /// Useful if the user does not have the app installed at the time
        /// they click on the link, as query parameters are not preserved
        /// in that instance
        /// </summary>
        public Dictionary<string, object> Data { get; set; }
    }
}
