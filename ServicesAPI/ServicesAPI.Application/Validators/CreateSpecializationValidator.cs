using FluentValidation;
using ServicesAPI.Application.Commands.Specializations.Create;

namespace ServicesAPI.Application.Validators
{
    public class CreateSpecializationValidator : AbstractValidator<CreateSpecialization>
    {
        public CreateSpecializationValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
