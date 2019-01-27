using System.Threading.Tasks;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services.Interfaces;

namespace questionnaire.Infrastructure.Services {
    public class GraduateService : IGraduateService {
        private readonly IGraduateRepository _graduateRepository;

        public GraduateService (IGraduateRepository graduateRepository) {
            _graduateRepository = graduateRepository;
        }

        public async Task<bool> ExistByEmailAsync (string email) =>
            await _graduateRepository.GetByEmailAsync (email, false) != null;
    }
}