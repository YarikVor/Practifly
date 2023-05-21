using AutoMapper;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.Profile;
using PractiFly.WebApi.Dto.Registration;

namespace PractiFly.WebApi.AutoMapper.Profiles;

public class UserProfile : Profile
{
    private const string DefaultPhotoUrl = "https://www.nicepng.com/maxp/u2y3a9e6t4o0a9w7/";

    public UserProfile()
    {
        CreateMap<RegistrationDto, User>()
            .ForMember(user => user.RegistrationDate, par => par.MapFrom(dto => DateOnly.FromDateTime(DateTime.Today)))
            //.ForMember(user => user.FilePhoto, par => par.MapFrom(dto => DefaultPhotoUrl));
            ;
        
        string baseUrl = null!;
        CreateMap<User, UserProfileInfoViewDto>()
            .ForMember(dto => dto.FilePhoto, par => par.MapFrom(
                (user, _, _, opt) => (string)opt.Items["baseUrl"] + (user.IsDefaultPhoto ? 0 : user.Id).ToString()));
        CreateMap<User, UserTokenInfoDto>()
            .ForMember(dto => dto.User, par => par.MapFrom(e => e));
    }
}