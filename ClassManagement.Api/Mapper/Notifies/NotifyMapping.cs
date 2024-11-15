using AutoMapper;
using ClassManagement.Api.Common.Converter;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Notifies;
using Utilities.Handlers;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Mapper.Notifies
{
    public class NotifyMapping : Profile
    {
        public NotifyMapping()
        {
            CreateMap<Notify, NotifyResponse>()

                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => StringConverter.Convert(src.Title.ToLower(), StringMode.UpperFirstCharInSentence)))

                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => StringConverter.Convert(src.Content.ToLower(), StringMode.UpperFirstCharInSentence)))

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateHandler.UTCConverter(src.CreatedAt).ToLocalTime()))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateHandler.UTCConverter(src.UpdatedAt).ToLocalTime()));

            CreateMap<CreateNotifyRequest, Notify>()

                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Utilities.ConvertIdString(src.Title)))

                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateNotifyRequest, Notify>()

                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember?.ToString())));
        }
    }
}
