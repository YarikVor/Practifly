using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Materials;
using PractiFly.WebApi.Dto.CourseMaterials;
using PractiFly.WebApi.Dto.Heading;

namespace PractiFly.WebApi.AutoMappers;

public class CourseMaterialsProfile : Profile
{
    public CourseMaterialsProfile(IPractiflyContext _context)
    {
        CreateProjection<Heading, HeadingInfoDto>();
        CreateProjection<Material, MaterialForInclusionDto>() //Id, Name, IsPractical
            .ForMember(dto => dto.IsIncluded, par => par.MapFrom(
                e => _context
                    .CourseMaterials
                    .Any(cm => cm.MaterialId == e.Id)))
            .ForMember(dto => dto.PriorityLevel, par => par.MapFrom(
                e => _context
                    .CourseMaterials
                    .Where(cm => cm.MaterialId == e.Id)
                    .Select(cm => cm.PriorityLevel))) //Чи правильно?
            //.ForMember(dto => dto.Type, par => par.MapFrom(
            //    )
            ;
        CreateProjection<Material, MaterialFromIncludedBlockViewDto>()
            .ForMember(dto => dto.PriorityLevel, par => par.MapFrom(
                e => _context
                    .CourseMaterials
                    .Where(cm => cm.MaterialId == e.Id)
                    .Select(cm => cm.PriorityLevel)));
        //CreateProjection<Material, MaterialFromIncludedBlockViewDto>()
        //    .ForMember(dto => dto.PriorityLevel, par => par.MapFrom(
        //        e => _context
        //            .CourseMaterials
        //            .Where(cm => cm.MaterialId == e.Id)
        //            .Select(cm => cm.PriorityLevel)));
        //CreateProjection<ThemeMaterial, ThemeMaterialInfoDto>()
        //    .ForMember(dto => dto.Material, par => par.MapFrom(
        //        tm =>  tm.Material ))

        CreateProjection<Heading, CourseHeadingInfoDto>()
            .ForMember(dto => dto.Id, par => par.MapFrom(ch => ch.Id))
            .ForMember(dto => dto.Name, par => par.MapFrom(ch => ch.Name))
            .ForMember(dto => dto.Code, par => par.MapFrom(ch => ch.Code));


        var courseId = 0;
        CreateProjection<HeadingMaterial, MaterialForInclusionDto>()
            .ForMember(
                dto => dto.Id,
                par => par.MapFrom(
                    m => m.MaterialId))
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