using System.Threading.Tasks;
using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Infrastructure.Services.Interfaces {
    public interface IAuthService {
        Task RegisterStudentAsync (string name, string surname, string email,
            string indexNumber, string phoneNumber, string password);
        Task RegisterCareerOfficeAsync (string name, string surname, string email, string phoneNumber, string password);
        Task<Account> LoginAsync (string email, string password);
    }
}