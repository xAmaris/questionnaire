using questionnaire.Infrastructure.Commands.Account;
using FluentValidation;

namespace questionnaire.Infrastructure.Validators.Account {
    public class ChangePasswordValidator : AbstractValidator<ChangePassword> {
        public ChangePasswordValidator () {
            RuleFor (reg => reg.OldPassword)
                .NotNull ()
                .Must (u => !u.Contains (" "))
                .WithMessage ("Password should not contain space")
                .Matches (@"^(?=.*\d)(?=.*[^\d]).{6,30}$")
                .WithMessage ("Passoword must consist of 6-30 characters and at least one number");
            RuleFor (reg => reg.NewPassword)
                .NotNull ()
                .Must (u => !u.Contains (" "))
                .WithMessage ("Password should not contain space")
                .Matches (@"^(?=.*\d)(?=.*[^\d]).{6,30}$")
                .WithMessage ("Passoword must consist of 6-30 characters and at least one number")
                .NotEqual (reg => reg.OldPassword)
                .WithMessage ("New password should not be equal to old password.");
        }
    }
}