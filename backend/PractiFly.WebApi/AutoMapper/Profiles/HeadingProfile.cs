using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Materials;
using PractiFly.WebApi.Dto.Heading;

namespace PractiFly.WebApi.AutoMapper.Profiles;

public class HeadingProfile : Profile
{
    public HeadingProfile()
    {
        CreateMap<HeadingCreateDto, Heading>();
        CreateMap<Heading, HeadingInfoDto>();
        CreateMap<HeadingEditDto, Heading>();
    }
}