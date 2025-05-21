using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;

namespace CartonCaps.Persistence.Repositories
{

    /// <summary>
    /// Repository managing a user's purchase history records
    /// </summary>
    public interface IPurchaseHistoryRepository
    {

        /// <summary>
        /// Returns a summary of a user's purchase history
        /// </summary>
        /// <param name="userId">The user to return a history for</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PurchaseHistory> GetPurchaseHistoryByUserId(Guid userId, CancellationToken cancellationToken);
    }
}
