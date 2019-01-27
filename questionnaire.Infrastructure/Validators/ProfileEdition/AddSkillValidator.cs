using questionnaire.Infrastructure.Commands.ProfileEdition;
using FluentValidation;

namespace questionnaire.Infrastructure.Validators.ProfileEdition {
    public class AddSkillValidator : AbstractValidator<AddSkill> {
        public AddSkillValidator () {
            RuleFor (reg => reg.SkillId)
                .NotNull ()
                .NotEmpty ()
                .WithMessage ("Skill Id cannot be null or empty");
        }
    }
}