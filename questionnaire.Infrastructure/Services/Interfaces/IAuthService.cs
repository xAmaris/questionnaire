using System.Threading.Tasks;
using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Infrastructure.Services.Interfaces {
    public interface IAuthService {
        Task RegisterStudentAsync(string name, string surname, string email,
            string indexNumber, string phoneNumber, string password);
        Task RegisterGraduateAsync (string name, string surname, string email, string phoneNumber, string password);
        Task RegisterCareerOfficeAsync (string name, string surname, string email, string phoneNumber, string password);
        Task RegisterEmployerAsync (string name, string surname, string email, string phoneNumber, string password,
            string companyName, string location, string companyDescription);
        Task<Account> LoginAsync (string email, string password);
    }
}