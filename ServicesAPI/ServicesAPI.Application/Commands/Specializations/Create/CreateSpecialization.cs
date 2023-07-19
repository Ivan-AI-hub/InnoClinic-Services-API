using MediatR;
using ServicesAPI.Domain;

namespace ServicesAPI.Application.Commands.Specializations.Create
{
    public record CreateSpecialization(string Name, bool IsActive) : IRequest<Specialization>;
}
