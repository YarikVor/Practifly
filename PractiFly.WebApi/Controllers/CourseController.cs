using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Dto.CourseData;
using PractiFly.WebApi.Dto.CourseMaterials;
using PractiFly.WebApi.Dto.CourseThemes;
using PractiFly.WebApi.Dto.MyCourse;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CourseController : Controller
{
    private readonly IPractiflyContext _context;
    private readonly IMapper _mapper;

    public CourseController(IPractiflyContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{userId:int}/usercourse")]
    public async Task<IActionResult> UserCourse(int userId)
    {
        UserCourseStatusDto[] result = await _context
            .UserCourses
            .AsNoTracking()
            .Where(e => e.UserId == userId)
            .ProjectTo<UserCourseStatusDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync();

        return Json(result);
    }

    [HttpGet]
    [Route("user/{userId:int?}/courses")]
    public async Task<IActionResult> UserCourses(int? userId = null)
    {
        CourseItemDto[] result;
        if (!userId.HasValue)
        {
            result = _context.Courses.AsNoTracking()
                .ProjectTo<CourseItemDto>(_mapper.ConfigurationProvider)
                .ToArray();
        }
        else
        {
            result = _context.Courses.AsNoTracking()
                .Where(e => e.OwnerId == userId)
                .ProjectTo<CourseItemDto>(_mapper.ConfigurationProvider)
                .ToArray();
        }

        return Json(result);
    }

    [HttpGet]
    [Route("course/{courseId:int}/themes")]
    public async Task<IActionResult> GetCourseThemes(int courseId)
    {
        CourseItemWithThemeDto result = await _context
            .Courses
            .AsNoTracking()
            .Where(e => e.Id == courseId)
            .ProjectTo<CourseItemWithThemeDto>(_mapper.ConfigurationProvider)
            .FirstAsync();

        return Json(result);
    }


    [HttpGet]
    public async Task<IActionResult> GetMaterialsInTheme(int themeId)
    {
        

        MaterialsMenuDto[] result = await
            _context
                .CourseMaterials
                .AsNoTracking()
                .Where(cm => cm.CourseId == _context.Themes.FirstOrDefault(theme => theme.Id == themeId).CourseId)
                .Select(cm => new MaterialsMenuDto()
                {
                    Id = cm.MaterialId,
                    Name = cm.Material.Name,
                    IsIncluded = _context
                        .ThemeMaterials
                        .Any(tm => tm.MaterialId == cm.MaterialId && tm.ThemeId == themeId),
                    Priority = cm.PriorityLevel
                })
                .ToArrayAsync();

        return Json(result);
    }
}