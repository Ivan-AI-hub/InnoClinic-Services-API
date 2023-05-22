using MediatR;
using ServicesAPI.Domain;

namespace ServicesAPI.Application.Queries.Services.GetByCategory
{
    public record GetServicesByCategory(string ServiceCategoryName, int PageSize, int PageNumber) : IRequest<IEnumerable<Service>>;
}
