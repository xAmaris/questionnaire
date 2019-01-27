using System;
using questionnaire.Infrastructure.Commands.ProfileEdition;
using FluentValidation;

namespace questionnaire.Infrastructure.Validators.ProfileEdition {
    public class AddCertificateValidator : AbstractValidator<AddCertificate> {
        public AddCertificateValidator () {
            RuleFor (reg => reg.Title)
                .NotNull ().WithMessage ("Title cannot be null.")
                .NotEmpty ().WithMessage ("Title cannot be empty");
            RuleFor (reg => reg.DateOfReceived)
                .LessThan (p => DateTime.Now).WithMessage ("Date Of received cannot be greater than current date.");
        }
    }
}