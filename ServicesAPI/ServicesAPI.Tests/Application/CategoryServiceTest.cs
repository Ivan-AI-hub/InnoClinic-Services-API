using ServicesAPI.Application.Commands.Categories.Create;
using ServicesAPI.Application.Mappings;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Tests.Application
{
    public class CategoryServiceTest
    {
        [Theory, AutoMoqData]
        public async Task CreateCategory_Successfuly(CreateCategory createCategory,
                                               Mock<ServicesContext> context,
                                               [ApplicationMapper][Frozen] IMapper mapper)
        {
            //arrange
            var repository = GetCategoryRepository(context);
            var createHandler = new CreateCategoryHandler(repository, mapper);

            //act
            await createHandler.Handle(createCategory, default);
            var createdCategory = await repository.GetByNameAsync(createCategory.Name, default);

            //assert
            createdCategory.Should().NotBeNull();
            createdCategory.Name.Should().Be(createCategory.Name);
            createdCategory.TimeSlotSize.Should().Be(createCategory.TimeSlotSize);
        }

        private ICategoryRepository GetCategoryRepository(Mock<ServicesContext> context)
        {
            var db = new InMemoryDatabase();
            db.CreateTable<Category>("Categories");
            context.Setup(x => x.CreateConnection()).Returns(() => db.OpenConnection());
            return new CategoryRepository(context.Object);
        }
    }
}
