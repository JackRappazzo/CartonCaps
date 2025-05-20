using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;

namespace CartonCaps.Persistence.Repositories
{
    public class MockUserRepository : IUserRepository
    {

        protected static List<CartonCapsUser> userStore;


        public MockUserRepository()
        {
            if (userStore == null)
            {
                #region Mock Data

                userStore = new CartonCapsUser[]
               {
                new CartonCapsUser()
                {
                    Id = CartonCapsUser.MockLoggedInUserId,
                    FirstName = "Lorie",
                    LastName = "Ipsum",
                    DateOfBirth = new DateOnly(2000, 1,1),
                    DisplayName = "LorieIpsum",
                    Email = "sample1@email.com",
                    ReferralCode = "ABC123de",
                    RegisteredIpAddress = "127.0.0.1",
                    RegisteredMacAddress = string.Empty,
                    RegisteredSessionId = Guid.NewGuid(),
                },
                new CartonCapsUser()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Sam",
                    LastName = "Pullman",
                    DateOfBirth = new DateOnly(1980, 1,30),
                    DisplayName = "SamPullMan",
                    Email = "sample@man.com",
                    ReferralCode = "12AbD345",
                    RegisteredIpAddress = "127.0.0.1",
                    RegisteredMacAddress = string.Empty,
                    RegisteredSessionId = Guid.NewGuid(),
                },
                new CartonCapsUser()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Daniel",
                    LastName = "Smith",
                    DateOfBirth = new DateOnly(1981, 2,10),
                    DisplayName = "Danny Smith",
                    Email = "dan@smith.com",
                    ReferralCode = "Ab12EDz",
                    RegisteredIpAddress = "127.0.0.1",
                    RegisteredMacAddress = string.Empty,
                    RegisteredSessionId = Guid.NewGuid(),
                },
                new CartonCapsUser()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Chelsea",
                    LastName = "Monet",
                    DateOfBirth = new DateOnly(1970, 1,10),
                    DisplayName = "CMonet",
                    Email = "c@monet.com",
                    ReferralCode = "beDf1254",
                    RegisteredIpAddress = "127.0.0.1",
                    RegisteredMacAddress = string.Empty,
                    RegisteredSessionId = Guid.NewGuid(),
                },
                new CartonCapsUser() {
                    Id = Guid.NewGuid(),
                    FirstName = "Paul",
                    LastName = "Xavier",
                    DateOfBirth = new DateOnly(1995, 12, 10),
                    DisplayName = "PaulXavier",
                    Email = "xavier@paul.com",
                    ReferralCode = "mnop1234",
                    RegisteredIpAddress = "127.0.0.1",
                    RegisteredMacAddress = string.Empty,
                    RegisteredSessionId = Guid.NewGuid(),
                }
               }.ToList();
                #endregion
            }
        }

        /// <summary>
        /// Returns a <see cref="CartonCapsUser"/> if it exists. Returns null otherwise
        /// </summary>
        /// <param name="userId">The <see cref="Guid"/> that represents the user</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CartonCapsUser?> FetchUserById(Guid userId, CancellationToken cancellationToken)
        {
            return userStore.Where(u => u.Id == userId).FirstOrDefault();
        }
    }
}
