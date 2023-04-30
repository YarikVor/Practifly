using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.WebApi.Dto.Heading;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("api")]
public class HeadingCourseController: Controller
{
    private readonly IPractiflyContext _context;
    
    public HeadingCourseController(IPractiflyContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get headings by begin heading code
    /// </summary>
    /// <param name="beginCode">
    /// The beginning of a section of code that returns sub-sections (ex: 12 -> 12.__, where '_' - 0-9).
    /// Supports three rubric levels (ex: "", "12", "12.12", "12.12.12")
    /// </param>
    /// <returns></returns>
    /// <response code="200">Return subheading info (maybe empty result)</response>
    [HttpGet]
    [Route("heading/sub")]
    public async Task<IActionResult> GetSubheading([RegularExpression(@"^(?:\d{2})?(?:\.\d{2}){0,2}$")] string? beginCode = "")
    {
        string patternSubRubricCode = string.IsNullOrEmpty(beginCode) 
            ? "__" 
            : $"{beginCode}.__";
        
        var result = await _context.Headings
            .AsNoTracking()
            .Where(e => EF.Functions.Like(e.Code, patternSubRubricCode))
            .Select(e => new HeadingItemDto()
            {
                Code = e.Code,
                Name = e.Name,
                Id = e.Id
            })
            .ToArrayAsync();
        
        return Json(result);
    }

    /// <summary>
    /// Returns the subtopics, but adds an IsIncluded field indicating whether it is included in the course.
    /// </summary>
    /// <param name="courseId"> Course identifier used to obtain included and excluded rubrics </param>
    /// <param name="beginCode">
    /// The beginning of a section of code that returns sub-sections (ex: 12 -> 12.__, where '_' - 0-9).
    /// Supports three rubric levels (ex: "", "12", "12.12", "12.12.12")
    /// </param>
    /// <response code="200">Operation was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No courses found.</response>
    /// <returns></returns>
    [HttpGet]
    [Route("course/{courseId:int}/heading")]
    public async Task<IActionResult> CourseHeading(int courseId, [RegularExpression(@"^(?:\d{2})?(?:\.\d{2}){0,2}$")] string? beginCode = "")
    {
        string patternSubRubricCode = string.IsNullOrEmpty(beginCode) 
            ? "__" 
            : $"{beginCode}.__";
        
        var result = await _context.Headings
            .AsNoTracking()
            .Where(e => EF.Functions.Like(e.Code, patternSubRubricCode))
            .Select(e => new HeadingItemInCourseDto()
            {
                Code = e.Code,
                Name = e.Name,
                Id = e.Id,
                IsIncluded = _context.CourseHeadings.Any(ch => ch.CourseId == courseId)
            })
            .ToListAsync();
        
        return Json(result);
    }
    
    /// <summary>
    /// Change heading in course (include or exclude)
    /// </summary>
    /// <param name="headingItemCheckingDto">
    /// Contains the rubric and course ID and a field indicating whether or not to include the rubric in the course
    /// </param>
    /// <returns></returns>
    /// <response code="200">The rubric has been successfully changed</response>
    /// <response code="404">The rubric or course was not found</response>
    [HttpPost]
    [Route("course/heading/include")]
    public async Task<IActionResult> ChangeHeadingInCourse(HeadingItemCheckingDto headingItemCheckingDto)
    {
        bool isValid = _context.Courses.Any(c => c.Id == headingItemCheckingDto.CourseId) &&
                       _context.Headings.Any(h => h.Id == headingItemCheckingDto.HeadingId);

        if (!isValid)
            return NotFound();
        
        
        if (headingItemCheckingDto.IsIncluded)
        {
            _context.CourseHeadings.AddAsync(new CourseHeading()
            {
                CourseId = headingItemCheckingDto.CourseId,
                HeadingId = headingItemCheckingDto.HeadingId
            });
        }
        else
        {
            var courseHeading = await _context.CourseHeadings
                .FirstOrDefaultAsync(ch => ch.CourseId == headingItemCheckingDto.CourseId && ch.HeadingId == headingItemCheckingDto.HeadingId);

            if (courseHeading == null) 
                return Ok();
            _context.CourseHeadings.Remove(courseHeading);
            _context.SaveChangesAsync();

        }
        
        return Ok();
    }
    
}