using MediatR;
using ServicesAPI.Domain;

namespace ServicesAPI.Application.Commands.Specializations.Edit
{
    public record EditSpecialization(Guid id, string Name, bool Status, IEnumerable<Service> Services) : IRequest;
}
