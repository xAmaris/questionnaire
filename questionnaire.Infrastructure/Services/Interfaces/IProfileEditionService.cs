using System;
using System.Threading.Tasks;

namespace questionnaire.Infrastructure.Services.Interfaces {
    public interface IProfileEditionService {
        Task AddCertificateAsync (int accountId, string title, DateTime dateOfReceived);
        Task AddCourseAsync (int accountId, string name);
        Task AddEducationAsync (int accountId, string course, int year, string specialization,
            string nameOfUniveristy, bool graduated);
        Task AddExperienceAsync (int accountId, string position, string companyName, string location,
            DateTime from, DateTime to, bool isCurrentJob);
        Task AddLanguageAsync (int accountId, string name, string proficiency);
        Task AddProfileLinkAsync (int accountId, string content);
        Task AddSkillAsync (int accountId, int skillId);
    }
}