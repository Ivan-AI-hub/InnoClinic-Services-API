using FluentValidation;
using ServicesAPI.Application.Commands.Services.Create;

namespace ServicesAPI.Application.Validators
{
    public class CreateServiceValidator : AbstractValidator<CreateService>
    {
        public CreateServiceValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.SpecializationId).NotNull().NotEmpty();
            RuleFor(x => x.CategoryName).NotNull().NotEmpty();

            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
        }
    }
}
