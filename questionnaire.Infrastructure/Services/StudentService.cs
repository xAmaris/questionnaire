using System.Threading.Tasks;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services.Interfaces;

namespace questionnaire.Infrastructure.Services {
    public class StudentService : IStudentService {
        private readonly IStudentRepository _studentRepository;

        public StudentService (IStudentRepository studentRepository) {
            _studentRepository = studentRepository;
        }

        public async Task<bool> ExistByEmailAsync (string email) =>
            await _studentRepository.GetByEmailAsync (email, false) != null;
    }
}