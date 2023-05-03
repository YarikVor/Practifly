using System.ComponentModel.DataAnnotations;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities;
using PractiFly.DbEntities.Courses;
using PractiFly.WebApi.Dto.Heading;
using PractiFly.WebApi.Dto.HeadingCourse;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("api")]
public class HeadingCourseController : Controller
{
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IPractiflyContext _context;

    public HeadingCourseController(
        IPractiflyContext context,
        IConfigurationProvider configurationProvider
    )
    {
        _context = context;
        _configurationProvider = configurationProvider;
    }

    /// <summary>
    ///     Get headings by begin heading code
    /// </summary>
    /// <param name="beginCode">
    ///     The beginning of a section of code that returns sub-sections (ex: 12 -> 12.__, where '_' - 0-9).
    ///     Supports four rubric levels (ex: "", "12", "12.12", "12.12.12")
    /// </param>
    /// <returns></returns>
    /// <response code="200">Return subheading info (maybe empty result)</response>
    [HttpGet]
    [Route("heading/sub")]
    public async Task<IActionResult> GetSubheading(
        [RegularExpression(EntitiesConstants.SubHeadingPattern)]
        string beginCode
    )
    {
        var patternSubRubricCode = beginCode.GetCodeLike();

        var result = await _context.Headings
            .AsNoTracking()
            .Where(e => EF.Functions.Like(e.Code, patternSubRubricCode))
            .ProjectTo<HeadingItemDto>(_configurationProvider)
            .ToListAsync();

        return Json(result);
    }

    /// <summary>
    ///     Returns the subtopics, but adds an IsIncluded field indicating whether it is included in the course.
    /// </summary>
    /// <param name="courseId"> Course identifier used to obtain included and excluded rubrics </param>
    /// <param name="beginCode">
    ///     The beginning of a section of code that returns sub-sections (ex: 12 -> 12.__, where '_' - 0-9).
    ///     Supports three rubric levels (ex: "", "12", "12.12", "12.12.12")
    /// </param>
    /// <response code="200">Operation was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No courses found.</response>
    /// <returns></returns>
    [HttpGet]
    [Route("course/heading/sub")]
    public async Task<IActionResult> CourseSubHeading(
        int courseId,
        [RegularExpression(EntitiesConstants.SubHeadingPattern)]
        string beginCode
    )
    {
        var patternSubheadingCode = beginCode.GetCodeLike();

        var result = await _context
            .CourseHeadings
            .AsNoTracking()
            .Where(e => e.CourseId == courseId)
            .Select(e => e.Heading)
            .Where(e => EF.Functions.Like(e.Code, patternSubheadingCode))
            .ProjectTo<HeadingItemInCourseDto>(_configurationProvider)
            .ToListAsync();

        return Json(result);
    }

    /// <summary>
    ///     Change heading in course (include or exclude)
    /// </summary>
    /// <param name="headingItemCheckingDto">
    ///     Contains the rubric and course ID and a field indicating whether or not to include the rubric in the course
    /// </param>
    /// <returns></returns>
    /// <response code="200">The rubric has been successfully changed</response>
    /// <response code="404">The rubric or course was not found</response>
    [HttpPost]
    [Route("course/heading/include")]
    public async Task<IActionResult> ChangeHeadingInCourse(HeadingItemCheckingDto headingItemCheckingDto)
    {
        var isValid = _context.Courses.Any(c => c.Id == headingItemCheckingDto.CourseId) &&
                      _context.Headings.Any(h => h.Id == headingItemCheckingDto.HeadingId);

        if (!isValid)
            return NotFound();


        if (headingItemCheckingDto.IsIncluded)
        {
            var courseHeading = new CourseHeading
            {
                CourseId = headingItemCheckingDto.CourseId,
                HeadingId = headingItemCheckingDto.HeadingId
            };

            await _context.CourseHeadings.AddAsync(courseHeading);
            await _context.SaveChangesAsync();

            if (courseHeading.Id == 0)
                return BadRequest();
        }
        else
        {
            var courseHeading = await _context.CourseHeadings
                .FirstOrDefaultAsync(ch =>
                    ch.CourseId == headingItemCheckingDto.CourseId && ch.HeadingId == headingItemCheckingDto.HeadingId);

            if (courseHeading == null)
                return Ok();
            _context.CourseHeadings.Remove(courseHeading);
            await _context.SaveChangesAsync();
        }

        return Ok();
    }
}