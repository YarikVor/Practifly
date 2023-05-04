using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Materials;
using PractiFly.WebApi.Dto.Heading;

namespace PractiFly.WebApi.AutoMapper;

public class HeadingCourseProfile : Profile
{
    public HeadingCourseProfile(IPractiflyContext context)
    {
        CreateProjection<Heading, HeadingItemDto>();
        CreateProjection<Heading, HeadingItemInCourseDto>();
        //TODO: IsIncluded in HeadingItemInCourseDto
    }
}