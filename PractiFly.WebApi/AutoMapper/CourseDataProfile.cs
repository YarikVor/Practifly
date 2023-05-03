using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.Admin.UserView;
using PractiFly.WebApi.Dto.CourseData;

namespace PractiFly.WebApi.AutoMapper;

public class CourseDataProfile : Profile
{
    public CourseDataProfile(IPractiflyContext _context)
    {
        CreateProjection<Course, CourseFullInfoDto>()
            .ForMember(dto => dto.OwnerInfoDto, par => par.MapFrom(
                e => _context));

        CreateProjection<Course, OwnerInfoDto>()
            .ForMember(dto => dto.Id, par => par.MapFrom(
                e => e.OwnerId))
            .ForMember(dto => dto.Owner, par => par.MapFrom(
                e => string.Concat(e.Owner.FirstName, " ", e.Owner.LastName)))
            .ForMember(dto => dto.FilePhoto, par => par.MapFrom(
                e => e.Owner.FilePhoto));

        CreateProjection<Course, CourseInfoDto>()
            .ForMember(dto => dto.Language, par => par.MapFrom(
                e => e.Language.Name))
            .ForMember(dto => dto.CourseName, par => par.MapFrom(
                e => e.Name));

        CreateProjection<UserCourse, UserFullnameItemDto>()
            .ForMember(dto => dto.Id, par => par.MapFrom(
                e => e.UserId))
            .ForMember(dto => dto.Fullname, par => par.MapFrom(
                e => string.Concat(e.User.FirstName, " ", e.User.LastName)));

        CreateProjection<Course, OwnerInfoDto>()
            .ForMember(dto => dto.Id, par => par.MapFrom(
                o => o.OwnerId))
            .ForMember(dto => dto.Owner, par => par.MapFrom(
                o => string.Concat(o.Owner.FirstName, " ", o.Owner.LastName)))
            .ForMember(dto => dto.FilePhoto, par => par.MapFrom(
                o => o.Owner.FilePhoto));
    }
}