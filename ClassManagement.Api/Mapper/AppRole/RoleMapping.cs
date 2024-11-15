using AutoMapper;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.AppRole;

namespace ClassManagement.Api.Mapper.AppRole
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<Role, RoleResponse>();

            CreateMap<CreateRoleRequest, Role>()

                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Utilities.DescWithRole(src)))

                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToUpper()));

            CreateMap<UpdateRoleRequest, Role>()

                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Utilities.DescWithRole(src)))

                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToUpper()))

                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember?.ToString())));
        }
    }
}
