using System.Threading.Tasks;

namespace questionnaire.Infrastructure.Services.Interfaces {
    public interface ICareerOfficeService {
        Task<bool> ExistByEmailAsync (string email);
    }
}