using AutoMapper;
using ClassManagement.Api.Common.Converter;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Semester;
using Utilities.Handlers;

namespace ClassManagement.Api.Mapper.Semesters
{
    public class SemesterMapping : Profile
    {
        public SemesterMapping()
        {
            CreateMap<Semester, SemesterResponse>()

                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => StringConverter.Convert(src.Name.ToLower())))

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToLocalTime()))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateHandler.UTCConverter(src.UpdatedAt).ToLocalTime()));

            CreateMap<CreateSemesterRequest, Semester>()

                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToUpper()))

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateSemesterRequest, Semester>()

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember?.ToString())));
        }
    }
}
