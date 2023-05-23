using FluentValidation;
using ServicesAPI.Application.Commands.Services.Edit;

namespace ServicesAPI.Application.Validators
{
    public class EditSpecializationValidator : AbstractValidator<EditService>
    {
        public EditSpecializationValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
