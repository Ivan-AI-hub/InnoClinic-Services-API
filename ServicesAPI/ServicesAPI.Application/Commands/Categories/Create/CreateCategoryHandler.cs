using AutoMapper;
using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Commands.Categories.Create
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategory>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public Task Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            return _categoryRepository.CreateAsync(category, cancellationToken);
        }
    }
}
