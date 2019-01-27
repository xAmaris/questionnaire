using System;
using questionnaire.Infrastructure.Commands.ProfileEdition;
using FluentValidation;

namespace questionnaire.Infrastructure.Validators.ProfileEdition {
    public class AddExperienceValidator : AbstractValidator<AddExperience> {
        public AddExperienceValidator () {
            RuleFor (reg => reg.Position)
                .NotNull ()
                .MinimumLength (3)
                .NotEmpty ()
                .WithMessage ("Position cannot be null or empty.");
            RuleFor (reg => reg.CompanyName)
                .NotNull ()
                .MinimumLength (3)
                .Matches (@"^[a-zA-ZàáąâäãåąčććęęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšśžÀÁÂÄÃÅĄČĆĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð'-]+(\s{1}[a-zA-ZàáąâäãåąčććęęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšśžÀÁÂÄÃÅĄČĆĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð'-]+)*$")
                .WithMessage ("Company name should contain only letters with one space between words");
            RuleFor (reg => reg.Location)
                .NotNull ()
                .NotEmpty ()
                .WithMessage ("Location cannot be null or empty.");
            RuleFor (reg => reg.From)
                .LessThan (p => DateTime.Now)
                .LessThan (p => p.To)
                .WithMessage ("From date cannot be greater than current date.")
                .NotNull ()
                .NotEmpty ();
            RuleFor (reg => reg.To)
                .GreaterThan (p => p.From)
                .WithMessage ("To date cannot be greater than current date.")
                .NotNull ()
                .NotEmpty ();
            RuleFor (reg => reg.IsCurrentJob)
                .NotNull ()
                .NotEmpty ()
                .WithMessage ("Is current job cannot be null or empty.");
        }
    }
}