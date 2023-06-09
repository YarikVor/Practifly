using AutoMapper;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.Profile;
using PractiFly.WebApi.Dto.Registration;

namespace PractiFly.WebApi.AutoMapper.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegistrationDto, User>()
            .ForMember(user => user.RegistrationDate, par => par.MapFrom(dto => DateOnly.FromDateTime(DateTime.Today)))
            //.ForMember(user => user.FilePhoto, par => par.MapFrom(dto => DefaultPhotoUrl));
            ;

        CreateMap<User, UserProfileInfoViewDto>()
            .ForMember(dto => dto.FilePhoto, par => par.MapFrom(
                (user, _, _, opt) => (string)opt.Items["baseUrl"] + (user.IsCustomPhoto ? user.Id : 0)));
        CreateMap<User, UserTokenInfoDto>()
            .ForMember(dto => dto.User, par => par.MapFrom(e => e));
    }
}