using AutoMapper;
using BuildingHealth.Core.Models;
using BuildingHealth.Core.ViewModels;

namespace BuildingHealth.Core.AutoMapper
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            //source mapping to destination
            CreateMap<NotificationViewModel, Notification>().ForMember(des => des.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => src.CreateTime)).ReverseMap();

            CreateMap<EditUserModel, User>().ForMember(des => des.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(des => des.SecondName, opt => opt.MapFrom(src => src.SecondName))
                .ForMember(des => des.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(des => des.Phone, opt => opt.MapFrom(src => src.Phone)).ReverseMap();
        }
    }
}
