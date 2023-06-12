using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.WebApi.Dto.CourseData;
using PractiFly.WebApi.Dto.CourseThemes;

namespace PractiFly.WebApi.AutoMapper.Profiles;

public class CourseThemesProfile : Profile
{
    public CourseThemesProfile(IPractiflyContext context)
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

        CreateProjection<Course, CourseItemWithThemeDto>()
            .ForMember(e => e.Themes, par => par.MapFrom(
                    e => context
                        .Themes
                        .Where(t => t.CourseId == e.Id)
                )
            );

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
                dto => dto.IsIncluded,
                par => par.MapFrom(
                    cm => context
                        .ThemeMaterials
                        .Any(tm => tm.MaterialId == cm.MaterialId)
                )
            );
    }
}