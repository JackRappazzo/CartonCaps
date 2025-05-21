namespace CartonCaps.Api.Controllers.Messages
{
    /// <summary>
    /// API response containing the referral code and deferred deep link
    /// </summary>
    public class ReferralCodeAndLinkResponse()
    {

        /// <summary>
        /// Referral code
        /// </summary>
        public string ReferralCode { get; set; }

        /// <summary>
        /// Deferred deep link that points to the referral page
        /// </summary>
        public string DeferredLink { get; set; }
    }
}
