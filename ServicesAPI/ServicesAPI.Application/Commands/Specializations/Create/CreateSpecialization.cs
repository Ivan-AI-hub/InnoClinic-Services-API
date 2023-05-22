using MediatR;
using ServicesAPI.Domain;

namespace ServicesAPI.Application.Commands.Specializations.Create
{
    public record CreateSpecialization(string Name, bool Status, IEnumerable<Service> Services) : IRequest<Specialization>;
}
