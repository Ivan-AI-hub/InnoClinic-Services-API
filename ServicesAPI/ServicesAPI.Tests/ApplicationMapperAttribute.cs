using AutoFixture;
using AutoFixture.Community.AutoMapper;
using ServicesAPI.Application.Mappings;
using System.Reflection;

namespace ServicesAPI.Tests
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class ApplicationMapperAttribute : Attribute, IParameterCustomizationSource
    {
        public ICustomization GetCustomization(ParameterInfo parameter)
        {
            return new AutoMapperCustomization(x => x.AddProfile<ApplicationMappingProfile>());
        }
    }
}
