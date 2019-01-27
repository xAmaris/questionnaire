using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using questionnaire.Core.Domains.ImportFile;
using questionnaire.Infrastructure.Extensions.ExceptionHandling;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services.Interfaces;
using NLog;

namespace questionnaire.Infrastructure.Services {
    public class UnregisteredUserService : IUnregisteredUserService {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();
        private readonly IUnregisteredUserRepository _unregisteredUserRepository;
        private readonly IAccountService _accountService;

        public UnregisteredUserService (IUnregisteredUserRepository unregisteredUserRepository,
            IAccountService accountService) {
            _unregisteredUserRepository = unregisteredUserRepository;
            _accountService = accountService;
        }

        public async Task<bool> ExistByEmailAsync (string email) =>
            await _unregisteredUserRepository.GetByEmailAsync (email, false) != null;

        public async Task CreateAsync (string name, string surname, string course,
            string dateOfCompletion, string typeOfStudy, string email) {
            if (await ExistByEmailAsync (email) || await _accountService.ExistsByEmailAsync (email))
                throw new ObjectAlreadyExistException ($"User of given email: {email} already exist.");
            DateTime dateTimeOfCompletion = DateTime.ParseExact (dateOfCompletion, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            if (dateTimeOfCompletion > DateTime.UtcNow) {
                throw new Exception ("Date of completion cannot be greater than current date.");
            }
            await _unregisteredUserRepository.AddAsync (new UnregisteredUser (name, surname, course, dateTimeOfCompletion, typeOfStudy, email));
        }

        public async Task<IEnumerable<UnregisteredUser>> GetAllAsync () {
            return await _unregisteredUserRepository.GetAllAsync ();
        }

        public async Task<UnregisteredUser> GetByIdAsync (int id) {
            return await _unregisteredUserRepository.GetByIdAsync (id);
        }

        public async Task UpdateAsync (int id, string name, string surname, string course,
            string dateOfCompletion, string typeOfStudy, string email) {
            var unregisteredUser = await _unregisteredUserRepository.GetByIdAsync (id);
            DateTime dateTimeOfCompletion = DateTime.ParseExact (dateOfCompletion, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            if (dateTimeOfCompletion > DateTime.UtcNow) {
                throw new Exception ("Date of completion cannot be greater than current date.");
            }
            Logger.Info ($"Updating unregistered user with Id {id}.");
            unregisteredUser.Update (name, surname, course, dateTimeOfCompletion, typeOfStudy, email);
            await _unregisteredUserRepository.UpdateAsync (unregisteredUser);
        }

        public async Task DeleteAsync (int id) {
            var unregisteredUser = await _unregisteredUserRepository.GetByIdAsync (id);
            Logger.Info ($"Deleting unregistered user with Id {unregisteredUser.Id}.");
            await _unregisteredUserRepository.DeleteAsync (unregisteredUser);
        }
    }
}