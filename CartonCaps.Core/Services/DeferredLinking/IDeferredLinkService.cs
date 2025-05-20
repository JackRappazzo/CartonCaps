
namespace CartonCaps.Core.Services.DeferredDeepLinking
{
    /// <summary>
    /// Handles creating and resolving deferred deep links
    /// </summary>
    public interface IDeferredLinkService
    {
        /// <summary>
        /// Create a new deferred deep link to direct a referral to the app
        /// </summary>
        /// <param name="referralCode">Unique code to associate a referral to a user</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> CreateReferralDeepLink(string referralCode, CancellationToken cancellationToken);
        
        /// <summary>
        /// Retrieves the destination and metadata for a deferred deep link
        /// </summary>
        /// <param name="linkCode">Unique code that identifies the deep link. Found at the end of the url.
        /// For example: https://sample.com/app/link/ABCD1234. ABC1234 is the link code</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DeferredLink?> ResolveDeepLink(string linkCode, CancellationToken cancellastionToken);
    }
}