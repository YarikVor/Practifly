using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.CourseData;
using PractiFly.WebApi.Dto.CourseDependencies;
using PractiFly.WebApi.Dto.CourseMaterials;
using PractiFly.WebApi.Dto.CourseThemes;
using PractiFly.WebApi.Dto.MyCourse;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.AutoMappers;

public class PractiFlyProfile : Profile
{
    private readonly PractiFlyContext _context;


    public PractiFlyProfile(IPractiflyContext context)
    {
        _context = (PractiFlyContext)context;


        // (CourseDependencyType)Course
        /*CreateMap<Course, CourseDependencyType>()
            .ForMember(
                e => e.Name,
                par
                    => par.MapFrom(e => e.Name)
            )
            .ForMember(
                e => e.Url,
                par
                    => par.MapFrom(e => e.FlagUrl)
            )
            .ForMember(
                e => e.Description,
                par
                    => par.MapFrom(e => e.Description)
            ).ForMember(
                e => e.Id,
                par
                    => par.MapFrom(e => e.Id)
            );*/
        /*
         new CourseDependencyType(){
            Name = courseDependenciesTypeCreateDto.CourseName
         }
        */

        // CreateMap<In, Out>() -> створення мапера, що перетворює тип даних In в тип даних Out
        // .ForMember(...) -> вказуємо, які властивості мапити
        // e => e.Name -> вказуємо, куди записувати дані
        // par => par.MapFrom(e => e.Name) -> вказуємо, звідки брати дані


        /*
         * .ForMember(out => out.CourseId, par => par.MapFrom(in => in.CourseId))
         *                          ^                                   ^
         *                          Куди записувати дані                Звідки брати дані
         */

        // TODO: Зробити: CountProgress, CountThemes, IsCompleted, IsChecked, Grade, GradeAverage, ThemeId


        CreateProjection<UserCourse, UserCourseStatusDto>()
            .ForMember(e => e.CourseId, par => par.MapFrom(e => e.CourseId))
            .ForMember(e => e.Language, par => par.MapFrom(e => e.Course.Language.Code))
            // TODO: можливо оцінки беруться із тем та з матеріалів
            .ForMember(
                e => e.GradeAverage,
                par => par.MapFrom(
                    e =>
                        (float)
                        _context
                            .UserMaterials
                            .Where(cm => cm.UserId == e.UserId)
                            .Where(cm => _context.CourseMaterials
                                .Where(courseMaterial => courseMaterial.CourseId == e.CourseId)
                                .Select(courseMaterial => courseMaterial.MaterialId)
                                .Any(materialId => materialId == cm.MaterialId)
                            )
                            .Select(um => um.Grade)
                            .DefaultIfEmpty()
                            .Average()
                )
            )
            .ForMember(
                e => e.Grade,
                par => par.MapFrom(
                    e => _context
                        .UserMaterials
                        .Where(cm => cm.UserId == e.UserId)
                        .Where(cm => _context.CourseMaterials
                            .Where(courseMaterial => courseMaterial.CourseId == e.CourseId)
                            .Select(courseMaterial => courseMaterial.MaterialId)
                            .Any(materialId => materialId == cm.MaterialId)
                        )
                        .Select(um => um.Grade)
                        .OrderByDescending(grade => grade)
                        .FirstOrDefault()
                )
            )
            .ForMember(
                e => e.Description,
                par => par.MapFrom(e => e.Course.Description)
            )
            .ForMember(
                e => e.Name,
                e => e.MapFrom(e => e.Course.Name)
            )
            .ForMember(
                e => e.Language,
                e => e.MapFrom(e => e.Course.Language.Code)
            )
            .ForMember(
                e => e.CourseId,
                e => e.MapFrom(e => e.CourseId)
            );


        #region CourseThemes

        CreateProjection<Course, CourseItemDto>();
        CreateProjection<Theme, ThemeItemDto>();

        CreateProjection<Course, CourseItemWithThemeDto>()
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
    }
}