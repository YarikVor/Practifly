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
                        .FirstOrDefault()
                )
            );
        
        CreateProjection<UserTheme, ThemeWithMaterialsDto>()
            .ForMember(
                m => m.Materials,
                par => par.MapFrom(
                    ut =>
                        _context
                            .UserMaterials
                            .Where(um => um.UserId == ut.UserId)
                            .Where(um => _context
                                .ThemeMaterials
                                .Where(tm => tm.ThemeId == ut.ThemeId) 
                                .Any(tm => tm.MaterialId == um.MaterialId)
                            )
                )
            )
            .ForMember(m => m.Id, par => par.MapFrom(e => e.ThemeId))
            .ForMember(m => m.Name, par => par.MapFrom(e => e.Theme.Name));

        CreateProjection<UserMaterial, CourseMaterialItemDto>()
            .ForMember(dto => dto.Id, par => par.MapFrom(m => m.MaterialId))
            .ForMember(dto => dto.Name, par => par.MapFrom(m => m.Material.Name));
        
        CreateProjection<ThemeMaterial, MaterialDetailsViewDto>()
            .ForMember(dto => dto.Url, par => par.MapFrom(m => m.Material.Url))
            .ForMember(dto => dto.Description, par => par.MapFrom(m => m.Description))
            .ForMember(dto => dto.Name, par => par.MapFrom(m => m.Material.Name))
            .ForMember(dto => dto.Id, par => par.MapFrom(m => m.Material.Id));

        CreateMap<UserMaterialSendDto, UserMaterial>()
            .ForMember(um => um.MaterialId, par => par.MapFrom(dto => dto.Id));
        
        
        //TODO: Maybe delete
        CreateProjection<Material, MaterialDetailsViewDto>()
            .ForMember(dto => dto.Url, par => par.MapFrom(m => m.Url));
        
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

        CreateProjection<Theme, CourseThemeItemDto>()
            .ForMember(dto => dto.IsCompleted, par => par.MapFrom(
                e => _context
                    .UserThemes
                    .Where(ut => ut.ThemeId == e.Id)
                    .Select(ut => ut.IsCompleted)
                    .FirstOrDefault()));

        CreateProjection<UserMaterial, CourseMaterialItemDto>()
            .ForMember(dto => dto.Id, par => par.MapFrom(
                e => e.MaterialId))
            .ForMember(dto => dto.Name, par => par.MapFrom(
                e => e.Material.Name));

        CreateProjection<UserMaterial, UserMaterialInfoDto>();


    }
}