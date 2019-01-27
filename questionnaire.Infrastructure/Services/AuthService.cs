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
        private readonly IGraduateRepository _graduateRepository;
        private readonly IGraduateService _graduateService;
        private readonly ICareerOfficeRepository _careerOfficeRepository;
        private readonly ICareerOfficeService _careerOfficeService;
        private readonly IEmployerRepository _employerRepository;
        private readonly IEmployerService _employerService;
        private readonly IAccountEmailFactory _accountEmailFactory;

        public AuthService (IAccountRepository accountRepository,
            IStudentRepository studentRepository,
            IStudentService studentService,
            IGraduateRepository graduateRepository,
            IGraduateService graduateService,
            ICareerOfficeRepository careerOfficeRepository,
            ICareerOfficeService careerOfficeService,
            IEmployerRepository employerRepository,
            IEmployerService employerService,
            IAccountEmailFactory accountEmailFactory) {
            _accountRepository = accountRepository;
            _studentRepository = studentRepository;
            _studentService = studentService;
            _graduateRepository = graduateRepository;
            _graduateService = graduateService;
            _careerOfficeRepository = careerOfficeRepository;
            _careerOfficeService = careerOfficeService;
            _employerRepository = employerRepository;
            _employerService = employerService;
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

        public async Task RegisterGraduateAsync (string name, string surname, string email, string phoneNumber,
            string password) {
            if (await _graduateService.ExistByEmailAsync (email.ToLowerInvariant ()))
                throw new ObjectAlreadyExistException ($"User of given email: {email} already exist.");
            var graduate = new Graduate (name, surname, email, phoneNumber, password);
            var activationKey = Guid.NewGuid ();
            graduate.AddAccountActivation (new AccountActivation (activationKey));
            await _graduateRepository.AddAsync (graduate);
            await _accountEmailFactory.SendActivationEmailAsync (graduate, activationKey);
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

        public async Task RegisterEmployerAsync (string name, string surname, string email, string phoneNumber,
            string password, string companyName,
            string location, string companyDescription) {
            if (await _employerService.ExistByEmailAsync (email.ToLowerInvariant ()))
                throw new ObjectAlreadyExistException ($"User of given email: {email} already exist.");
            var employer = new Employer (name, surname, email, phoneNumber, password, companyName, location,
                companyDescription);
            var activationKey = Guid.NewGuid ();
            employer.AddAccountActivation (new AccountActivation (activationKey));
            await _employerRepository.AddAsync (employer);
            await _accountEmailFactory.SendActivationEmailAsync (employer, activationKey);
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