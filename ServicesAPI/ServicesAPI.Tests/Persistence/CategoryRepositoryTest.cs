namespace ServicesAPI.Tests.Persistence
{
    public class CategoryRepositoryTest
    {
        [Theory, AutoMoqData]
        public async Task Create_Successfuly(Category category, [Frozen] Mock<ServicesContext> context)
        {
            var rep = GetCategoryRepository(context);

            await rep.CreateAsync(category);
            var categoryGottenById = await rep.GetByIdAsync(category.Id);
            var categoryGottenByName = await rep.GetByNameAsync(category.Name);

            categoryGottenById.Should().NotBeNull();
            categoryGottenById.Should().BeEquivalentTo(category);
            categoryGottenByName.Should().NotBeNull();
            categoryGottenByName.Should().BeEquivalentTo(category);
        }

        [Theory, AutoMoqData]
        public async Task GetById_Returns_Null(Guid id, [Frozen] Mock<ServicesContext> context)
        {
            var rep = GetCategoryRepository(context);

            var categoryGottenById = await rep.GetByIdAsync(id);

            categoryGottenById.Should().BeNull();
        }

        [Theory, AutoMoqData]
        public async Task GetByName_Returns_Null(string name, [Frozen] Mock<ServicesContext> context)
        {
            var rep = GetCategoryRepository(context);

            var categoryGottenById = await rep.GetByNameAsync(name);

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
