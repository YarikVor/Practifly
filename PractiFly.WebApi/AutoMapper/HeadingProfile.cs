using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Materials;
using PractiFly.WebApi.Dto.Heading;
using PractiFly.WebApi.Dto.HeadingCourse;

namespace PractiFly.WebApi.AutoMappers;

public class HeadingProfile : Profile
{
    public HeadingProfile(IPractiflyContext _context)
    {
        #region Heading

        CreateProjection<Heading, HeadingItemDto>();
        CreateProjection<Heading, HeadingInfoDto>();
        CreateMap<HeadingCreateDto, Heading>();
        CreateMap<Heading, HeadingInfoDto>();
        
        #endregion
        
        
        #region HeadingCourse

        //TODO: Dto for Editing

        //CreateProjection<Heading, HeadingCourseEditDto>()
        //    .ForMember(
        //    e => e.IsIncluded,
        //    par => par.MapFrom(e => e.) //має бути булеве поле, що відповідає полю IsIncluded
        //    ); 

        CreateProjection<CourseHeading, HeadingCourseItemDto>()
            .ForMember(
                e => e.Id,
                par => par.MapFrom(e => e.HeadingId)
            )
            .ForMember(dto => dto.Name, par => par.MapFrom(
                e => _context
                    .CourseHeadings
                    .Where(ch => ch.HeadingId == e.Id)
                    .Select(m => m.Heading.Name)))
            .ForMember(dto => dto.Code, par => par.MapFrom(
                e => _context
                    .CourseHeadings
                    .Where(ch => ch.HeadingId == e.Id)
                    .Select(m => m.Heading.Code)))
            .ForMember(dto => dto.IsIncluded, par => par.MapFrom(
                e => _context
                    .CourseHeadings
                    .Any(cm => cm.HeadingId == e.Id)));
        //CreateProjection<Course, CourseItemDto>();

        #endregion
    }
}