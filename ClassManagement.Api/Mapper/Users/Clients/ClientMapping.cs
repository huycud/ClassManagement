using AutoMapper;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.AppRole;
using ClassManagement.Api.DTO.Users.Clients;
using Utilities.Handlers;

///<summary>
/// Map dữ liệu cho đôi tượng Teacher và Student
/// </summary>

namespace ClassManagement.Api.Mapper.Users.Clients
{
    public class ClientMapping : Profile
    {
        public ClientMapping()
        {
            CreateMap<User, ClientResponse>()

                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => StringConverter.Convert(src.Client.Department.Name.ToLower())))

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToLocalTime()))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToLocalTime()))

                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => StringConverter.Convert(src.Client.Firstname)))

                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => StringConverter.Convert(src.Client.Lastname)))

                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => StringConverter.Convert(src.Client.Address)))

                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => Utilities.CheckImagePath(src)))

                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Client.DateOfBirth))

                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Client.Gender));

            CreateMap<CreateClientRequest, Client>()

                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => StringConverter.Convert(src.Firstname)))

                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => StringConverter.Convert(src.Lastname)))

                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId.ToUpper()));

            CreateMap<CreateClientRequest, User>()

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForMember(dest => dest.IsDisabled, opt => opt.MapFrom(src => false));

            CreateMap<UpdateClientRequest, User>()

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember?.ToString())));

            CreateMap<UpdateClientRequest, Client>()

                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => StringConverter.Convert(src.Firstname)))

                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => StringConverter.Convert(src.Lastname)))

                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => StringConverter.Convert(src.Address)))

                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember?.ToString())));
        }
    }
}
