using ServicesAPI.Application.Commands.Categories.Create;
using ServicesAPI.Application.Mappings;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Tests.Application
{
    public class CategoryServiceTest
    {
        private MapperConfiguration _mapperConfig;
        public CategoryServiceTest()
        {
            _mapperConfig = new MapperConfiguration(cnf => cnf.AddProfile(new ApplicationMappingProfile()));
        }

        [Theory, AutoMoqData]
        public void CreateCategory_Successfuly(CreateCategory createCategory,
                                               Mock<ICategoryRepository> categoryRepository,
                                               [ApplicationMapper][Frozen] IMapper mapper)
        {
            var handler = new CreateCategoryHandler(categoryRepository.Object, mapper);

            handler.Handle(createCategory, default);
        }
    }
}
