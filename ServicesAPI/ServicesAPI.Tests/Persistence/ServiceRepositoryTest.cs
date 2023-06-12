namespace ServicesAPI.Tests.Persistence
{
    public class ServiceRepositoryTest
    {
        [Theory, AutoMoqData]
        public async Task Create_Successfuly(Service service, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            service.Specialization = null;
            service.Category = null;
            var configuredContext = ConfigureServiceContext(context);
            var rep = new ServiceRepository(configuredContext);
            //act
            await rep.CreateAsync(service);
            var addedService = await rep.GetByIdAsync(service.Id);
            //assert
            addedService.Should().NotBeNull();
            addedService.Should().BeEquivalentTo(service);
        }

        [Theory, AutoMoqData]
        public async Task Edit_Successfuly(Service service, Service newService, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            service.Specialization = null;
            service.Category = null;
            var configuredContext = ConfigureServiceContext(context);
            var rep = new ServiceRepository(configuredContext);
            //act
            await rep.CreateAsync(service);
            await rep.EditAsync(service.Id, newService);
            var editedService = await rep.GetByIdAsync(service.Id);
            //assert
            editedService.Should().NotBeNull();
            editedService.Id.Should().Be(service.Id);
            editedService.SpecializationId.Should().Be(newService.SpecializationId);
            editedService.Name.Should().Be(newService.Name);
        }

        [Theory, AutoMoqData]
        public async Task EditStatus_Successfuly(Service service, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            service.Specialization = null;
            service.Category = null;
            var configuredContext = ConfigureServiceContext(context);
            var rep = new ServiceRepository(configuredContext);
            //act
            await rep.CreateAsync(service);
            await rep.EditStatusAsync(service.Id, !service.Status);
            var editedService = await rep.GetByIdAsync(service.Id);
            //assert
            editedService.Should().NotBeNull();
            editedService.Status.Should().NotBe(service.Status);

        }
        [Theory, AutoMoqData]
        public async Task GetActiveServicesByCategory_Successfuly(Service service, Category category, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            service.Specialization = null;
            service.Category = category;
            service.Status = true;
            var configuredContext = ConfigureServiceContext(context);
            var serviceRepository = new ServiceRepository(configuredContext);
            var categoryRepository = new CategoryRepository(configuredContext);
            //act
            await serviceRepository.CreateAsync(service);
            await categoryRepository.CreateAsync(category);
            var addedService = (await serviceRepository.GetActiveServicesByCategoryAsync(1, 1, category.Name)).FirstOrDefault();
            //assert
            addedService.Should().NotBeNull();
            addedService.Should().BeEquivalentTo(service);
        }

        [Theory, AutoMoqData]
        public async Task GetActiveServicesByCategory_CategoryNameNotExist_Return_EmptyQuery(Service service, Category category, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            service.Specialization = null;
            service.Category = category;
            service.Status = false;
            var configuredContext = ConfigureServiceContext(context);
            var serviceRepository = new ServiceRepository(configuredContext);
            var categoryRepository = new CategoryRepository(configuredContext);
            //act
            await serviceRepository.CreateAsync(service);
            await categoryRepository.CreateAsync(category);
            var services = await serviceRepository.GetActiveServicesByCategoryAsync(1, 1, category.Name);
            //assert
            services.Should().BeEmpty();
        }

        [Theory, AutoMoqData]
        public async Task GetActiveServicesByCategory_ServiceNotActivated_Return_EmptyQuery(Service service, string categoryName, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            service.Specialization = null;
            service.Status = false;
            var configuredContext = ConfigureServiceContext(context);
            var serviceRepository = new ServiceRepository(configuredContext);
            //act
            await serviceRepository.CreateAsync(service);
            var services = await serviceRepository.GetActiveServicesByCategoryAsync(1, 1, categoryName);
            //assert
            services.Should().BeEmpty();
        }

        [Theory, AutoMoqData]
        public async Task GetServicesBySpecializationId_Successfuly(Service service, Category category, [Frozen] Mock<ServicesContext> context)
        {
            //arrange
            service.Specialization = null;
            service.Category = category;
            service.Status = true;
            var configuredContext = ConfigureServiceContext(context);
            var serviceRepository = new ServiceRepository(configuredContext);
            var categoryRepository = new CategoryRepository(configuredContext);
            //act
            await serviceRepository.CreateAsync(service);
            await categoryRepository.CreateAsync(category);
            var addedService = (await serviceRepository.GetServicesBySpecializationIdAsync(service.SpecializationId)).FirstOrDefault();
            //assert
            addedService.Should().NotBeNull();
            addedService.Should().BeEquivalentTo(service);
        }

        private ServicesContext ConfigureServiceContext(Mock<ServicesContext> context)
        {
            var db = new InMemoryDatabase();
            db.CreateTable<Service>("Services");
            db.CreateTable<Category>("Categories");
            context.Setup(x => x.CreateConnection()).Returns(() => db.OpenConnection());
            return context.Object;
        }
    }
}
