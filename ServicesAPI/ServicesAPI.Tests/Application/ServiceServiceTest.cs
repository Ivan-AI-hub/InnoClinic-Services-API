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
               [Frozen] Mock<IServiceRepository> serviceRepository,
               [Frozen] Mock<ICategoryRepository> categoryRepository,
               [Frozen] Mock<IPublishEndpoint> publishEndpoint,
               [ApplicationMapper][Frozen] IMapper mapper)
        {
            var handler = new CreateServiceHandler(serviceRepository.Object, categoryRepository.Object, mapper, publishEndpoint.Object);

            await handler.Handle(createService, default);
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
        public async Task ChangeStatus_Successfuly(ChangeServiceStatus changeServiceStatus,
            [Frozen] Mock<IServiceRepository> serviceRepository)
        {
            //arrange
            serviceRepository.Setup(x => x.IsServiceExist(changeServiceStatus.Id)).Returns(true);
            var handler = new ChangeServiceStatusHandler(serviceRepository.Object);

            //act
            await handler.Handle(changeServiceStatus, default);
        }

        [Theory, AutoMoqData]
        public async Task ChangeStatus_Throw_ServiceNotFoundException(ChangeServiceStatus changeServiceStatus,
            [Frozen] Mock<IServiceRepository> serviceRepository)
        {
            serviceRepository.Setup(x => x.IsServiceExist(changeServiceStatus.Id)).Returns(false);
            var handler = new ChangeServiceStatusHandler(serviceRepository.Object);

            var act = () => handler.Handle(changeServiceStatus, default);

            await Assert.ThrowsAsync<ServiceNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task Edit_Successfuly(EditService editService,
                [Frozen] Mock<IServiceRepository> serviceRepository,
                [Frozen] Mock<ICategoryRepository> categoryRepository,
                [Frozen] Mock<IPublishEndpoint> publishEndpoint,
                [ApplicationMapper][Frozen] IMapper mapper)
        {
            serviceRepository.Setup(x => x.IsServiceExist(editService.Id)).Returns(true);
            var handler = new EditServiceHandler(serviceRepository.Object, categoryRepository.Object, publishEndpoint.Object, mapper);

            await handler.Handle(editService, default);
        }

        [Theory, AutoMoqData]
        public async Task Edit_Throw_ServiceNotFoundException(EditService editService,
                [Frozen] Mock<IServiceRepository> serviceRepository,
                [Frozen] Mock<ICategoryRepository> categoryRepository,
                [Frozen] Mock<IPublishEndpoint> publishEndpoint,
                [ApplicationMapper][Frozen] IMapper mapper)
        {
            serviceRepository.Setup(x => x.IsServiceExist(editService.Id)).Returns(false);
            var handler = new EditServiceHandler(serviceRepository.Object, categoryRepository.Object, publishEndpoint.Object, mapper);

            var act = () => handler.Handle(editService, default);

            await Assert.ThrowsAsync<ServiceNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task Edit_Throw_CategoryNotFoundException(EditService editService,
                [Frozen] Mock<IServiceRepository> serviceRepository,
                [Frozen] Mock<ICategoryRepository> categoryRepository,
                [Frozen] Mock<IPublishEndpoint> publishEndpoint,
                [ApplicationMapper][Frozen] IMapper mapper)
        {
            serviceRepository.Setup(x => x.IsServiceExist(editService.Id)).Returns(true);
            categoryRepository.Setup(x => x.GetByNameAsync(editService.CategoryName, It.IsAny<CancellationToken>()))
                  .Returns(Task.FromResult<Category>(null));
            var handler = new EditServiceHandler(serviceRepository.Object, categoryRepository.Object, publishEndpoint.Object, mapper);

            var act = () => handler.Handle(editService, default);

            await Assert.ThrowsAsync<CategoryNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task GetById_Successfuly(GetServiceById getById,
                [Frozen] Mock<IServiceRepository> serviceRepository,
                [Frozen] Mock<ICategoryRepository> categoryRepository,
                [Frozen] Mock<ISpecializationRepository> specializationRepository)
        {
            var handler = new GetServiceByIdHandler(serviceRepository.Object, categoryRepository.Object, specializationRepository.Object);

            var service = await handler.Handle(getById, default);

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
            serviceRepository.Setup(x => x.GetByIdAsync(getById.Id, It.IsAny<CancellationToken>())).Returns(Task.FromResult<Service>(null));
            var handler = new GetServiceByIdHandler(serviceRepository.Object, categoryRepository.Object, specializationRepository.Object);

            var act = () => handler.Handle(getById, default);

            await Assert.ThrowsAsync<ServiceNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task GetActiveServicesByCategory_Successfuly(GetActiveServicesByCategory getActiveServicesByCategory,
                [Frozen] Mock<IServiceRepository> serviceRepository)
        {
            var handler = new GetActiveServicesByCategoryHandler(serviceRepository.Object);

            var services = await handler.Handle(getActiveServicesByCategory, default);

            services.Should().NotBeNull();
        }
    }
}
