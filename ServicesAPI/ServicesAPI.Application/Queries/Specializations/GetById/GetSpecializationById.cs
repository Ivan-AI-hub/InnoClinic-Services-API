using MediatR;
using ServicesAPI.Domain;

namespace ServicesAPI.Application.Queries.Specializations.GetById
{
    public record GetSpecializationById(Guid Id) : IRequest<Specialization>;
}
