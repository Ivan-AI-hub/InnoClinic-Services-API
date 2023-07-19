using MediatR;

namespace ServicesAPI.Application.Commands.Categories.Create
{
    public record CreateCategory(string Name, int TimeSlotSize) : IRequest;
}
