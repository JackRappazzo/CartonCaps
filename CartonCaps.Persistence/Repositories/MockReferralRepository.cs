using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;

namespace CartonCaps.Persistence.Repositories
{
    /// <inheritdoc cref="IReferralRepository" />
    public class MockReferralRepository : IReferralRepository
    {
        private static List<ReferredUser> referredUsers;
        public MockReferralRepository()
        {
            if (referredUsers == null)
            {
                #region Mock Data
                referredUsers = new ReferredUser[]
                {
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(20),
                        ReferralState = ReferralState.Completed,
                        TruncatedName = "Sam P."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(5),
                        ReferralState = ReferralState.Completed,
                        TruncatedName = "William S."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(7),
                        ReferralState = ReferralState.Completed,
                        TruncatedName = "Sabrina M."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(35),
                        ReferralState = ReferralState.Completed,
                        TruncatedName = "Siobhan M."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(47),
                        ReferralState = ReferralState.Completed,
                        TruncatedName = "Hien N."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(2),
                        ReferralState = ReferralState.Completed,
                        TruncatedName = "Mark M."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(5),
                        ReferralState = ReferralState.Pending,
                        TruncatedName = "Louise C."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(120),
                        ReferralState = ReferralState.Pending,
                        TruncatedName = "Riley O."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(55),
                        ReferralState = ReferralState.Pending,
                        TruncatedName = "Sam P."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(3),
                        ReferralState = ReferralState.Pending,
                        TruncatedName = "William S."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(90),
                        ReferralState = ReferralState.Pending,
                        TruncatedName = "Sabrina M."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(100),
                        ReferralState = ReferralState.Completed,
                        TruncatedName = "Aoife N."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(23),
                        ReferralState = ReferralState.Completed,
                        TruncatedName = "Mary A."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(11),
                        ReferralState = ReferralState.Completed,
                        TruncatedName = "Millicent M."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(32),
                        ReferralState = ReferralState.NeedsAudit,
                        TruncatedName = "Larry B."
                    },
                    new ReferredUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferringUserId = CartonCapsUser.MockLoggedInUserId,
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(5),
                        ReferralState = ReferralState.NeedsAudit,
                        TruncatedName = "Bill B."
                    },
                }.ToList();
                #endregion
            }
        }

        public async Task<IEnumerable<ReferredUser>> GetReferredUsersByReferringId(Guid referringUserId, CancellationToken cancellationToken)
        {
            return referredUsers.Where(u => u.ReferringUserId == referringUserId);
        }

        public async Task<bool> UpdateReferralStateById(Guid referralId, ReferralState newReferralState, CancellationToken cancellationToken)
        {
            var userToUpdate = referredUsers.Where(r => r.Id == referralId).FirstOrDefault();
            if (userToUpdate != null)
            {
                userToUpdate.ReferralState = newReferralState;
                return true;
            }
            else return false;

        }
    }
}