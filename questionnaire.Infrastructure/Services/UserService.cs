using System;
using System.Threading.Tasks;
using questionnaire.Core.Domain;
using questionnaire.Core.Repositories;
using questionnaire.Infrastructure.Services.Interfaces;

namespace questionnaire.Infrastructure.Services {
    public class UserService : IUserService {
        private readonly IUserRepository _userRepository;
        public UserService (IUserRepository userRepository) {
            _userRepository = userRepository;
        }
        public async Task RegisterAsync (Guid userId, string email, string name, string password, string role = "user") {
            var user = await _userRepository.GetAsync (email);
            if (user != null) {
                throw new Exception ($"User with email '{email}' exists");
            }
            user = new User (userId, role, name, email, password);
            await _userRepository.AddAsync (user);
        }
    }
}