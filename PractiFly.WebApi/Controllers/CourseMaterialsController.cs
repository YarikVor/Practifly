﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.WebApi.Dto.CourseMaterials;
using PractiFly.WebApi.Dto.MaterialBlocks;

namespace PractiFly.WebApi.Controllers;

[Route("api")]
[ApiController]
//TODO: CourseMAterialController
public class CourseMaterialsController : Controller
{
    private readonly IPractiflyContext _context;
    private readonly IMapper _mapper;

    public CourseMaterialsController(IPractiflyContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
        //TODO: Mapper: Heading -> CourseHeadingInfoDto
        var result = await _context.CourseHeadings
            .AsNoTracking()
            .Where(e => e.CourseId == courseId)
            .Select(e => new CourseHeadingInfoDto
            {
                Id = e.Heading.Id,
                Code = e.Heading.Code,
                Name = e.Heading.Name
            })
            .ToListAsync();

        return Json(result);
    }

    /// <summary>
    ///     A method for extracting rubric materials and including them in the course
    /// </summary>
    /// <param name="headingId">The ID of the rubric from which the materials are obtained</param>
    /// <returns></returns>
    /// <response code="200">The rubric materials are returned</response>
    [HttpGet]
    [Route("course/heading/materials")]
    //TODO: Set heading by id or code (Nullable<int>)
    //public async Task<IActionResult> GetMaterialAndBlocksForInclusion(int materialId)
    public async Task<IActionResult> GetMaterialForInclusion(int headingId, int courseId)
    {
        //TODO: Mapper: HeadingMaterials -> MaterialForInclusionDto
        var result = await _context
            .HeadingMaterials
            .AsNoTracking()
            .Where(e => e.Id == headingId /*TODO:*/)
            //.ProjectTo<MaterialForInclusionDto>(_mapper.ConfigurationProvider)
            .Select(e => new MaterialForInclusionDto
            {
                IsIncluded = _context
                    .CourseMaterials
                    .Any(cm => cm.MaterialId == e.Id),
                PriorityLevel = _context
                    .CourseMaterials
                    .Where(cm => cm.MaterialId == e.Id)
                    .Select(cm => cm.PriorityLevel)
                    .First()
            })
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