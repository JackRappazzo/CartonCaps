using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Persistence.Models
{
    /// <summary>
    /// Represents a user in the CartonCaps app
    /// </summary>
    public class CartonCapsUser
    {
        public static readonly Guid MockLoggedInUserId = Guid.Parse("2a154b8b-9228-4701-9e57-b538a66796e0");

        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The user's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The name as it should be displayed
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Email address provided by the user
        /// </summary>
        public string Email { get; set; }
       
        /// <summary>
        /// The user's current referral code
        /// </summary>
        public string ReferralCode { get; set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// Date the user was created
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Date the user last logged in
        /// </summary>
        public DateTime LastLoggedInOn { get; set; }

        /// <summary>
        /// ID representing the session the user registered with.
        /// Potentially helpful in detecting referral fraud
        /// </summary>
        public Guid RegisteredSessionId { get; set; }

        /// <summary>
        /// IP address the user registered from.
        /// Potentially helpful in detecting referral fraud
        /// </summary>
        public string RegisteredIpAddress { get; set; }

        /// <summary>
        /// MAC address the user registered from.
        /// Potentially helpful in detecting referral fraud
        /// </summary>
        public string RegisteredMacAddress { get; set; }

    }
}
