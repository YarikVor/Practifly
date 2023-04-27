using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.WebApi.Dto.CourseThemes;

namespace PractiFly.WebApi.AutoMappers;

public class CourseThemesProfile : Profile
{
    public CourseDetailsProfile(IPractiflyContext _context)
    {
        #region CourseThemes

        CreateProjection<Theme, ThemeDto>(); //мапінг вікна перегляду тем курсів

        CreateProjection<Theme, ThemeItemDto>(); //мапінг перегляду переліку курсів

        CreateProjection<Theme, ThemeEditDto>(); //мапінг вікна редагування тем курсів

        CreateProjection<Course, CourseItemWithThemeDto>() //мапінг переліку тем, що входять до курсу
            .ForMember(e => e.Themes, par => par.MapFrom(
                    e => _context
                        .Themes
                        .Where(t => t.CourseId == e.Id)
                        // TODO: Use ProjectTo
                        .Select(
                            t => new ThemeItemDto()
                            {
                                Id = t.Id,
                                Name = t.Name,
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

        #endregion

        
        #region CourseThemes (one)

        //CreateProjection<Course, CourseItemDto>();


        //CreateProjection<UserMaterial, MaterialItemDto>()
        //    .ForMember(dto => dto.Id, par => par.MapFrom(
        //        m => m.MaterialId))
        //    .ForMember(dto => dto.Name, par => par.MapFrom(
        //        m => m.Material.Name));
        ////TODO: 
        //CreateProjection<ThemeMaterial, ThemeMaterialInfoDto>()
        //    .ForMember(dto => dto.Material, par => par.MapFrom(
        //        tm => tm.Material));

        ////.ForMember(dto => dto.ViewStatus, par => par.MapFrom(
        ////    tm => tm.ViewStatus))
        ////.ForMember(dto => dto.SendStatus, par => par.MapFrom(
        ////    tm => tm.SendStatus));
        //////
        //CreateProjection<UserMaterial, UserMaterialInfoDto>();
        //CreateProjection<UserMaterial, UserMaterialSendDto>();

        #endregion

    }
}