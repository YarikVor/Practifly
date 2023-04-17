using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.WebApi.Dto.CourseDependencies;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.AutoMappers;

public class PractiFlyProfile : Profile
{
    private readonly IPractiflyContext _context;

    public PractiFlyProfile(IPractiflyContext context)
    {
        _context = context;


        // (CourseDependencyType)CourseDependenciesTypeCreateDto
        CreateMap<CourseDependenciesTypeCreateDto, CourseDependencyType>()
            .ForMember(
                e => e.Name,
                par
                    => par.MapFrom(e => e.Name)
            )
            .ForMember(
                e => e.Url,
                par
                    => par.MapFrom(e => e.FlagUrl)
            )
            .ForMember(
                e => e.Description,
                par
                    => par.MapFrom(e => e.Description)
            ).ForMember(
                e => e.Id,
                par
                    => par.MapFrom(e => e.Id)
            );
        /*
         new CourseDependencyType(){
            Name = courseDependenciesTypeCreateDto.CourseName
         }
        */
        
        
    }
}

