using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.CourseDetails;

namespace PractiFly.WebApi.AutoMapper.Profiles;

public class CourseDetailsProfile : Profile
{
    public CourseDetailsProfile(IPractiflyContext _context)
    {
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
                                .Any(tm => tm.MaterialId == um.MaterialId))))
            .ForMember(m => m.Id, par => par.MapFrom(e => e.Theme.Id))
            .ForMember(m => m.Name, par => par.MapFrom(e => e.Theme.Name));

        CreateProjection<UserMaterial, CourseMaterialItemDto>()
            .ForMember(dto => dto.Id, par => par.MapFrom(m => m.Material.Id))
            .ForMember(dto => dto.Name, par => par.MapFrom(m => m.Material.Name));

        CreateProjection<ThemeMaterial, MaterialDetailsViewDto>()
            .ForMember(dto => dto.Url, par => par.MapFrom(m => m.Material.Url))
            .ForMember(dto => dto.Name, par => par.MapFrom(m => m.Material.Name))
            .ForMember(dto => dto.Id, par => par.MapFrom(m => m.Material.Id));

        CreateMap<UserMaterialSendDto, UserMaterial>()
            .ForMember(um => um.MaterialId, par => par.MapFrom(dto => dto.Id))
            .ReverseMap();

        CreateProjection<UserMaterial, UserMaterialInfoDto>();

        CreateProjection<Theme, CourseThemeItemDto>()
            .ForMember(dto => dto.IsCompleted, par => par.MapFrom(
                e => _context
                    .UserThemes
                    .Where(ut => ut.ThemeId == e.Id)
                    .Select(ut => ut.IsCompleted)
                    .FirstOrDefault()));
    }
}