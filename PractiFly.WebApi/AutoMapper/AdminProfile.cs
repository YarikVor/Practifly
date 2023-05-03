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
                   e => e.LastName));
        //Role?

        CreateProjection<User, UserProfileForAdminCreateDto>()
            .ForMember(
                up => up.Name, par => par.MapFrom(
                    e => _context
                        .Users
                        .Select(u => u.FirstName)))
            .ForMember(
                up => up.Surname, par => par.MapFrom(
                    e => _context
                        .Users
                        .Select(u => u.LastName)))
            //.ForMember(
            //    up => up.RegistrationDate, par => par.MapFrom(
            //        dto => DateOnly.FromDateTime(DateTime.Today)))
        ;
       
    }
}
