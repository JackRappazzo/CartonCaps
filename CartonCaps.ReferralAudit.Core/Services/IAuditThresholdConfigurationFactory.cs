namespace CartonCaps.ReferralAudit.Core.Services
{    /// <summary>
     /// Creates an <see cref="AuditThresholdConfiguration"/>
     /// </summary>
    public interface IAuditThresholdConfigurationFactory
    {
        /// <summary>
        /// Returns an <see cref="AuditThresholdConfiguration"/>
        /// </summary>
        /// <returns></returns>
        AuditThresholdConfiguration Create();
    }
}