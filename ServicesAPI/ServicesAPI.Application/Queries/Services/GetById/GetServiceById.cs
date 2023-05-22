using MediatR;
using ServicesAPI.Domain;

namespace ServicesAPI.Application.Queries.Services.GetById
{
    public record GetServiceById(Guid id) : IRequest<Service>;
}
