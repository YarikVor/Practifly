using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;

using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.Admin.UserView;

namespace PractiFly.WebApi.AutoMapper;

public class AdminProfile : Profile
{
    public AdminProfile(IPractiflyContext _context)
    {
       
        CreateProjection<User, UserProfileForAdminViewDto>()
            .ForMember(
                up => up.Name, par => par.MapFrom(
                    e => e.FirstName))
            .ForMember(
                up => up.Surname, par => par.MapFrom(
                    e => e.LastName))
            .ForMember(
                up => up.Role, par => par.MapFrom(
                    e => _context
                        .UserRoles
                        .Where(ur => ur.UserId == e.Id)
                        .Select(ur => ur.Role.Name)
                        .FirstOrDefault()));
        
        
        CreateMap<UserProfileForAdminCreateDto, User>()
            .ForMember(
                user => user.RegistrationDate,
                par => par.MapFrom(
                    dto => DateOnly.FromDateTime(DateTime.Today)));

        CreateProjection<User, UserFullnameItemDto>()
            .ForMember(dto => dto.Fullname, par => par.MapFrom(
                e => string.Concat(e.FirstName, " ", e.LastName)));
    }
}
