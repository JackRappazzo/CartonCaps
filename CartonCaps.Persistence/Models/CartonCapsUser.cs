using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Persistence.Models
{
    public class CartonCapsUser
    {
        public Guid Id { get; internal set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string DisplayName { get; set; }
        public string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastLoggedInOn { get; set; }

        public Guid RegisteredSessionId { get; set; }
        public string RegisteredIpAddress { get; set; }
        public string RegisteredMacAddress { get; set; }

    }
}
