using AutoMapper;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Class;
using Utilities.Handlers;

namespace ClassManagement.Api.Mapper.Classes
{
    public class ClassMapping : Profile
    {
        public ClassMapping()
        {
            CreateMap<Class, ClassResponse>()

                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => StringConverter.Convert(src.Name.ToLower())))

                .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => StringConverter.Convert(src.Semester.Name.ToLower())))

                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => StringConverter.Convert(src.Subject.Name.ToLower())))

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToLocalTime()))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToLocalTime()))

                .ForMember(dest => dest.StartedAt, opt => opt.MapFrom(src => src.StartedAt.ToLocalTime()))

                .ForMember(dest => dest.EndedAt, opt => opt.MapFrom(src => src.EndedAt.ToLocalTime()));

            CreateMap<CreateClassRequest, Class>()

                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToUpper()))

                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))

                .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => src.SubjectId.ToUpper()))

                .ForMember(dest => dest.SemesterId, opt => opt.MapFrom(src => src.SemesterId.ToUpper()))

                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => 0))

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForMember(dest => dest.ClassPeriods, opt => opt.MapFrom(src => string.Join(",", src.ClassPeriods.Select(x => x.ToString()))));

            CreateMap<UpdateClassRequest, Class>()

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember?.ToString())));

        }
    }
}
