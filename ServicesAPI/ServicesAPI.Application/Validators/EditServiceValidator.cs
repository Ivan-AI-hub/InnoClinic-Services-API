using FluentValidation;
using ServicesAPI.Application.Commands.Services.Edit;

namespace ServicesAPI.Application.Validators
{
    public class EditServiceValidator : AbstractValidator<EditService>
    {
        public EditServiceValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.SpecializationId).NotNull().NotEmpty();
            RuleFor(x => x.CategoryName).NotNull().NotEmpty();

            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
        }
    }
}
