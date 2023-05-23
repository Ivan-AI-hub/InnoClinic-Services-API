using MediatR;
namespace ServicesAPI.Application.Commands.Specializations.Edit
{
    public record EditSpecialization(Guid Id, string Name, bool IsActive) : IRequest;
}
