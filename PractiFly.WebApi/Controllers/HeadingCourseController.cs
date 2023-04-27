using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.WebApi.Dto.Heading;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class HeadingCourseController: Controller
{
    private readonly IPractiflyContext _context;
    
    public HeadingCourseController(IPractiflyContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("heading")]
    public async Task<IActionResult> Headings([RegularExpression(@"^(?:\d{2})?(?:\.\d{2}){0,2}$")] string beginRubricCode)
    {
        string patternSubRubricCode = $"{beginRubricCode}.__";
        
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
    
    [HttpGet]
    [Route("course/{courseId:int}/heading")]
    public async Task<IActionResult> CourseHeading(int courseId, [RegularExpression(@"^(?:\d{2})?(?:\.\d{2}){0,2}$")] string beginRubricCode)
    {
        string patternSubRubricCode = string.IsNullOrEmpty(beginRubricCode) 
            ? "__" 
            : $"{beginRubricCode}.__";
        
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
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> ChangeHeadingInCourse(HeadingItemCheckingDto headingItemCheckingDto)
    {
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