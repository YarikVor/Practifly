using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.WebApi.Dto.CourseData;
using PractiFly.WebApi.Dto.CourseThemes;

namespace PractiFly.WebApi.AutoMappers;

public class CourseThemesProfile : Profile
{
    public CourseThemesProfile(IPractiflyContext _context)
    {
        CreateProjection<Theme, ThemeInfoDto>();
        CreateProjection<Theme, ThemeItemDto>();
        CreateProjection<Theme, ThemeEditDto>();
        CreateProjection<Course, CourseItemDto>();
        CreateMap<ThemeCreateDto, Theme>();
        CreateMap<ThemeEditDto, Theme>();
        CreateMap<ThemeMaterialCreateDto, ThemeMaterial>();
        CreateMap<Theme, ThemeInfoDto>();
        CreateMap<ThemeMaterial, ThemeMaterialInfoDto>();


        CreateProjection<Course, CourseItemWithThemeDto>() //мапінг переліку тем, що входять до курсу
            .ForMember(e => e.Themes, par => par.MapFrom(
                    e => _context
                        .Themes
                        .Where(t => t.CourseId == e.Id)
                        // TODO: Use ProjectTo
                        .Select(
                            t => new ThemeItemDto
                            {
                                Id = t.Id,
                                Name = t.Name
                            }
                        )
                )
            );

        // TODO: Maybe another type data
        /*
        CreateProjection<Material, MaterialsMenuDto>()  //мапінг перегляду меню матеріалів
            .ForMember(
            dto => dto.Grade,
            par => par.MapFrom(
                e => 
                _context
                .ThemeMaterials
                .Where(i => i.MaterialId == e.Id)
                .Select(i => i.Number)
                )
            )
            .ForMember(dto => dto.IsSelected,
            par => par.MapFrom(
                e => 
                _context
                .ThemeMaterials
                .Any(i => i.MaterialId == e.Id)
                )
            );
        */
        CreateProjection<CourseMaterial, MaterialsMenuDto>()
            .ForMember(
                dto => dto.Id,
                par => par.MapFrom(cm => cm.Material.Id)
            )
            .ForMember(
                dto => dto.Name,
                par => par.MapFrom(cm => cm.Material.Name)
            )
            .ForMember(
                dto => dto.Priority,
                par => par.MapFrom(cm => cm.PriorityLevel)
            )
            .ForMember(
                dto => dto.IsIncluded,
                par => par.MapFrom(
                    cm => _context
                        .ThemeMaterials
                        .Any(tm => tm.MaterialId == cm.MaterialId)
                )
            );
    }
}