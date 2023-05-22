using MediatR;

namespace ServicesAPI.Application.Commands.Services.Edit
{
    public record EditService(Guid Id, string Name, int Price, bool Status, Guid SpecializationId, Guid CategoryId) : IRequest;
}
