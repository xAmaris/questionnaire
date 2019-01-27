using System.Threading.Tasks;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services.Interfaces;

namespace questionnaire.Infrastructure.Services {
    public class CareerOfficeService : ICareerOfficeService {
        private readonly ICareerOfficeRepository _careerOfficeRepository;

        public CareerOfficeService (ICareerOfficeRepository careerOfficeRepository) {
            _careerOfficeRepository = careerOfficeRepository;
        }

        public async Task<bool> ExistByEmailAsync (string email) =>
            await _careerOfficeRepository.GetByEmailAsync (email, false) != null;
    }
}