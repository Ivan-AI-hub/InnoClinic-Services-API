using MediatR;
using ServicesAPI.Application.Commands.Services.Create;
using ServicesAPI.Domain;

namespace ServicesAPI.Application.Commands.Specializations.Create
{
    public record CreateSpecialization(string Name, bool Status) : IRequest<Specialization>;
}
