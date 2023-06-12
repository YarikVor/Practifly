using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.CourseData;

namespace PractiFly.WebApi.AutoMapper.Profiles;

public class CourseDataProfile : Profile
{
    public CourseDataProfile(IPractiflyContext _context)
    {
        CreateMap<Course, CourseInfoDto>();

        string baseUrl = null!;
        CreateProjection<User, OwnerInfoDto>()
            .ForMember(dto => dto.FullName, par => par.MapFrom(
                e => string.Concat(e.FirstName, " ", e.LastName)))
            .ForMember(dto => dto.FilePhoto, par => par.MapFrom(
                e => baseUrl + (e.IsCustomPhoto ? e.Id : 0)));

        CreateMap<Course, CourseInfoDto>()
            .ForMember(dto => dto.Language, par => par.MapFrom(e => e.Language.Name));

        CreateProjection<Course, CourseFullInfoDto>()
            .ForMember(dest => dest.Course, opt => opt.MapFrom(e => e))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(e => e.Owner))
            .ForMember(
                dest => dest.Users,
                opt => opt.MapFrom(
                    e => _context
                        .UserCourses
                        .Where(uc => uc.CourseId == e.Id)
                        .Select(e => e.User)
                )
            );

        CreateMap<CreateCourseDto, Course>()
            .ForMember(c => c.Language, par => par.Ignore())
            .ForMember(c => c.Owner, par => par.Ignore())
            .ForMember(c => c.LanguageId, par => par.MapFrom(
                dto => _context
                    .Languages
                    .Where(l => l.Code == dto.Language)
                    .Select(l => l.Id)
                    .FirstOrDefault()));

        CreateMap<CourseUsersDto, UserCourse>();
        //.ForMember(e => e.UserId, par => par.MapFrom(
        //    dto => dto.UserId));
        // _context
        // .Users
        //.Where(u => u.Id == dto.UserId)
        //.Any(u => u.Id == dto.UserId)));
    }
}