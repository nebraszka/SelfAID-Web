using AutoMapper;
using SelfAID.CommonLib.Dtos.Emotion;
using SelfAID.CommonLib.Models;

namespace SelfAID.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Emotion, GetEmotionDto>();
            CreateMap<AddEmotionDto, Emotion>();
        }
    }
}