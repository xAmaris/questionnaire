using System;
using System.Threading.Tasks;
using questionnaire.Core.Domains;
using questionnaire.Infrastructure.Extensions.ExceptionHandling;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services.Interfaces;

namespace questionnaire.Infrastructure.Services {
    public class ProfileEditionService : IProfileEditionService {
        private readonly IAccountRepository _accountRepository;
        private readonly ISkillRepository _skillRepository;

        public ProfileEditionService (IAccountRepository accountRepository,
            ISkillRepository skillRepository) {
            _accountRepository = accountRepository;
            _skillRepository = skillRepository;
        }

        public async Task AddCertificateAsync (int accountId, string title, DateTime dateOfReceived) {
            var account = await _accountRepository.GetWithProfileEditionByIdAsync (accountId);
            try {
                account.AddCertificate (new Certificate (accountId, title, dateOfReceived));
                await _accountRepository.UpdateAsync (account);
            } catch (Exception e) {
                throw new InccorectRoleException (e.Message, e);
            }
        }

        public async Task AddCourseAsync (int accountId, string name) {
            var account = await _accountRepository.GetWithProfileEditionByIdAsync (accountId);
            try {
                account.AddCourse (new Course (name));
                await _accountRepository.UpdateAsync (account);
            } catch (Exception e) {
                throw new InccorectRoleException (e.Message, e);
            }
        }

        public async Task AddEducationAsync (int accountId, string course, int year, string specialization,
            string nameOfUniveristy, bool graduated) {
            var account = await _accountRepository.GetWithProfileEditionByIdAsync (accountId);
            try {
                account.AddEducation (new Education (course, year, specialization, nameOfUniveristy, graduated));
                await _accountRepository.UpdateAsync (account);
            } catch (Exception e) {
                throw new InccorectRoleException (e.Message, e);
            }
        }

        public async Task AddExperienceAsync (int accountId, string position, string companyName, string location,
            DateTime from, DateTime to, bool isCurrentJob) {
            var account = await _accountRepository.GetWithProfileEditionByIdAsync (accountId);
            try {
                account.AddExperience (new Experience (position, companyName, location, from, to, isCurrentJob));
                await _accountRepository.UpdateAsync (account);
            } catch (Exception e) {
                throw new InccorectRoleException (e.Message, e);
            }
        }

        public async Task AddLanguageAsync (int accountId, string name, string proficiency) {
            var account = await _accountRepository.GetWithProfileEditionByIdAsync (accountId);
            try {
                account.AddLanguage (new Language (name, proficiency));
                await _accountRepository.UpdateAsync (account);
            } catch (Exception e) {
                throw new InccorectRoleException (e.Message, e);
            }
        }

        public async Task AddProfileLinkAsync (int accountId, string content) {
            var account = await _accountRepository.GetWithProfileEditionByIdAsync (accountId);
            try {
                account.AddProfileLink (new ProfileLink (content));
                await _accountRepository.UpdateAsync (account);
            } catch (Exception e) {
                throw new InccorectRoleException (e.Message, e);
            }
        }

        public async Task AddSkillAsync (int accountId, int skillId) {
            var account = await _accountRepository.GetWithProfileEditionByIdAsync (accountId);
            var skill = await _skillRepository.GetByIdAsync (skillId);
            try {
                account.AddSkill (skill);
                await _accountRepository.UpdateAsync (account);
            } catch (Exception e) {
                throw new InccorectRoleException (e.Message, e);
            }
        }
    }
}