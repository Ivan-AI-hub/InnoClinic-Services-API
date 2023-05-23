using MediatR;
using ServicesAPI.Domain;

namespace ServicesAPI.Application.Queries.Services.GetByCategory
{
    public record GetActiveServicesByCategory(string ServiceCategoryName, int PageSize, int PageNumber) : IRequest<IEnumerable<Service>>;
}
