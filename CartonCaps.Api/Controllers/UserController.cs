using System.Text.Json.Nodes;
using CartonCaps.Api.Controllers.Messages;
using Microsoft.AspNetCore.Mvc;

namespace CartonCaps.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        int MockUserId = 555;

        public UserController()
        {

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

            return Ok(new ReferredUsersResponse()
            {
                Items = new List<ReferredUser>(),
                NumberPerPage = numberPerPage,
                PageStart = pageStart,
                Total = 100
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
                DeferredLink = "https://deferred.app/destination"
            });
        }
    }
}
