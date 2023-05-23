using MediatR;
namespace ServicesAPI.Application.Commands.Specializations.ChangeStatus
{
    public record ChangeSpecializationStatus(Guid Id, bool IsActive) : IRequest;
}
