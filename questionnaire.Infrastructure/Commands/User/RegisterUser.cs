using System;

namespace questionnaire.Infrastructure.Commands.User {
    public class RegisterUser {
        public Guid EventId { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}