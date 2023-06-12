using Microsoft.AspNetCore.Mvc;
using ServicesAPI.Application.Commands.Services.ChangeStatus;
using ServicesAPI.Application.Commands.Services.Create;
using ServicesAPI.Application.Commands.Services.Edit;
using ServicesAPI.Application.Queries.Services.GetByCategory;
using ServicesAPI.Application.Queries.Services.GetById;
using ServicesAPI.Presentation.Controllers;

namespace ServicesAPI.Tests.Presentation
{
    public class ServiceControllerTest
    {
        [Theory, AutoMoqData]
        public async Task CreateService_Successfuly(ServiceController controller, CreateService model)
        {
            //act
            var result = await controller.CreateService(model);
            //assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Theory, AutoMoqData]
        public async Task GetByCategory_Successfuly(ServiceController controller, GetActiveServicesByCategory model)
        {
            //act
            var result = await controller.GetByCategory(model);
            //assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Theory, AutoMoqData]
        public async Task GetServiceInfo_Successfuly(ServiceController controller, GetServiceById model)
        {
            //act
            var result = await controller.GetServiceById(model);
            //assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Theory, AutoMoqData]
        public async Task ChangeStatus_Successfuly(ServiceController controller, ChangeServiceStatus model)
        {
            //act
            var result = await controller.ChangeStatus(model.Id, model.Status);
            //assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Theory, AutoMoqData]
        public async Task EditService_Successfuly(ServiceController controller, EditService model)
        {
            //act
            var result = await controller.EditService(model.Id, model.Name, model.Price, model.Status, model.SpecializationId, model.CategoryName);
            //assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
