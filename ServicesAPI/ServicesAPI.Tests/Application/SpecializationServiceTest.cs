using FluentAssertions;
using MassTransit;
using ServicesAPI.Application.Commands.Specializations.ChangeStatus;
using ServicesAPI.Application.Commands.Specializations.Create;
using ServicesAPI.Application.Commands.Specializations.Edit;
using ServicesAPI.Application.Queries.Specializations.GetById;
using ServicesAPI.Application.Queries.Specializations.GetInfo;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Exceptions;
using ServicesAPI.Domain.Interfaces;
using ServicesAPI.Tests;

namespace ServicesAPI.Tests.Application
{
    public class SpecializationServiceTest
    {
        [Theory, AutoMoqData]
        public async Task CreateSpecialization_Successfuly(CreateSpecialization createSpecialization,
               [Frozen] Mock<ISpecializationRepository> specializationRepository,
               [Frozen] Mock<IPublishEndpoint> publishEndpoint,
               [ApplicationMapper][Frozen] IMapper mapper)
        {
            var handler = new CreateSpecializationHandler(specializationRepository.Object, mapper, publishEndpoint.Object);

            await handler.Handle(createSpecialization, default);
        }

        [Theory, AutoMoqData]
        public async Task ChangeStatus_Successfuly(ChangeSpecializationStatus changeSpecializationStatus,
            [Frozen] Mock<ISpecializationRepository> specializationRepository)
        {
            specializationRepository.Setup(x => x.IsSpecializationExist(changeSpecializationStatus.Id)).Returns(true);
            var handler = new ChangeSpecializationStatusHandler(specializationRepository.Object);

            await handler.Handle(changeSpecializationStatus, default);
        }

        [Theory, AutoMoqData]
        public async Task ChangeStatus_Throw_SpecializationNotFoundException(ChangeSpecializationStatus changeSpecializationStatus,
            [Frozen] Mock<ISpecializationRepository> specializationRepository)
        {
            specializationRepository.Setup(x => x.IsSpecializationExist(changeSpecializationStatus.Id)).Returns(false);
            var handler = new ChangeSpecializationStatusHandler(specializationRepository.Object);

            var act = () => handler.Handle(changeSpecializationStatus, default);

            await Assert.ThrowsAsync<SpecializationNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task Edit_Successfuly(EditSpecialization editSpecialization,
                [Frozen] Mock<ISpecializationRepository> specializationRepository,
                [Frozen] Mock<IPublishEndpoint> publishEndpoint,
                [ApplicationMapper][Frozen] IMapper mapper)
        {
            specializationRepository.Setup(x => x.IsSpecializationExist(editSpecialization.Id)).Returns(true);
            var handler = new EditSpecializationHandler(specializationRepository.Object, mapper, publishEndpoint.Object);

            await handler.Handle(editSpecialization, default);
        }

        [Theory, AutoMoqData]
        public async Task Edit_Throw_SpecializationNotFoundException(EditSpecialization editSpecialization,
                [Frozen] Mock<ISpecializationRepository> specializationRepository,
                [Frozen] Mock<IPublishEndpoint> publishEndpoint,
                [ApplicationMapper][Frozen] IMapper mapper)
        {
            specializationRepository.Setup(x => x.IsSpecializationExist(editSpecialization.Id)).Returns(false);
            var handler = new EditSpecializationHandler(specializationRepository.Object, mapper, publishEndpoint.Object);

            var act = () => handler.Handle(editSpecialization, default);

            await Assert.ThrowsAsync<SpecializationNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task GetById_Successfuly(GetSpecializationById getById,
                [Frozen] Mock<ISpecializationRepository> specializationRepository,
                [Frozen] Mock<IServiceRepository> serviceRepository)
        {
            var handler = new GetSpecializationByIdHandler(specializationRepository.Object, serviceRepository.Object);

            var specialization = await handler.Handle(getById, default);

            specialization.Should().NotBeNull();
            specialization.Services.Should().NotBeNull();
        }

        [Theory, AutoMoqData]
        public async Task GetById_Throw_SpecializationNotFoundException(GetSpecializationById getById,
        [Frozen] Mock<ISpecializationRepository> specializationRepository,
        [Frozen] Mock<IServiceRepository> serviceRepository)
        {
            specializationRepository.Setup(x => x.GetByIdAsync(getById.Id, It.IsAny<CancellationToken>())).Returns(Task.FromResult<Specialization>(null));
            var handler = new GetSpecializationByIdHandler(specializationRepository.Object, serviceRepository.Object);

            var act = () => handler.Handle(getById, default);

            await Assert.ThrowsAsync<SpecializationNotFoundException>(act);
        }

        [Theory, AutoMoqData]
        public async Task GetSpecializationsInfo_Successfuly(GetSpecializationsInfo getSpecializationsInfo,
                [Frozen] Mock<ISpecializationRepository> specializationRepository)
        {
            var handler = new GetSpecializationsInfoHandler(specializationRepository.Object);

            var specializations = await handler.Handle(getSpecializationsInfo, default);

            specializations.Should().NotBeNull();
        }
    }
}

