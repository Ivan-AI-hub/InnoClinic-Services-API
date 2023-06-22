using MediatR;
using ServicesAPI.Domain;

namespace ServicesAPI.Application.Commands.Services.Create
{
    public record CreateService(string Name, int Price, bool Status, Guid SpecializationId, string CategoryName) : IRequest<Service>;
}
