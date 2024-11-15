using AutoMapper;
using ClassManagement.Api.Common.Converter;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Department;
using Utilities.Handlers;

namespace ClassManagement.Api.Mapper.Departments
{
    public class DepartmentMapping : Profile
    {
        public DepartmentMapping()
        {
            CreateMap<Department, DepartmentResponse>()

                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => StringConverter.Convert(src.Name.ToLower())))

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToLocalTime()))

                //.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateHandler.UTCConverter(src.CreatedAt).ToLocalTime()))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateHandler.UTCConverter(src.UpdatedAt).ToLocalTime()));

            CreateMap<CreateDepartmentRequest, Department>()

                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToUpper()))

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateDepartmentRequest, Department>()

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember?.ToString())));
        }
    }
}
