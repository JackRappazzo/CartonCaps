using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using LeapingGorilla.Testing.Core.Attributes;
using NSubstitute.ReturnsExtensions;

namespace CartonCaps.UnitTests.Services.Referrals.ReferralLinkServiceTests.FetchTests
{
    public abstract class WhenTestingFetchValidLink : WhenTestingReferralLinkService
    {
        protected Guid UserId;

        protected (string referralCode, string deferredLink) Result;

        [Given]
        public void UserIdIsSet()
        {
            UserId = Guid.NewGuid();
        }

        [Given]
        public void RepositoryDoesNotReturnALink()
        {
            ReferralLinkRepository.FetchUnexpiredReferralLinkByUserId(UserId, CancellationToken)
                .ReturnsNull();
        }

        [When]
        public async Task FetchValidLinkIsCalled()
        {
            Result = await ReferralLinkService.FetchReferralCodeAndValidLink(UserId, CancellationToken);
        }
    }
}
