using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using CartonCaps.Persistence.Repositories;

namespace CartonCaps.ReferralAudit.Core.Services
{
    /// <summary>
    /// Determines a referral state for an individual referral.
    /// </summary>
    public class IndividualReferralStateEvaluator : IIndividualReferralStateEvaluator
    {
        private readonly IPurchaseHistoryRepository purchaseHistoryRepository;
        private readonly IUserRepository userRepository;

        private AuditThresholdConfiguration thresholdConfiguration;

        //Evaluation parameters may come from elsewhere, such as ParameterStore


        public IndividualReferralStateEvaluator(IUserRepository userRepository, IPurchaseHistoryRepository purchaseHistoryRepository, IAuditThresholdConfigurationFactory configFactory)
        {
            this.purchaseHistoryRepository = purchaseHistoryRepository;
            this.userRepository = userRepository;
            this.thresholdConfiguration = configFactory.Create();
        }

        /// <summary>
        /// Evaluates if a referral is in the <see cref="ReferralState.Completed"/> or <see cref="ReferralState.Pending"/> state.
        /// </summary>
        /// <remarks></remarks>
        /// <param name="referral">Referral record to evaluate</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<ReferralState> EvaluateReferralState(ReferredUser referral, CancellationToken cancellationToken)
        {
            var userToCheck = await userRepository.FetchUserById(referral.ReferredUserId, cancellationToken);

            if (userToCheck == null)
            {
                //This is a big problem and we need to blow up over it. 
                throw new InvalidOperationException($"User with id {referral.ReferredUserId} does not exist");
            }

            bool hasLoggedIn = EvaluateLoginRule(userToCheck);
            bool madePurchases = await EvaluatePurchaseRule(userToCheck, cancellationToken);

            if (hasLoggedIn && madePurchases)
            {
                return ReferralState.Completed;
            }
            else
            {
                return ReferralState.Pending;
            }
        }

        private bool EvaluateLoginRule(CartonCapsUser user)
        {

            if (user.LastLoggedInOn == null)
            {
                //User registered but has not logged in
                return false;
            }
            else
            {
                return (user.LastLoggedInOn - user.CreatedOn) > thresholdConfiguration.LoginThreshold;
            }
        }

        private async Task<bool> EvaluatePurchaseRule(CartonCapsUser userToCheck, CancellationToken cancellationToken)
        {
            //Possible for no purchase history.
            //Do not throw, just assume 0
            var purchaseSum = (await purchaseHistoryRepository.GetPurchaseHistoryByUserId(userToCheck.Id, cancellationToken))?.AmountSpentUsd ?? 0;

            return purchaseSum > thresholdConfiguration.PurchaseThreadhold;
        }
    }
}
