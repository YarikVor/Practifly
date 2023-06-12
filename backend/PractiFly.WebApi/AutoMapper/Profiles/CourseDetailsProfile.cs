using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.CourseDetails;

namespace PractiFly.WebApi.AutoMapper.Profiles;

public class CourseDetailsProfile : Profile
{
    public CourseDetailsProfile(IPractiflyContext context)
    {
        CreateProjection<UserTheme, ThemeWithMaterialsDto>()
            .ForMember(
                m => m.Materials,
                par => par.MapFrom(
                    ut =>
                        context
                            .UserMaterials
                            .Where(um => um.UserId == ut.UserId)
                            .Where(um => context
                                .ThemeMaterials
                                .Where(tm => tm.ThemeId == ut.ThemeId)
                                .Any(tm => tm.MaterialId == um.MaterialId))))
            .ForMember(m => m.Id, par => par.MapFrom(e => e.Theme.Id))
            .ForMember(m => m.Name, par => par.MapFrom(e => e.Theme.Name));

        CreateProjection<UserMaterial, CourseMaterialItemDto>()
            .ForMember(dto => dto.Id, par => par.MapFrom(m => m.Material.Id))
            .ForMember(dto => dto.Name, par => par.MapFrom(m => m.Material.Name));

        CreateProjection<ThemeMaterial, MaterialDetailsViewDto>()
            .ForMember(dto => dto.Url, par => par.MapFrom(m => m.Material.Url))
            .ForMember(dto => dto.Name, par => par.MapFrom(m => m.Material.Name))
            .ForMember(dto => dto.Id, par => par.MapFrom(m => m.Material.Id))
            .ForMember(dto => dto.Note, par => par.MapFrom(m => m.Material.Note));


        CreateMap<UserMaterialSendDto, UserMaterial>()
            .ForMember(um => um.MaterialId, par => par.MapFrom(dto => dto.Id))
            .ReverseMap();

        CreateProjection<UserMaterial, UserMaterialInfoDto>();

        CreateProjection<Theme, CourseThemeItemDto>()
            .ForMember(dto => dto.IsCompleted, par => par.MapFrom(
                e => context
                    .UserThemes
                    .Where(ut => ut.ThemeId == e.Id)
                    .Select(ut => ut.IsCompleted)
                    .FirstOrDefault()));
        //
        var userId = 0;
        CreateProjection<Course, UserCourseInfoDto>()
            .ForMember(dto => dto.Themes, par => par.MapFrom(c => context
                .Themes
                .Where(t => t.CourseId == c.Id)
            ));

        CreateProjection<Theme, FullThemeWithMaterialsDto>()
            .ForMember(dto => dto.IsCompleted, par => par.MapFrom(t => context
                .UserThemes
                .Where(ut => ut.UserId == userId && ut.ThemeId == t.Id)
                .Select(ut => ut.IsCompleted)
                .FirstOrDefault()
            ))
            .ForMember(dto => dto.Grade, par => par.MapFrom(t => context
                .UserThemes
                .Where(ut => ut.UserId == userId && ut.ThemeId == t.Id)
                .Select(ut => ut.Grade)
                .FirstOrDefault()))
            .ForMember(dto => dto.Materials, par => par.MapFrom(t => context
                .ThemeMaterials
                .Where(tm => tm.ThemeId == t.Id)
                .Select(tm => tm.Material)
                .Select(m => new
                {
                    Material = m,
                    UserMaterial = context
                        .UserMaterials
                        .FirstOrDefault(um => um.UserId == userId && um.MaterialId == m.Id)
                })
                .Select(m => new CourseMaterialItemDto
                {
                    Id = m.Material.Id,
                    Name = m.Material.Name,
                    IsCompleted = m.UserMaterial != null && m.UserMaterial.IsCompleted,
                    Grade = m.UserMaterial == null ? 0 : m.UserMaterial.Grade,
                    Note = m.Material.Note,
                    Url = m.Material.Url
                })
            ))
            ;
    }
}