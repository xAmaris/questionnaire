using System;
using System.Threading.Tasks;
using questionnaire.Core.Domains;
using questionnaire.Core.Domains.Abstract;
using questionnaire.Infrastructure.Extensions.ExceptionHandling;
using questionnaire.Infrastructure.Extensions.Factories.Interfaces;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services.Interfaces;
using NLog;

namespace questionnaire.Infrastructure.Services {
    public class AuthService : IAuthService {
        private readonly IAccountRepository _accountRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentService _studentService;
        private readonly ICareerOfficeRepository _careerOfficeRepository;
        private readonly ICareerOfficeService _careerOfficeService;
        private readonly IAccountEmailFactory _accountEmailFactory;

        public AuthService (IAccountRepository accountRepository,
            IStudentRepository studentRepository,
            IStudentService studentService,
            ICareerOfficeRepository careerOfficeRepository,
            ICareerOfficeService careerOfficeService,
            IAccountEmailFactory accountEmailFactory) {
            _accountRepository = accountRepository;
            _studentRepository = studentRepository;
            _studentService = studentService;
            _careerOfficeRepository = careerOfficeRepository;
            _careerOfficeService = careerOfficeService;
            _accountEmailFactory = accountEmailFactory;
        }

        public async Task<Account> LoginAsync (string email, string password) {
            var account = await _accountRepository.GetByEmailAsync (email, false);
            if (account == null || !account.Activated || account.Deleted) {
                return null;
            }
            if (!VerifyPasswordHash (password, account.PasswordHash, account.PasswordSalt))
                return null;
            return account;
        }

        public async Task RegisterStudentAsync (string name, string surname, string email, string indexNumber,
            string phoneNumber, string password) {
            if (await _studentService.ExistByEmailAsync (email.ToLowerInvariant ()))
                throw new ObjectAlreadyExistException ($"User of given email: {email} already exist.");
            var student = new Student (name, surname, email,
                indexNumber, phoneNumber, password);
            var activationKey = Guid.NewGuid ();
            student.AddAccountActivation (new AccountActivation (activationKey));
            await _studentRepository.AddAsync (student);
            await _accountEmailFactory.SendActivationEmailAsync (student, activationKey);
        }

        public async Task RegisterCareerOfficeAsync (string name, string surname, string email, string phoneNumber,
            string password) {
            if (await _careerOfficeService.ExistByEmailAsync (email.ToLowerInvariant ()))
                throw new ObjectAlreadyExistException ($"User of given email: {email} already exist.");
            var careerOffice = new CareerOffice (name, surname, email, phoneNumber, password);
            var activationKey = Guid.NewGuid ();
            careerOffice.AddAccountActivation (new AccountActivation (activationKey));
            await _careerOfficeRepository.AddAsync (careerOffice);
            await _accountEmailFactory.SendActivationEmailAsync (careerOffice, activationKey);
        }

        private bool VerifyPasswordHash (string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 (passwordSalt)) {
                var computedHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
                for (int i = 0; i < computedHash.Length; i++) {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
    }
}