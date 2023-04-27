using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.MyCourse;
using PractiFly.WebApi.Dto.Profile;

namespace PractiFly.WebApi.AutoMappers;

public class MyCourseProfile : Profile
{
    public MyCourseProfile(IPractiflyContext _context)
    {
        CreateProjection<User, UserProfileInfoViewDto>();
        CreateProjection<User, UserProfileInfoCreateDto>();
        CreateProjection<User, UserInfoDto>()
            .ForMember(
                dto => dto.CountCompleted,
                par => par.MapFrom(
                    user =>
                        _context
                            .UserCourses
                            .Count(uc => uc.UserId == user.Id && uc.IsCompleted)
                )
            )
            .ForMember(
                dto => dto.CountInProgress,
                par => par.MapFrom(
                    user =>
                        _context
                            .UserCourses
                            .Count(uc => uc.UserId == user.Id && !uc.IsCompleted)
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
    }
}