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
    }
}