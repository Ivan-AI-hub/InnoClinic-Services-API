using MediatR;
using ServicesAPI.Domain;

namespace ServicesAPI.Application.Queries.Services.GetBySpecialization
{
    public record GetServicesBySpecialization(string SpecializationName) : IRequest<IEnumerable<Service>>;
}
