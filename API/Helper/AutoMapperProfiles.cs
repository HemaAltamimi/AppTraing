using API.Dtos;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helper
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,MemberDto>()
            .ForMember(dest => dest.Age,sor => sor.MapFrom(src => src.DateOfBirth.CalculationAge()))
            .ForMember(dest => dest.PhotoUrl,sor => sor.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Photo,PhotoDto>();
        }    
    }
}