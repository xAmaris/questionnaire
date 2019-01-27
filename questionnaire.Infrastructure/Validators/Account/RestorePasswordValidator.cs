using questionnaire.Infrastructure.Commands.Account;
using FluentValidation;

namespace questionnaire.Infrastructure.Validators.Account {
    public class RestorePasswordValidator : AbstractValidator<RestorePassword> {
        public RestorePasswordValidator () {
            RuleFor (reg => reg.Email)
                .EmailAddress ()
                .MinimumLength (5)
                .MaximumLength (150)
                .NotNull ();
        }
    }
}