using System;
using questionnaire.Core.Domains.ImportFile;
using questionnaire.Infrastructure.Commands.ImportFile;
using FluentValidation;

namespace questionnaire.Infrastructure.Validators.ImportFile {
    public class AddUnregisteredUserValidator : AbstractValidator<AddUnregisteredUser> {
        public AddUnregisteredUserValidator () {
            RuleFor (reg => reg.Name)
                .MinimumLength (3)
                .Matches (@"^[a-zA-ZàáąâäãåąčććęęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšśžÀÁÂÄÃÅĄČĆĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð'-]+(\s{1}[a-zA-ZàáąâäãåąčććęęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšśžÀÁÂÄÃÅĄČĆĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð'-]+)*$")
                .WithMessage ("Name should contain only letters with one space between words.")
                .NotNull ()
                .WithMessage ("Name cannot be null.");
            RuleFor (reg => reg.Surname)
                .MinimumLength (3)
                .Matches (@"^[a-zA-ZàáąâäãåąčććęęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšśžÀÁÂÄÃÅĄČĆĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð'-]+(\s{1}[a-zA-ZàáąâäãåąčććęęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšśžÀÁÂÄÃÅĄČĆĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð'-]+)*$")
                .WithMessage ("Surname should contain only letters with one space between words.")
                .NotNull ()
                .WithMessage ("Surname cannot be null.");
            RuleFor (reg => reg.Course)
                .MaximumLength (70)
                .MinimumLength (3)
                .WithMessage ("Course cannot be null or empty.")
                .NotNull ()
                .WithMessage ("Course cannot be null.");
            RuleFor (reg => reg.DateOfCompletion)
                .NotNull ()
                .NotEmpty ()
                .WithMessage ("Date of completion cannot be greater than current date.");
            RuleFor (reg => reg.TypeOfStudy)
                .MaximumLength (70)
                .MinimumLength (3)
                .NotNull ()
                .WithMessage ("Type of study cannot be null.");
            RuleFor (reg => reg.Email)
                .EmailAddress ()
                .MinimumLength (5)
                .NotNull ()
                .WithMessage ("Email cannot be null");

        }
    }
}