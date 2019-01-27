using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface ICareerOfficeRepository {
        Task AddAsync (CareerOffice careerOffice);
        Task<CareerOffice> GetByIdAsync (int id, bool isTracking = true);
        Task<CareerOffice> GetByEmailAsync (string email, bool isTracking = true);
        Task UpdateAsync (CareerOffice careerOffice);
    }
}