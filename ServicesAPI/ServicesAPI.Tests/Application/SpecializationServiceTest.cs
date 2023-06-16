using MassTransit;
using ServicesAPI.Application.Commands.Specializations.ChangeStatus;
using ServicesAPI.Application.Commands.Specializations.Create;
using ServicesAPI.Application.Commands.Specializations.Edit;
using ServicesAPI.Application.Queries.Specializations.GetById;
using ServicesAPI.Application.Queries.Specializations.GetInfo;
using ServicesAPI.Domain.Exceptions;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Tests.Application
{
    public class SpecializationServiceTest
    {
        [Theory, AutoMoqData]
        public async Task CreateSpecialization_Successfuly(CreateSpecialization createSpecialization,
               [Frozen] Mock<ServicesContext> context,
               [Frozen] Mock<IPublishEndpoint> publishEndpoint,
               [ApplicationMapper][Frozen] IMapper mapper)
        {
            //arrange 
            var specializationRepository = GetSpecializationRepository(context);
            var createHandler = new CreateSpecializationHandler(specializationRepository, mapper, publishEndpoint.Object);

            //act
            var createSpecializationResult = await createHandler.Handle(createSpecialization, default);
            var createdSpecialization = await specializationRepository.GetByIdAsync(createSpecializationResult.Id);

            //assert
            createdSpecialization.Should().BeEquivalentTo(createSpecializationResult);
        }

        [Theory, AutoMoqData]
        public async Task ChangeStatus_Successfuly(Specialization specialization,
            [Frozen] Mock<ServicesContext> context)
        {
            //arrange 
            var specializationRepository = GetSpecializationRepository(context);
            var changeStatusHandler = new ChangeSpecializationStatusHandler(specializationRepository);
            var changeStatusModel = new ChangeSpecializationStatus(specialization.Id, !specialization.IsActive);

            //act
            await specializationRepository.CreateAsync(specialization);
            await changeStatusHandler.Handle(changeStatusModel, default);
            var editedSpecialization = await specializationRepository.GetByIdAsync(specialization.Id);

            //assert
            editedSpecialization.IsActive.Should().NotBe(specialization.IsActive);
        }

        [Theory, AutoMoqData]
        public async Task ChangeStatus_Throw_SpecializationNotFoundException(ChangeSpecializationStatus changeSpecializationStatus,
            [Frozen] Mock<ISpecializationRepository> specializationRepository)
        {
            //arrange
            specializationRepository.Setup(x => x.IsSpecializationExist(changeSpecializationStatus.Id)).Returns(false);
            var handler = new ChangeSpecializationStatusHandler(specializationRepository.Object);
            
            //act
            var act = () => handler.Handle(changeSpecializationStatus, default);

            //assert
            await Assert.ThrowsAsync<SpecializationNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task Edit_Successfuly(Specialization specialization,
                [Frozen] Mock<ServicesContext> context,
                [Frozen] Mock<IPublishEndpoint> publishEndpoint,
                [ApplicationMapper][Frozen] IMapper mapper)
        {
            //arrange
            var specializationRepository = GetSpecializationRepository(context);
            var editHandler = new EditSpecializationHandler(specializationRepository, mapper, publishEndpoint.Object);

            var editModel = new EditSpecialization(specialization.Id, Guid.NewGuid().ToString(), !specialization.IsActive);

            //act
            await specializationRepository.CreateAsync(specialization);
            await editHandler.Handle(editModel, default);
            var editedSpecialization = await specializationRepository.GetByIdAsync(specialization.Id);

            //assert
            editedSpecialization.Should().NotBeEquivalentTo(specialization);
        }

        [Theory, AutoMoqData]
        public async Task Edit_Throw_SpecializationNotFoundException(EditSpecialization editSpecialization,
                [Frozen] Mock<ISpecializationRepository> specializationRepository,
                [Frozen] Mock<IPublishEndpoint> publishEndpoint,
                [ApplicationMapper][Frozen] IMapper mapper)
        {
            //arrange
            specializationRepository.Setup(x => x.IsSpecializationExist(editSpecialization.Id)).Returns(false);
            var handler = new EditSpecializationHandler(specializationRepository.Object, mapper, publishEndpoint.Object);

            //act
            var act = () => handler.Handle(editSpecialization, default);

            //assert
            await Assert.ThrowsAsync<SpecializationNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task GetById_Successfuly(GetSpecializationById getById,
                [Frozen] Mock<ISpecializationRepository> specializationRepository,
                [Frozen] Mock<IServiceRepository> serviceRepository)
        {
            //arrange
            var handler = new GetSpecializationByIdHandler(specializationRepository.Object, serviceRepository.Object);

            //act
            var specialization = await handler.Handle(getById, default);

            //assert
            specialization.Should().NotBeNull();
            specialization.Services.Should().NotBeNull();
        }

        [Theory, AutoMoqData]
        public async Task GetById_Throw_SpecializationNotFoundException(GetSpecializationById getById,
        [Frozen] Mock<ISpecializationRepository> specializationRepository,
        [Frozen] Mock<IServiceRepository> serviceRepository)
        {
            //arrange
            specializationRepository.Setup(x => x.GetByIdAsync(getById.Id, It.IsAny<CancellationToken>())).Returns(Task.FromResult<Specialization>(null));
            var handler = new GetSpecializationByIdHandler(specializationRepository.Object, serviceRepository.Object);

            //act
            var act = () => handler.Handle(getById, default);

            //assert
            await Assert.ThrowsAsync<SpecializationNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task GetSpecializationsInfo_Successfuly(GetSpecializationsInfo getSpecializationsInfo,
                [Frozen] Mock<ISpecializationRepository> specializationRepository)
        {
            //arrange
            var handler = new GetSpecializationsInfoHandler(specializationRepository.Object);

            //act
            var specializations = await handler.Handle(getSpecializationsInfo, default);

            //assert
            specializations.Should().NotBeNull();
        }

        private ISpecializationRepository GetSpecializationRepository(Mock<ServicesContext> context)
        {
            var db = new InMemoryDatabase();
            db.CreateTable<Specialization>("Specializations");
            context.Setup(x => x.CreateConnection()).Returns(() => db.OpenConnection());
            return new SpecializationRepository(context.Object);
        }
    }
}

