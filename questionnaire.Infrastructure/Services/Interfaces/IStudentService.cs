using System.Threading.Tasks;

namespace questionnaire.Infrastructure.Services.Interfaces {
    public interface IStudentService {
        Task<bool> ExistByEmailAsync (string email);
    }
}