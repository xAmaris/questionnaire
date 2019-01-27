using System.Threading.Tasks;

namespace questionnaire.Infrastructure.Services.Interfaces {
    public interface IGraduateService {
        Task<bool> ExistByEmailAsync (string email);
    }
}