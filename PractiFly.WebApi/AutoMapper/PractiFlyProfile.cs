using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Materials;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.CourseData;
using PractiFly.WebApi.Dto.CourseDependencies;
using PractiFly.WebApi.Dto.CourseDetails;
using PractiFly.WebApi.Dto.CourseThemes;
using PractiFly.WebApi.Dto.Heading;
using PractiFly.WebApi.Dto.HeadingCourse;
using PractiFly.WebApi.Dto.Level;
using PractiFly.WebApi.Dto.MyCourse;
using PractiFly.WebApi.Dto.Profile;
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

        #region MyCourse
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
                par => par.MapFrom(e => e.Course.Name)
            )
            .ForMember(
                e => e.Language,
                par => par.MapFrom(e => e.Course.Language.Code)
            )
            .ForMember(
                e => e.CourseId,
                par => par.MapFrom(e => e.CourseId)
            );
        #endregion

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
        #region Heading
        CreateProjection<Heading, HeadingItemDto>();

        CreateProjection<Heading, HeadingInfoDto>();
        #endregion

        #region HeadingCourse
        //TODO: Dto for Editing

        //CreateProjection<Heading, HeadingCourseEditDto>()
        //    .ForMember(
        //    e => e.IsIncluded,
        //    par => par.MapFrom(e => e.) //має бути булеве поле, що відповідає полю IsIncluded
        //    ); 

        //CreateProjection<CourseHeading, HeadingCourseItemDto>()
        //    .ForMember(
        //    e => e.Id, 
        //    par => par.MapFrom(e => e.HeadingId)
        //    )
        //    .ForMember(
        //    e => e.IsIncluded,
        //    par => par.MapFrom(e => _context) //IsIncluded for this 
        //    );

        #endregion

        #region Profile
        CreateProjection<User, UserProfileInfoViewDto>();
        CreateProjection<User, UserProfileInfoCreateDto>();
        CreateProjection<User, UserInfoDto>()
            .ForMember(
            dto => dto.CountCompleted,
            par => par.MapFrom(
                    user =>
                        _context
                            .UserCourses
                            .Where(uc => uc.UserId == user.Id && uc.IsCompleted)
                            .Count()
                            )
            )
            .ForMember(
            dto => dto.CountInProgress,
            par => par.MapFrom(
                    user =>
                        _context
                            .UserCourses
                            .Where(uc => uc.UserId == user.Id && !uc.IsCompleted)
                            .Count()
                            )
            )
            .ForMember(
                e => e.AverageGrade,
                par => par.MapFrom(
                    e =>
                        (float)
                        _context
                            .UserCourses
                            .Where(uc => uc.UserId == e.Id)
                            .Select(uc => uc.Grade)
                            .DefaultIfEmpty()
                            .Average()
                            )
                );
        #endregion
        #region CourseDetails
        CreateProjection<Course, ThemeDetailsMenuDto>()
             .ForMember(m => m.MaterialItemDto, par => par.MapFrom(
                    m => _context
                        .Courses
                        .Where(c => c.Id == c.Id)
                        .Select(
                            c => new MaterialItemDto()
                            {
                                Id = c.Id,
                                Name = c.Name,
                            }
                        )
                ));
        CreateProjection<Material, MaterialDetailsViewDto>()
            .ForMember(dto => dto.MaterialUrl, par => par.MapFrom(
                m => m.Url));
        CreateProjection<Theme, CourseThemeItemDto>();
        CreateProjection<UserMaterial, MaterialItemDto>()
            .ForMember(dto => dto.Id, par => par.MapFrom(
                m => m.MaterialId))
            .ForMember(dto => dto.Name, par => par.MapFrom(
                m => m.Material.Name));
        //TODO: 
        //CreateProjection<ThemeMaterial, ThemeMaterialInfoDto>()
        //    .ForMember(dto => dto.Material, par => par.MapFrom(
        //        tm =>  tm.Material ))
            
        #endregion
    }
}