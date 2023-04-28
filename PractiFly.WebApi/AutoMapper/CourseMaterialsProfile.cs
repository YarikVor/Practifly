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
    }
}