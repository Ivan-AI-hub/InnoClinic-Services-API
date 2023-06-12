namespace ServicesAPI.Tests.Persistence
{
    public class SpecializationRepositoryTest
    {
        [Theory, AutoMoqData]
        public async Task Create_Successfuly(Specialization specialization, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            var rep = GetSpecializationRepository(context);
            //act
            await rep.CreateAsync(specialization);
            var addedSpecialization = await rep.GetByIdAsync(specialization.Id);
            //assert
            addedSpecialization.Should().NotBeNull();
            addedSpecialization.Should().BeEquivalentTo(specialization);
        }

        [Theory, AutoMoqData]
        public async Task Edit_Successfuly(Specialization specialization, Specialization newSpecialization, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            var rep = GetSpecializationRepository(context);
            //act
            await rep.CreateAsync(specialization);
            await rep.EditAsync(specialization.Id, newSpecialization);
            var editedSpecialization = await rep.GetByIdAsync(specialization.Id);
            //assert
            editedSpecialization.Should().NotBeNull();
            editedSpecialization.Id.Should().Be(specialization.Id);
            editedSpecialization.Name.Should().Be(newSpecialization.Name);
        }

        [Theory, AutoMoqData]
        public async Task EditStatus_Successfuly(Specialization specialization, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            var rep = GetSpecializationRepository(context);
            //act
            await rep.CreateAsync(specialization);
            await rep.EditStatusAsync(specialization.Id, !specialization.IsActive);
            var editedSpecialization = await rep.GetByIdAsync(specialization.Id);
            //assert
            editedSpecialization.Should().NotBeNull();
            editedSpecialization.IsActive.Should().NotBe(specialization.IsActive);

        }

        [Theory, AutoMoqData]
        public async Task IsSpecializationExist_True(Specialization specialization, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            var rep = GetSpecializationRepository(context);
            //act
            await rep.CreateAsync(specialization);
            var result = rep.IsSpecializationExist(specialization.Id);
            //assert
            result.Should().BeTrue();
        }

        [Theory, AutoMoqData]
        public void IsSpecializationExist_False(Guid randomId, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            var rep = GetSpecializationRepository(context);
            //act
            var result = rep.IsSpecializationExist(randomId);
            //assert
            result.Should().BeFalse();
        }

        private SpecializationRepository GetSpecializationRepository(Mock<ServicesContext> context)
        {
            var db = new InMemoryDatabase();
            db.CreateTable<Specialization>("Specializations");
            context.Setup(x => x.CreateConnection()).Returns(() => db.OpenConnection());
            return new SpecializationRepository(context.Object);
        }
    }
}
