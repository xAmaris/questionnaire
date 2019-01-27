using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.ImportFile;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface IUnregisteredUserRepository {
        Task AddAsync (UnregisteredUser unregisteredUser);
        Task AddAllAsync (IEnumerable<UnregisteredUser> unregisteredUsers);
        Task<UnregisteredUser> GetByIdAsync (int id, bool isTracking = true);
        Task<UnregisteredUser> GetByEmailAsync (string email, bool isTracking = true);
        Task<IEnumerable<UnregisteredUser>> GetAllAsync (bool isTracking = true);
        Task UpdateAsync (UnregisteredUser unregisteredUser);
        Task DeleteAsync (UnregisteredUser unregisteredUser);
    }
}