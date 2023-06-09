using MediatR;

namespace ServicesAPI.Application.Commands.Services.ChangeStatus
{
    public record ChangeServiceStatus(Guid Id, bool Status) : IRequest;
}
