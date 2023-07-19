using MediatR;
using ServicesAPI.Domain;

namespace ServicesAPI.Application.Queries.Specializations.GetInfo
{
    public record GetSpecializationsInfo(int PageSize, int PageNumber) : IRequest<IEnumerable<Specialization>>;
}
