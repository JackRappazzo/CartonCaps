using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;

namespace CartonCaps.Persistence.Repositories
{
    /// <inheritdoc cref="IReferralLinkRepository"/>
    public class MockReferralLinkRepository : IReferralLinkRepository
    {
        private static List<ReferralLink> referralLinks;
        
        public MockReferralLinkRepository()
        {
            if (referralLinks == null)
            {
                var data = new[]
                {
                    new ReferralLink(){
                        Id = Guid.NewGuid(),
                        UserId = CartonCapsUser.MockLoggedInUserId,
                        Url = "https://old.referral.app/link/code",
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(60),
                        ExpiresOn = DateTime.Now - TimeSpan.FromHours(5)
                    },
                    new ReferralLink() {
                        Id = Guid.NewGuid(),
                        UserId = CartonCapsUser.MockLoggedInUserId,
                        Url = "https://even.older.referral.app/link/code",
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(120),
                        ExpiresOn = DateTime.Now - TimeSpan.FromDays(60)
                    },
                };

                referralLinks = data.ToList();
            }
        }

        public async Task<ReferralLink?> FetchUnexpiredReferralLinkByUserId(Guid userId, CancellationToken cancellationToken)
        {
            return referralLinks
                .Where(r => r.UserId == userId && r.ExpiresOn > DateTime.Now)
                .OrderByDescending(l=>l.ExpiresOn)
                .FirstOrDefault();
        }

        public async Task InsertReferralLink(Guid userId, string url, DateTime expiration, CancellationToken cancellationToken)
        {
            //In live we would ensure we don't enter duplicate URLs
            //omitting for brevity here

            var referralLink = new ReferralLink()
            {
                Id = Guid.NewGuid(), //Mocking auto-generated GUID
                UserId = userId,
                Url = url,
                CreatedOn = DateTime.Now,
                ExpiresOn = expiration,
            };

            referralLinks.Add(referralLink);
        }
    }
}
