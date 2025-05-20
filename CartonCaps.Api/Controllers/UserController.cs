using System.Text.Json.Nodes;
using CartonCaps.Api.Controllers.Messages;
using CartonCaps.Core.Services.DeferredDeepLinking;
using CartonCaps.Core.Services.Referrals;
using CartonCaps.Persistence.Models;
using Microsoft.AspNetCore.Mvc;

namespace CartonCaps.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        IDeferredLinkService deferredLinkService;
        IReferredUserService referredUserService;

        public UserController(IDeferredLinkService deferredLinkService, IReferredUserService referredUserService) 
        {
            this.deferredLinkService = deferredLinkService;
            this.referredUserService = referredUserService;
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
        public async Task<IActionResult> GetReferredUsers(int pageStart, int numberPerPage, CancellationToken cancellationToken)
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
            return Ok(new ReferralCodeAndLinkResponse()
            {
                ReferralCode = "123ABC",

                DeferredLink = await deferredLinkService.CreateReferralDeepLink("123ABC", cancellationToken)
            });
        }
    }
}
