using System;

namespace questionnaire.Infrastructure.Commands.Account {
    public class ChangePasswordByRestoringPassword {
        public Guid Token { get; set; }
        public string NewPassword { get; set; }

    }
}