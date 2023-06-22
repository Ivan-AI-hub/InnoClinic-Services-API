using AutoMapper;
using ServicesAPI.Application.Commands.Categories.Create;
using ServicesAPI.Application.Commands.Services.Create;
using ServicesAPI.Application.Commands.Services.Edit;
using ServicesAPI.Application.Commands.Specializations.Create;
using ServicesAPI.Application.Commands.Specializations.Edit;
using ServicesAPI.Domain;

namespace ServicesAPI.Application.Mappings
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<CreateCategory, Category>();

            CreateMap<CreateSpecialization, Specialization>();
            CreateMap<EditSpecialization, Specialization>();

            CreateMap<CreateService, Service>();
            CreateMap<EditService, Service>();
        }
    }
}
