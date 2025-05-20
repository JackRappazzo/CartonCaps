using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Core.Services.DeferredDeepLinking
{
    /// <summary>
    /// Constants for deferred link destinations
    /// </summary>
    public static class DeferredLinkDestinations
    {

        //Mock destinations

        /// <summary>
        /// Path to the referral version of the registration gateway
        /// </summary>
        public static readonly string ReferralRegistration = "registration/referral";
        
        /// <summary>
        /// Path to the standard registration gateway
        /// </summary>
        public static readonly string Registration = "registration";

        /// <summary>
        /// Path to the login screen
        /// </summary>
        public static readonly string Login = "login";
    }
}
