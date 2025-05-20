using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using LeapingGorilla.Testing.Core.Attributes;

namespace CartonCaps.UnitTests.Services.Referrals.ReferralLinkServiceTests.FetchTests
{
    public abstract class WhenTestingFetchValidLink : WhenTestingReferralLinkService
    {
        protected CartonCapsUser User;

        protected string Result;

        [Given]
        public void UserIsSet()
        {
            User = new CartonCapsUser()
            {
                Id = Guid.NewGuid(),
                FirstName = "Sam",
                LastName = "Pullman",
                CreatedOn = DateTime.Now,
                ReferralCode = "ABC123de",
            };
        }

        [When]
        public async Task FetchValidLinkIsCalled()
        {
            Result = await ReferralLinkService.FetchValidReferralLink(User, CancellationToken);
        }
    }
}
