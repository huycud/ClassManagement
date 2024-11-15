using AutoMapper;
using ClassManagement.Api.Common.Converter;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Users.Clients;
using ClassManagement.Api.DTO.Users.Manager;

///<summary>
/// Map dữ liệu cho đối tượng Admin
/// </summary>

namespace ClassManagement.Api.Mapper.Users.Manager
{
    public class AdminMapping : Profile
    {
        public AdminMapping()
        {
            CreateMap<User, AdminResponse>()

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateHandler.UTCConverter(src.CreatedAt).ToLocalTime()))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateHandler.UTCConverter(src.UpdatedAt).ToLocalTime()))

                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.Admin.Fullname))

                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => Utilities.CheckImagePath(src)));

            CreateMap<User, ClientResponse>();

            CreateMap<CreateAdminRequest, User>()

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForMember(dest => dest.IsDisabled, opt => opt.MapFrom(src => false));

            CreateMap<CreateAdminRequest, Admin>();

            CreateMap<UpdateAdminRequest, User>()

                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember?.ToString())));

            CreateMap<UpdateAdminRequest, Admin>()

                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember?.ToString())));
        }
    }
}
