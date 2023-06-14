using AutoFixture;
using MassTransit;
using ServicesAPI.Application.Commands.Services.ChangeStatus;
using ServicesAPI.Application.Commands.Services.Create;
using ServicesAPI.Application.Commands.Services.Edit;
using ServicesAPI.Application.Queries.Services.GetByCategory;
using ServicesAPI.Application.Queries.Services.GetById;
using ServicesAPI.Domain.Exceptions;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Tests.Application
{
    public class ServiceServiceTest
    {
        [Theory, AutoMoqData]
        public async Task CreateService_Successfuly(CreateService createService,
               Mock<ServicesContext> context,
               [Frozen] Mock<IPublishEndpoint> publishEndpoint,
               [ApplicationMapper][Frozen] IMapper mapper)
        {
            //arrange
            var configuredContext = ConfigureServiceContext(context);
            var serviceRepository = new ServiceRepository(configuredContext);
            var categoryRepository = new CategoryRepository(configuredContext);
            var specializationRepository = new SpecializationRepository(configuredContext);

            var createHandler = new CreateServiceHandler(serviceRepository, categoryRepository, mapper, publishEndpoint.Object);
            var getByIdHandler = new GetServiceByIdHandler(serviceRepository, categoryRepository, specializationRepository);

            var serviceCategory = new Category(createService.CategoryName, 20);

            //act
            await categoryRepository.CreateAsync(serviceCategory);
            var createServiceResult = await createHandler.Handle(createService, default);
            var createdService = await getByIdHandler.Handle(new GetServiceById(createServiceResult.Id), default);

            //assert
            createdService.Should().NotBeNull();
            createdService.Should().BeEquivalentTo(createServiceResult);
        }

        [Theory, AutoMoqData]
        public async Task CreateService_Throw_CategoryNotFoundException(CreateService createService,
                [Frozen] Mock<IServiceRepository> serviceRepository,
                [Frozen] Mock<ICategoryRepository> categoryRepository,
                [Frozen] Mock<IPublishEndpoint> publishEndpoint,
                [ApplicationMapper][Frozen] IMapper mapper)
        {
            //arrange
            categoryRepository.Setup(x => x.GetByNameAsync(createService.CategoryName, It.IsAny<CancellationToken>()))
                              .Returns(Task.FromResult<Category>(null));

            var handler = new CreateServiceHandler(serviceRepository.Object, categoryRepository.Object, mapper, publishEndpoint.Object);

            //act
            var act = () => handler.Handle(createService, default);

            //assert
            await Assert.ThrowsAsync<CategoryNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task ChangeStatus_Successfuly(Service service,
            Mock<ServicesContext> context,
            [Frozen] Mock<ICategoryRepository> categoryRepository,
            [Frozen] Mock<ISpecializationRepository> specializationRepository)
        {
            //arrange
            var configuredContext = ConfigureServiceContext(context);
            var serviceRepository = new ServiceRepository(configuredContext);

            var changeStatusHandler = new ChangeServiceStatusHandler(serviceRepository);
            var getByIdHandler = new GetServiceByIdHandler(serviceRepository, categoryRepository.Object, specializationRepository.Object);

            var changeStatusModel = new ChangeServiceStatus(service.Id, !service.Status);
            var getServiceByIdModel = new GetServiceById(service.Id);

            //act
            await serviceRepository.CreateAsync(service, default);
            await changeStatusHandler.Handle(changeStatusModel, default);
            var changedService = await getByIdHandler.Handle(getServiceByIdModel, default);

            //assert 
            changedService.Status.Should().NotBe(service.Status);
        }

        [Theory, AutoMoqData]
        public async Task ChangeStatus_Throw_ServiceNotFoundException(ChangeServiceStatus changeServiceStatus,
            [Frozen] Mock<IServiceRepository> serviceRepository)
        {
            //arrange 
            serviceRepository.Setup(x => x.IsServiceExist(changeServiceStatus.Id)).Returns(false);
            var handler = new ChangeServiceStatusHandler(serviceRepository.Object);

            //act
            var act = () => handler.Handle(changeServiceStatus, default);

            //assert
            await Assert.ThrowsAsync<ServiceNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task Edit_Successfuly(Service service, 
                Specialization specialization, 
                Category category,
                Mock<ServicesContext> context,
                [Frozen] Mock<IPublishEndpoint> publishEndpoint,
                [ApplicationMapper][Frozen] IMapper mapper)
        {
            //arrange
            var configuredContext = ConfigureServiceContext(context);
            var serviceRepository = new ServiceRepository(configuredContext);
            var categoryRepository = new CategoryRepository(configuredContext);
            var specializationRepository = new SpecializationRepository(configuredContext);

            var editHandler = new EditServiceHandler(serviceRepository, categoryRepository, publishEndpoint.Object, mapper);
            var getByIdHandler = new GetServiceByIdHandler(serviceRepository, categoryRepository, specializationRepository);

            var fixture = new Fixture();
            var editServiceModel = new EditService(service.Id, fixture.Create<string>(), fixture.Create<int>(),
                                                    fixture.Create<bool>(), specialization.Id, category.Name);
            var getServiceByIdModel = new GetServiceById(service.Id);

            //act
            await categoryRepository.CreateAsync(category);
            await specializationRepository.CreateAsync(specialization);
            await serviceRepository.CreateAsync(service);
            await editHandler.Handle(editServiceModel, default);
            var editedService = await getByIdHandler.Handle(getServiceByIdModel, default);

            //assert
            editedService.Should().NotBeEquivalentTo(service);
        }

        [Theory, AutoMoqData]
        public async Task Edit_Throw_ServiceNotFoundException(EditService editService,
                [Frozen] Mock<IServiceRepository> serviceRepository,
                [Frozen] Mock<ICategoryRepository> categoryRepository,
                [Frozen] Mock<IPublishEndpoint> publishEndpoint,
                [ApplicationMapper][Frozen] IMapper mapper)
        {
            //arrange 
            serviceRepository.Setup(x => x.IsServiceExist(editService.Id)).Returns(false);
            var handler = new EditServiceHandler(serviceRepository.Object, categoryRepository.Object, publishEndpoint.Object, mapper);

            //act
            var act = () => handler.Handle(editService, default);

            //assert
            await Assert.ThrowsAsync<ServiceNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task Edit_Throw_CategoryNotFoundException(EditService editService,
                [Frozen] Mock<IServiceRepository> serviceRepository,
                [Frozen] Mock<ICategoryRepository> categoryRepository,
                [Frozen] Mock<IPublishEndpoint> publishEndpoint,
                [ApplicationMapper][Frozen] IMapper mapper)
        {
            //arrange 
            serviceRepository.Setup(x => x.IsServiceExist(editService.Id)).Returns(true);
            categoryRepository.Setup(x => x.GetByNameAsync(editService.CategoryName, It.IsAny<CancellationToken>()))
                  .Returns(Task.FromResult<Category>(null));
            var handler = new EditServiceHandler(serviceRepository.Object, categoryRepository.Object, publishEndpoint.Object, mapper);

            //act
            var act = () => handler.Handle(editService, default);

            //assert
            await Assert.ThrowsAsync<CategoryNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task GetById_Successfuly(GetServiceById getById,
                [Frozen] Mock<IServiceRepository> serviceRepository,
                [Frozen] Mock<ICategoryRepository> categoryRepository,
                [Frozen] Mock<ISpecializationRepository> specializationRepository)
        {
            //arrange 
            var handler = new GetServiceByIdHandler(serviceRepository.Object, categoryRepository.Object, specializationRepository.Object);

            //act
            var service = await handler.Handle(getById, default);

            //assert
            service.Should().NotBeNull();
            service.Specialization.Should().NotBeNull();
            service.Category.Should().NotBeNull();
        }

        [Theory, AutoMoqData]
        public async Task GetById_Throw_ServiceNotFoundException(GetServiceById getById,
        [Frozen] Mock<IServiceRepository> serviceRepository,
        [Frozen] Mock<ICategoryRepository> categoryRepository,
        [Frozen] Mock<ISpecializationRepository> specializationRepository)
        {
            //arrange 
            serviceRepository.Setup(x => x.GetByIdAsync(getById.Id, It.IsAny<CancellationToken>())).Returns(Task.FromResult<Service>(null));
            var handler = new GetServiceByIdHandler(serviceRepository.Object, categoryRepository.Object, specializationRepository.Object);

            //act
            var act = () => handler.Handle(getById, default);

            //assert
            await Assert.ThrowsAsync<ServiceNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task GetActiveServicesByCategory_Successfuly(GetActiveServicesByCategory getActiveServicesByCategory,
                [Frozen] Mock<IServiceRepository> serviceRepository)
        {
            //arrange 
            var handler = new GetActiveServicesByCategoryHandler(serviceRepository.Object);

            //act
            var services = await handler.Handle(getActiveServicesByCategory, default);

            //assert
            services.Should().NotBeNull();
        }

        private ServicesContext ConfigureServiceContext(Mock<ServicesContext> context)
        {
            var db = new InMemoryDatabase();
            db.CreateTable<Service>("Services");
            db.CreateTable<Category>("Categories");
            db.CreateTable<Specialization>("Specializations");
            context.Setup(x => x.CreateConnection()).Returns(() => db.OpenConnection());
            return context.Object;
        }
    }
}
