using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Dto.CourseData;
using PractiFly.WebApi.Dto.CourseDetails;
using PractiFly.WebApi.Dto.CourseThemes;
using PractiFly.WebApi.Dto.HeadingCourse;
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

    /// <summary>
    /// Retrieves an array of courses associated with a user identified by the specified Id, or all courses if no Id is provided.
    /// </summary>
    /// <param name="userId">Id of the user.</param>
    /// <returns>A JSON-encoded representation of the array of courses.</returns>
    [HttpGet]
    [Route("user/{userId:int?}/courses")]
    public async Task<IActionResult> UserCourses(int? userId = null)
    {
        CourseItemDto[] result;
        if (!userId.HasValue)
        {
            result = await _context.Courses.AsNoTracking()
                .ProjectTo<CourseItemDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
        else
        {
            result = await _context.Courses.AsNoTracking()
                .Where(e => e.OwnerId == userId)
                .ProjectTo<CourseItemDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
        
        return Json(result);
    }


    /// <summary>
    /// Returns a list of headings included in the course identified by the specified Id.
    /// </summary>
    /// <param name="courseId">Id of the course.</param>
    /// <returns>A JSON-encoded representation of the list of included headings.</returns>
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetIncludedHeadings(int courseId)
    {
        //TODO: Check included headings to courses in HeadingCourseItemDto
        HeadingCourseItemDto result = await _context
            .Courses
            .AsNoTracking()
            .Where(e => e.Id == courseId)
            .ProjectTo<HeadingCourseItemDto>(_mapper.ConfigurationProvider)
            .FirstAsync();

        return Json(result);
    }

    //TODO: ???
    /// <summary>
    /// Returns detailed information about a material identified by the specified Id.
    /// </summary>
    /// <param name="themeMaterialId">Id of the theme material</param>
    /// <returns>A JSON-encoded representation of the material details.</returns>
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> ViewMaterialDetails(int themeMaterialId)
    {
        MaterialDetailsViewDto result = await _context
            .Courses
            .AsNoTracking()
            .Where(e => e.Id == themeMaterialId)
            .ProjectTo<MaterialDetailsViewDto>(_mapper.ConfigurationProvider)
            .FirstAsync();

        return Json(result);
    }

    /// <summary>
    /// Returns an array of materials associated with a theme identified by the specified themeId.
    /// </summary>
    /// <param name="themeId">Id of the theme.</param>
    /// <returns>A JSON-encoded representation of the array of materials.</returns>
    [HttpGet]
    [Route("[action]")]
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