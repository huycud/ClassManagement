using AutoMapper;
using ClassManagement.Api.Common.Converter;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Subject;
using Utilities.Handlers;

namespace ClassManagement.Api.Mapper.Subjects
{
    public class SubjectMapping : Profile
    {
        public SubjectMapping()
        {
            CreateMap<Subject, SubjectResponse>()

                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => StringConverter.Convert(src.Name.ToLower())))

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateHandler.UTCConverter(src.CreatedAt).ToLocalTime()))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateHandler.UTCConverter(src.UpdatedAt).ToLocalTime()));

            CreateMap<CreateSubjectRequest, Subject>()

                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToUpper()))

                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId.ToUpper()))

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateSubjectRequest, Subject>()

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember?.ToString())));
        }
    }
}
