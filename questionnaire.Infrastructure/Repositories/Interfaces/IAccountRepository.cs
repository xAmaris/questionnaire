using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface IAccountRepository {
        Task<Account> GetByIdAsync (int id, bool isTracking = true);
        Task<Account> GetByEmailAsync (string email, bool isTracking = true);
        Task<Account> GetByActivationKeyAsync (Guid activationKey, bool isTracking = true);
        Task<Account> GetWithAccountRestoringPasswordAsync (int id, bool isTracking = true);
        Task<Account> GetWithAccountRestoringPasswordByTokenAsync (Guid token, bool isTracking = true);
        Task<Account> GetWithProfileEditionByIdAsync (int id, bool isTracking = true);
        Task<IEnumerable<Account>> GetAllAsync (bool isTracking = true);
        Task UpdateAsync (Account account);
        Task DeleteAsync (Account account);
    }
}