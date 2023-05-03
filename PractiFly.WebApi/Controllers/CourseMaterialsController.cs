using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities;
using PractiFly.WebApi.Dto.CourseMaterials;
using System.ComponentModel.DataAnnotations;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.Controllers;

[Route("api")]
[ApiController]
public class CourseMaterialsController : Controller
{
    private readonly IPractiflyContext _context;
    private readonly IConfigurationProvider _configurationProvider;

    public CourseMaterialsController(IPractiflyContext context, IConfigurationProvider configurationProvider)
    {
        _context = context;
        _configurationProvider = configurationProvider;
    }

    /*/// <summary>
    /// Returns a list of courses for an admin user.
    /// </summary>
    /// <returns>A JSON-encoded representation of the list of courses, including only the course ID and name.</returns>
    [HttpGet]
    [Route("course/all")]
    public async Task<IActionResult> GetCoursesForAdmin()
    {
        var courses = await _context.Courses
        .Select(c => new CourseItemDto
        {
            Id = c.Id,
            Name = c.Name
        })
        .ToListAsync();

        return Json(courses);
    }*/

    /// <summary>
    ///     Returns a list of course headings associated with a course identified by the specified Id.
    /// </summary>
    /// <param name="courseId">Id of the course.</param>
    /// <returns>A JSON-encoded representation of the list of course headings.</returns>
    /// <response code="200"></response>
    [HttpGet]
    [Route("course/headings")]
    public async Task<IActionResult> CourseHeadings(int courseId)
    {
        var result = await _context.CourseHeadings
            .AsNoTracking()
            .Where(e => e.CourseId == courseId)
            .ProjectTo<CourseHeadingInfoDto>(_configurationProvider)
            .ToListAsync();

        return Json(result);
    }

    /// <summary>
    ///     A method for extracting rubric materials and including them in the course.
    /// </summary>
    /// <param name="headingId">The ID of the rubric from which the materials are obtained.</param>
    /// <param name="code">Code pattern.</param>
    /// <param name="courseId">Id of the course.</param>
    /// <returns></returns>
    /// <response code="200">The rubric materials are returned.</response>
    [HttpGet]
    [Route("course/heading/materials")]
    public async Task<IActionResult> GetMaterialForInclusion(int? headingId, [RegularExpression(EntitiesConstants.HeadingPattern)] string code, int courseId)
    {
        var query = _context.HeadingMaterials.AsNoTracking();

        if (headingId.HasValue)
        {
            query = query.Where(e => e.Id == headingId.Value);
        }
        else if (!string.IsNullOrEmpty(code))
        {
            query = query.Where(e => e.Heading.Code == code);
        }
        else return BadRequest();

        var result = await query
            .ProjectTo<MaterialForInclusionDto>(_configurationProvider, new { courseId })
            .OrderBy(e => e.PriorityLevel)
            .ToListAsync();

        return Json(result);
    }

    /// <summary>
    ///     Returns a list of course headings associated with a course identified by the specified courseId.
    /// </summary>
    /// <param name="materialId">Id of the material.</param>
    /// <returns>A JSON-encoded representation of the list of course headings.</returns>
    /*[HttpGet]
    [Route("material")]
    public async Task<IActionResult> GetMaterialsFromHeading(int headingId)
    {
        //TODO: Select from HeadingMaterials and get Materials
        var result = await _context
            .Materials
            .AsNoTracking()
            .Where(e => e.Id == headingId)
            //.ProjectTo<MaterialBlocksDto>(_mapper.ConfigurationProvider)
            .ProjectTo<MaterialBlockItemDto>(_mapper.ConfigurationProvider)
            .OrderBy(e => e.Id)
            .ToListAsync();

        return Json(result);
    }*/
}