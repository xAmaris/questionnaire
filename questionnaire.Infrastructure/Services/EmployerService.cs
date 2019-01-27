using System.Threading.Tasks;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services.Interfaces;

namespace questionnaire.Infrastructure.Services {
    public class EmployerService : IEmployerService {
        private readonly IEmployerRepository _employerRepository;

        public EmployerService (IEmployerRepository employerRepository) {
            _employerRepository = employerRepository;
        }

        public async Task<bool> ExistByEmailAsync (string email) =>
            await _employerRepository.GetByEmailAsync (email, false) != null;
    }
}