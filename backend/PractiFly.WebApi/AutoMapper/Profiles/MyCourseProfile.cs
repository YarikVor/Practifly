using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.MyCourse;
using PractiFly.WebApi.Dto.Profile;

namespace PractiFly.WebApi.AutoMapper.Profiles;

public class MyCourseProfile : Profile
{
    public MyCourseProfile(IPractiflyContext _context)
    {
        string baseUrl = null!;
        CreateProjection<User, UserProfileInfoViewDto>()
            .ForMember(dto => dto.FilePhoto, par => par.MapFrom(
                e => baseUrl + (e.IsCustomPhoto ? e.Id : 0)));
        //CreateProjection<User, UserProfileInfoCreateDto>();
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
                        (float)(
                            _context
                                .UserCourses
                                .Where(uc => uc.UserId == e.Id)
                                .Select(uc => uc.Grade)
                                .Average() ?? 0)
                )
            )
            .ForMember(
                e => e.FilePhoto, par => par.MapFrom(e => baseUrl + (e.IsCustomPhoto ? e.Id : 0)));
        //TODO: Next topic, grade for current theme - відкинули
        CreateProjection<UserCourse, UserCourseStatusDto>()
            .ForMember(e => e.Language, par => par.MapFrom(e => e.Course.Language.Code))
            //TODO: можливо оцінки беруться із тем та з матеріалів
            .ForMember(
                e => e.GradeAverage,
                par => par.MapFrom(
                    e =>
                        (float)
                        (_context

                            .UserThemes
                            .Where(ut => ut.UserId == e.UserId)
                            .Where(ut => ut.Theme.CourseId == e.CourseId)
                            .Select(ut => ut.Grade)
                            //.DefaultIfEmpty()
                            .Average() ?? 0)
                )
            )
            .ForMember(
                dto => dto.CountThemes,
                par => par.MapFrom(
                    user =>
                        _context
                        .Themes
                        .Count(t => t.CourseId == user.CourseId)
                )
            )
            //TODO: CountProgress
            //.ForMember(
            //    dto => dto.CountProgress,
            //    par => par.MapFrom(
            //        user =>
            //            _context
            //            .UserThemes
            //           .Where(ut => ut.ThemeId == user.Id)
            //           //.Select(ut => ut.IsCompleted)
            //           .Count(uc => uc.IsCompleted)
            //    )
            //)
            .ForMember(
                dto => dto.CountProgress,
                par => par.MapFrom(
                    uc => _context
                    .UserThemes
                    .Where(ut => ut.Theme.CourseId == uc.CourseId)
                    .Where(ut => ut.UserId == uc.UserId)
                    .Count (ut => ut.IsCompleted)))
            //.ForMember(
            //    e => e.Grade,
            //    par => par.MapFrom(
            //        e => _context
            //            .UserMaterials
            //            .Where(cm => cm.UserId == e.UserId)
            //            .Where(cm => _context.CourseMaterials
            //                .Where(courseMaterial => courseMaterial.CourseId == e.CourseId)
            //                .Select(courseMaterial => courseMaterial.MaterialId)
            //                .Any(materialId => materialId == cm.MaterialId)
            //            )
            //            .Select(um => um.Grade)
            //            .OrderByDescending(grade => grade)
            //            .FirstOrDefault() ?? 0
            //    )
            //)
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
            );
    }
}