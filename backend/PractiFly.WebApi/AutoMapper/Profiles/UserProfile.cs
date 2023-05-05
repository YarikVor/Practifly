using AutoMapper;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.Registration;

namespace PractiFly.WebApi.AutoMapper.Profiles;

public class UserProfile : Profile
{
    private const string DefaultPhotoUrl = "https://www.nicepng.com/maxp/u2y3a9e6t4o0a9w7/";

    public UserProfile()
    {
        CreateMap<RegistrationDto, User>()
            .ForMember(user => user.UserName, par => par.MapFrom(dto => dto.Username))
            .ForMember(user => user.FirstName, par => par.MapFrom(dto => dto.Name))
            .ForMember(user => user.LastName, par => par.MapFrom(dto => dto.Surname))
            .ForMember(user => user.PhoneNumber, par => par.MapFrom(dto => dto.Phone))
            .ForMember(user => user.RegistrationDate, par => par.MapFrom(dto => DateOnly.FromDateTime(DateTime.Today)))
            .ForMember(user => user.FilePhoto, par => par.MapFrom(dto => DefaultPhotoUrl));
        //TODO:
        CreateMap<User, UserTokenInfoDto>()
            .ForMember(dto => dto.User, par => par.MapFrom(
                e => e));
    }
}