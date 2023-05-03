using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Materials;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.CourseDetails;
using PractiFly.WebApi.Dto.CourseThemes;

namespace PractiFly.WebApi.AutoMappers;

public class CourseDetailsProfile : Profile
{
    public CourseDetailsProfile(IPractiflyContext _context)
    {
        CreateProjection<Theme, CourseThemeItemDto>()
            .ForMember(
                t => t.IsCompleted, par => par.MapFrom(
                    e => _context
                        .UserThemes
                        .Select(ut => ut.IsCompleted)
                )
            );

        CreateProjection<Theme, CourseThemeWithMaterialsDto>()
            .ForMember(
                m => m.Materials,
                par => par.MapFrom(
                    m => _context
                        .Themes
                        .Where(c => c.Id == c.Id)
                    //.Select(
                    //    c => new MaterialItemDto()
                    //    {
                    //        Id = c.Id,
                    //        Name = c.Name,
                    //    }
                    //)
                )
            )
            .ForMember(
                t => t.IsCompleted,
                par => par.MapFrom(
                    e => _context
                        .UserThemes
                        .Select(ut => ut.IsCompleted)
                )
            );

        CreateProjection<Material, MaterialDetailsViewDto>()
            .ForMember(dto => dto.Url, par => par.MapFrom(m => m.Url));

        CreateProjection<UserMaterial, CourseMaterialItemDto>()
            .ForMember(dto => dto.Id, par => par.MapFrom(m => m.MaterialId))
            .ForMember(dto => dto.Name, par => par.MapFrom(m => m.Material.Name));

        CreateProjection<ThemeMaterial, ThemeContentInfoDto>()
            .ForMember(dto => dto.Material, par => par.MapFrom(tm => tm.Material));
        //.ForMember(dto => dto.ViewStatus, par => par.MapFrom(
        //    tm => tm.ViewStatus))
        //.ForMember(dto => dto.SendStatus, par => par.MapFrom(
        //    tm => tm.SendStatus));
        ////
        CreateProjection<UserMaterial, UserMaterialInfoDto>();
        CreateProjection<UserMaterial, UserMaterialSendDto>();
        CreateProjection<CourseMaterial, MaterialsMenuDto>()

            .ForMember(dto => dto.Id, par => par.MapFrom(
                cm => cm.MaterialId))
            .ForMember(dto => dto.Name, par => par.MapFrom(
                cm => cm.Material.Name))
            //.ForMember(dto => dto.IsIncluded, par => par.MapFrom(
            //    cm => _context
            //        .ThemeMaterials
            //        .Any(tm => tm.MaterialId == cm.MaterialId && tm.ThemeId == tm.themeId)
            .ForMember(dto => dto.Priority, par => par.MapFrom(
                cm => cm.PriorityLevel));
    }
}