
namespace CartonCaps.Core.Services.DeferredDeepLinking
{
    public interface IDeferredLinkService
    {
        Task<string> CreateReferralDeepLink(string referralCode, CancellationToken cancellationToken);
        Task<DeferredLink?> ResolveDeepLink(string linkCode, CancellationToken cancellationToken);
    }
}