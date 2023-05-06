using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Materials;
using PractiFly.WebApi.Dto.Heading;
using PractiFly.WebApi.Dto.HeadingCourse;

namespace PractiFly.WebApi.AutoMapper.Profiles;

public class HeadingProfile : Profile
{
    public HeadingProfile(IPractiflyContext _context)
    {

        CreateMap<HeadingCreateDto, Heading>();
        CreateMap<Heading, HeadingInfoDto>();
        CreateMap<HeadingEditDto, Heading>();

        //TODO: IsIncluded in HeadingItemInCourseDto
        //TODO: Maybe not CourseHeading, but Heading
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
    }
}