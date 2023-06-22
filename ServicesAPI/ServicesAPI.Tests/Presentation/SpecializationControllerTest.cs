using Microsoft.AspNetCore.Mvc;
using ServicesAPI.Application.Commands.Specializations.ChangeStatus;
using ServicesAPI.Application.Commands.Specializations.Create;
using ServicesAPI.Application.Commands.Specializations.Edit;
using ServicesAPI.Application.Queries.Specializations.GetInfo;
using ServicesAPI.Presentation.Controllers;

namespace ServicesAPI.Tests.Presentation
{
    public class SpecializationControllerTest
    {
        [Theory, AutoMoqData]
        public async Task CreateSpecialization_Successfuly(SpecializationController controller, CreateSpecialization model)
        {
            //act
            var result = await controller.CreateSpecialization(model);
            //assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Theory, AutoMoqData]
        public async Task GetSpecializationsInfo_Successfuly(SpecializationController controller, GetSpecializationsInfo model)
        {
            //act
            var result = await controller.GetSpecializationsInfo(model);
            //assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Theory, AutoMoqData]
        public async Task ChangeStatus_Successfuly(SpecializationController controller, ChangeSpecializationStatus model)
        {
            //act
            var result = await controller.ChangeStatus(model.Id, model.IsActive);
            //assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Theory, AutoMoqData]
        public async Task EditSpecialization_Successfuly(SpecializationController controller, EditSpecialization model)
        {
            //act
            var result = await controller.EditSpecialization(model.Id, model.Name, model.IsActive);
            //assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
