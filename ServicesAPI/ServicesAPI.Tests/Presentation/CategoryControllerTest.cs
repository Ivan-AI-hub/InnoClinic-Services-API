using Microsoft.AspNetCore.Mvc;
using ServicesAPI.Application.Commands.Categories.Create;
using ServicesAPI.Presentation.Controllers;

namespace ServicesAPI.Tests.Presentation
{
    public class CategoryControllerTest
    {
        [Theory, AutoMoqData]
        public async Task CreateCategory_Successfuly(CategoryController controller, CreateCategory createCategory)
        {
            //act
            var result = await controller.CreateCategory(createCategory);
            //assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
