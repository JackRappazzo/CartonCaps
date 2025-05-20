using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Api.Controllers.Messages;
using CartonCaps.Persistence.Models;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit.Attributes;
using Newtonsoft.Json;
using NSubstitute;

namespace CartonCaps.IntegrationTests.Api.Controllers.UserControllerTests.ReferredUsersTests
{
    public abstract class WhenTestingGetReferredUsers : WhenTestingUserController
    {
        protected IEnumerable<ReferredUser> ExpectedReferrals;

        [Then]
        public async Task ShouldReturnReferredUsers()
        {
            var result = JsonConvert.DeserializeObject<ReferredUsersResponse>((await Response.Content.ReadAsStringAsync()));

            Assert.That(result.Items.Count, Is.EqualTo(2));
            Assert.That(result.Items.Select(i => i.TruncatedName), Is.EquivalentTo(ExpectedReferrals.Select(e => e.TruncatedName)));
        }
        protected IEnumerable<ReferredUser> GetMockReferredUsers()
        {
            return new ReferredUser[]
                {
                    new ReferredUser()
                    {
                        CreatedOn = DateTime.Now,
                        ReferralState = ReferralState.Completed,
                        ReferringUserId = Guid.NewGuid(),
                        TruncatedName = "Trunc N."
                    },
                    new ReferredUser()
                    {
                        CreatedOn = DateTime.Now,
                        ReferralState = ReferralState.Completed,
                        ReferringUserId = Guid.NewGuid(),
                        TruncatedName = "Anon E."
                    }
                };
        }
    }
}
