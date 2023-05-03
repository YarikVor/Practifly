using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.WebApi.Dto.CourseDetails;
using PractiFly.WebApi.Dto.CourseThemes;
using PractiFly.WebApi.Dto.HeadingCourse;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("api")]
public class CourseController : Controller
{
    private readonly IPractiflyContext _context;
    private readonly IMapper _mapper;

    public CourseController(IPractiflyContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    ///// <summary>
    ///// Retrieves an array of courses associated with a owner (user) identified by the specified Id, or all courses if no Id is provided.
    ///// </summary>
    ///// <param name="ownerId">Id of the owner (user).</param>
    ///// <response code="200">Courses representation was successful.</response>
    ///// <response code="400">Operation was failed.</response>
    ///// <response code="404">No courses found.</response>
    ///// <returns>A JSON-encoded representation of the array of courses.</returns>
    //[HttpGet]
    //[Route("course/all")]
    //public async Task<IActionResult> Courses(int? ownerId = null)
    //{
    //    CourseItemDto[] result;
    //    if (!ownerId.HasValue)
    //    {
    //        result = await _context.Courses.AsNoTracking()
    //            .ProjectTo<CourseItemDto>(_mapper.ConfigurationProvider)
    //            .ToArrayAsync();
    //    }
    //    else
    //    {
    //        result = await _context.Courses.AsNoTracking()
    //            .Where(e => e.OwnerId == ownerId)
    //            .ProjectTo<CourseItemDto>(_mapper.ConfigurationProvider)
    //            .ToArrayAsync();
    //    }

    //    return Json(result);
    //}

    /// <summary>
    ///     Returns a list of headings included in the course identified by the specified Id.
    /// </summary>
    /// <param name="courseId">Id of the course.</param>
    /// <response code="200">Representation headings in course was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No headings found.</response>
    /// <returns>A JSON-encoded representation of the list of included headings.</returns>
    [HttpGet]
    [Route("course/headings/included")]
    public async Task<IActionResult> GetIncludedHeadings(int courseId)
    {
        //TODO: Check included headings to courses in HeadingCourseItemDto
        var result = await _context
            .Courses
            .AsNoTracking()
            .Where(e => e.Id == courseId)
            .ProjectTo<HeadingCourseItemDto>(_mapper.ConfigurationProvider)
            .FirstAsync();

        return Json(result);

        //TODO: можлива реалізація даного методу
        //HeadingCourseItemDto result = await _context
        //.Courses
        //.AsNoTracking()
        //.Where(c => c.Id == courseId)
        //.Select(c => new HeadingCourseItemDto()
        //{
        //    Id = c.Id,
        //    Name = c.Name,
        //    Headings = c.CourseHeadings
        //        .Where(ch => ch.IsIncluded)
        //        .Select(ch => new HeadingItemDto()
        //        {
        //            Id = ch.Heading.Id,
        //            Name = ch.Heading.Name
        //        })
        //        .ToList()
        //})
        //.FirstOrDefaultAsync();
    }

    //TODO: ???
    /// <summary>
    ///     Returns detailed information about a material identified by the specified Id.
    /// </summary>
    /// <param name="themeMaterialId">Id of the theme material</param>
    /// <response code="200">Viewing material details was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No materials found.</response>
    /// <returns>A JSON-encoded representation of the material details.</returns>
    [HttpGet]
    [Route("theme/materials")]
    public async Task<IActionResult> ViewMaterialDetails(int themeMaterialId)
    {
        var result = await _context
            .Courses
            .AsNoTracking()
            .Where(e => e.Id == themeMaterialId)
            .ProjectTo<MaterialDetailsViewDto>(_mapper.ConfigurationProvider)
            .FirstAsync();

        return Json(result);
    }

    /// <summary>
    ///     Returns an array of materials associated with a theme identified by the specified themeId.
    /// </summary>
    /// <param name="themeId">Id of the theme.</param>
    /// <response code="200">Getting materials in theme was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No materials found.</response>
    /// <returns>A JSON-encoded representation of the array of materials.</returns>
    [HttpGet]
    [Route("theme/materials_")]
    public async Task<IActionResult> GetMaterialsInTheme(int themeId)
    {
        var result = await
            _context
                .CourseMaterials
                .AsNoTracking()
                .Where(cm => cm.CourseId == _context.Themes.FirstOrDefault(theme => theme.Id == themeId).CourseId)
                .Select(cm => new MaterialsMenuDto
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