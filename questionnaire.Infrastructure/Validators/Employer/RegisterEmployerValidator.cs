using questionnaire.Infrastructure.Commands.Employer;
using FluentValidation;

namespace questionnaire.Infrastructure.Validators.Employer {
    public class RegisterEmployerValidator : AbstractValidator<RegisterEmployer> {
        public RegisterEmployerValidator () {
            RuleFor (reg => reg.Name)
                .NotNull ()
                .MinimumLength (3)
                .Matches (@"^[a-zA-ZàáąâäãåąčććęęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšśžÀÁÂÄÃÅĄČĆĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð'-]+(\s{1}[a-zA-ZàáąâäãåąčććęęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšśžÀÁÂÄÃÅĄČĆĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð'-]+)*$")
                .WithMessage ("Name should contain only letters with one space between words");
            RuleFor (reg => reg.Surname)
                .NotNull ()
                .MinimumLength (3)
                .Matches (@"^[a-zA-ZàáąâäãåąčććęęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšśžÀÁÂÄÃÅĄČĆĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð'-]+(\s{1}[a-zA-ZàáąâäãåąčććęęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšśžÀÁÂÄÃÅĄČĆĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð'-]+)*$")
                .WithMessage ("Surname should contain only letters with one space between words");
            RuleFor (reg => reg.Email)
                .NotNull ()
                .EmailAddress ()
                .MinimumLength (5);
            RuleFor (reg => reg.PhoneNumber)
                .NotNull ()
                .Matches (@"(?:[0-9]){9,9}$")
                .WithMessage ("Phone number is invalid.");
            RuleFor (reg => reg.Password)
                .NotNull ()
                .Must (u => !string.IsNullOrEmpty (u) && u.Contains (""))
                .WithMessage ("Password should not contain space")
                .Matches (@"^(?=.*\d)(?=.*[^\d]).{6,30}$")
                .WithMessage ("Passoword must consist of 6-30 characters and at least one number");
            RuleFor (reg => reg.CompanyName)
                .NotNull ()
                .MinimumLength (3)
                .Matches (@"^[a-zA-ZàáąâäãåąčććęęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšśžÀÁÂÄÃÅĄČĆĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð'-]+(\s{1}[a-zA-ZàáąâäãåąčććęęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšśžÀÁÂÄÃÅĄČĆĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð'-]+)*$")
                .WithMessage ("Company name should contain only letters with one space between words");
            RuleFor (reg => reg.Location)
                .NotEmpty ()
                .WithMessage ("Location cannot be empty.")
                .NotNull ()
                .WithMessage ("Location cannot be null.");
            RuleFor (reg => reg.CompanyDescription)
                .NotEmpty ()
                .WithMessage ("Company description cannot be empty.")
                .NotNull ()
                .WithMessage ("Company description cannot be null.");
        }
    }
}