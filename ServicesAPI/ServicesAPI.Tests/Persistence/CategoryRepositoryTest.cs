namespace ServicesAPI.Tests.Persistence
{
    public class CategoryRepositoryTest
    {
        [Theory, AutoMoqData]
        public async Task Create_Successfuly(Category category, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            var rep = GetCategoryRepository(context);
            //act
            await rep.CreateAsync(category);
            var categoryGottenById = await rep.GetByIdAsync(category.Id);
            var categoryGottenByName = await rep.GetByNameAsync(category.Name);
            //assert
            categoryGottenById.Should().NotBeNull();
            categoryGottenById.Should().BeEquivalentTo(category);
            categoryGottenByName.Should().NotBeNull();
            categoryGottenByName.Should().BeEquivalentTo(category);
        }

        [Theory, AutoMoqData]
        public async Task GetById_Returns_Null(Guid id, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            var rep = GetCategoryRepository(context);
            //act
            var categoryGottenById = await rep.GetByIdAsync(id);
            //assert
            categoryGottenById.Should().BeNull();
        }

        [Theory, AutoMoqData]
        public async Task GetByName_Returns_Null(string name, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            var rep = GetCategoryRepository(context);
            //act
            var categoryGottenById = await rep.GetByNameAsync(name);
            //assert
            categoryGottenById.Should().BeNull();
        }

        private CategoryRepository GetCategoryRepository(Mock<ServicesContext> context)
        {
            var db = new InMemoryDatabase();
            db.CreateTable<Category>("Categories");
            context.Setup(x => x.CreateConnection()).Returns(() => db.OpenConnection());
            return new CategoryRepository(context.Object);
        }
    }
}
