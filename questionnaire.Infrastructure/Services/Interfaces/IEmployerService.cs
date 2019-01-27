using System.Threading.Tasks;

namespace questionnaire.Infrastructure.Services.Interfaces {
    public interface IEmployerService {
        Task<bool> ExistByEmailAsync (string email);
    }
}