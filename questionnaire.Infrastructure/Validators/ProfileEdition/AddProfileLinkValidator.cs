using questionnaire.Infrastructure.Commands.ProfileEdition;
using FluentValidation;

namespace questionnaire.Infrastructure.Validators.ProfileEdition {
    public class AddProfileLinkValidator : AbstractValidator<AddProfileLink> {
        public AddProfileLinkValidator () {
            RuleFor (reg => reg.Content)
                .NotNull ()
                .NotEmpty ()
                .WithMessage ("Content cannot be null or empty.");
        }
    }
}