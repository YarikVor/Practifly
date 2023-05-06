using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.Admin.UserView;
using PractiFly.WebApi.Dto.CourseData;

namespace PractiFly.WebApi.AutoMapper.Profiles;

public class CourseDataProfile : Profile
{
    public CourseDataProfile(IPractiflyContext _context)
    {
        CreateMap<Course, CourseInfoDto>();
        
        CreateProjection<User, OwnerInfoDto>()
            .ForMember(dto => dto.FullName, par => par.MapFrom(
                e => string.Concat(e.FirstName, " ", e.LastName)));

        CreateProjection<Course, CourseInfoDto>()
            .ForMember(dto => dto.Language, par => par.MapFrom(e => e.Language.Name));


        CreateProjection<Course, CourseFullInfoDto>()
            .ForMember(dest => dest.CourseInfoDto, opt => opt.MapFrom(e => e))
            .ForMember(dest => dest.OwnerInfoDto, opt => opt.MapFrom(e => e.Owner))
            .ForMember(
                dest => dest.UserFullnameItemDto,
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

        CreateMap<Course, CourseInfoDto>();
    }
}