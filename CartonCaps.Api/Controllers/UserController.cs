using System.Text.Json.Nodes;
using CartonCaps.Api.Controllers.Messages;
using CartonCaps.Core.Services.DeferredDeepLinking;
using CartonCaps.Core.Services.Referrals;
using CartonCaps.Persistence.Models;
using CartonCaps.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CartonCaps.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IDeferredLinkService deferredLinkService;
        private readonly IReferredUserService referredUserService;
        private readonly IReferralLinkService referralCodeService;
        private readonly IUserRepository userRepository;

        public UserController(IDeferredLinkService deferredLinkService, IReferredUserService referredUserService, IReferralLinkService referralCodeService, IUserRepository userRepository)
        {
            this.deferredLinkService = deferredLinkService;
            this.referredUserService = referredUserService;
            this.userRepository = userRepository;
            this.referralCodeService = referralCodeService;
        }


        /// <summary>
        /// Returns a <see cref="PagedResponse{ReferredUser}"/> representing the
        /// referrals of the current user
        /// </summary>
        /// <param name="pageStart"></param>
        /// <param name="numberPerPage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        //[Authorize]
        [HttpGet("referredUsers")]
        public async Task<IActionResult> GetReferredUsers(int pageStart = 0, int numberPerPage = 10, CancellationToken cancellationToken = default)
        {
            //Mock for User.Identity.GetUserId()
            var userId = CartonCapsUser.MockLoggedInUserId;

            var referredUsers = await referredUserService.GetUserReferralsById(userId, pageStart, numberPerPage, cancellationToken);

            return Ok(new ReferredUsersResponse()
            {
                Items = referredUsers.Referrals,
                NumberPerPage = numberPerPage,
                PageStart = pageStart,
                Total = referredUsers.Total
            });

        }

        /// <summary>
        /// Returns the current referral code as well as a deferred link for the referral registration flow
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        //[Authorize]
        [HttpGet("referralCode")]
        public async Task<IActionResult> GetReferralCodeAndLink(CancellationToken cancellationToken)
        {
            //Mock for User.Identity.GetUserId()
            var userId = CartonCapsUser.MockLoggedInUserId;

            var user = await userRepository.FetchUserById(userId, cancellationToken);



            return Ok(new ReferralCodeAndLinkResponse()
            {
                ReferralCode = user.ReferralCode,

                DeferredLink = await referralCodeService.FetchValidReferralLink(user, cancellationToken)
            });
        }
    }
}
