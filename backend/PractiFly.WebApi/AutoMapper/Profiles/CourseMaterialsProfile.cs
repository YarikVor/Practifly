using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Materials;
using PractiFly.WebApi.Dto.CourseMaterials;
using PractiFly.WebApi.Dto.Heading;

namespace PractiFly.WebApi.AutoMapper.Profiles;

public class CourseMaterialsProfile : Profile
{
    public CourseMaterialsProfile(IPractiflyContext _context)
    {
        CreateProjection<Heading, HeadingInfoDto>();
        CreateProjection<Material, MaterialForInclusionDto>()
            .ForMember(dto => dto.IsIncluded, par => par.MapFrom(
                e => _context
                    .CourseMaterials
                    .Any(cm => cm.MaterialId == e.Id)))
            .ForMember(dto => dto.PriorityLevel, par => par.MapFrom(
                e => _context
                    .CourseMaterials
                    .Where(cm => cm.MaterialId == e.Id)
                    .Select(cm => cm.PriorityLevel)));
            
        CreateProjection<Material, MaterialFromIncludedBlockViewDto>()
            .ForMember(dto => dto.PriorityLevel, par => par.MapFrom(
                e => _context
                    .CourseMaterials
                    .Where(cm => cm.MaterialId == e.Id)
                    .Select(cm => cm.PriorityLevel)));

        CreateProjection<Heading, CourseHeadingInfoDto>();


        var courseId = 0;
        CreateProjection<HeadingMaterial, MaterialForInclusionDto>()
            .ForMember(
                dto => dto.Id,
                par => par.MapFrom(
                    m => m.Material.Id))
            .ForMember(
                dto => dto.Name,
                par => par.MapFrom(
                    m => m.Material.Name))
            .ForMember(
                dto => dto.IsPractical,
                par => par.MapFrom(
                    m => m.Material.IsPractical))
            .ForMember(
                dto => dto.IsIncluded,
                par => par.MapFrom(
                    m => _context
                        .CourseMaterials
                        .Any(cm => cm.CourseId == courseId && cm.MaterialId == m.Id)))
            .ForMember(dto => dto.PriorityLevel,
                par => par.MapFrom(
                    m => _context
                        .CourseMaterials
                        .Where(cm => cm.CourseId == courseId && cm.MaterialId == m.Id)
                        .Select(cm => cm.PriorityLevel)
                        .FirstOrDefault()
                )
            );
    }
}