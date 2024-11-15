using AutoMapper;
using ClassManagement.Api.Common.Converter;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Homeworks;

namespace ClassManagement.Api.Mapper.Homeworks
{
    public class HomeworkMapping : Profile
    {
        public HomeworkMapping()
        {
            CreateMap<Homework, HomeworkResponse>()

                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name.ToUpper()))

                .ForMember(dest => dest.StartedAt, opt => opt.MapFrom(src => DateHandler.UTCConverter(src.StartedAt).ToLocalTime()))

                .ForMember(dest => dest.EndedAt, opt => opt.MapFrom(src => DateHandler.UTCConverter(src.EndedAt).ToLocalTime()))

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateHandler.UTCConverter(src.CreatedAt).ToLocalTime()));

            CreateMap<CreateHomeworkRequest, Homework>()

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateHomeworkRequest, Homework>()

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember?.ToString())));
        }
    }
}
