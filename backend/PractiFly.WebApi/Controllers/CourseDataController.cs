﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Dto.Admin.UserView;
using PractiFly.WebApi.Dto.CourseData;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.Controllers;

[Route("api/")]
[ApiController]
public class CourseDataController : Controller
{
    private readonly IAmazonS3ClientManager _amazonClient;
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IPractiflyContext _context;
    private readonly IMapper _mapper;

    public CourseDataController(
        IPractiflyContext context,
        IConfigurationProvider configurationProvider,
        IMapper mapper,
        IAmazonS3ClientManager amazonClient
    )
    {
        _context = context;
        _configurationProvider = configurationProvider;
        _mapper = mapper;
        _amazonClient = amazonClient;
    }


    /// <summary>
    ///     Retrieves an array of courses associated with a owner (user) identified by the specified Id, or all courses if no
    ///     Id is provided.
    /// </summary>
    /// <param name="ownerId">Id of the owner (user).</param>
    /// <response code="200">Courses representation was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No courses found.</response>
    /// <returns>A JSON-encoded representation of the array of courses.</returns>
    [HttpGet]
    [Route("course/all")]
    public async Task<IActionResult> Courses(int? ownerId = null)
    {
        var query = _context.Courses.AsNoTracking();

        if (ownerId.HasValue)
            query = query.Where(e => e.OwnerId == ownerId);

        var courseItemDtos = await query
            .ProjectTo<CourseItemDto>(_configurationProvider)
            .ToListAsync();

        return Json(courseItemDtos);
    }

    [HttpGet]
    [Route("teacher/course/all")]
    [Authorize(Roles = $"{UserRoles.Teacher}, {UserRoles.Admin}, {UserRoles.Manager}",
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetMyOwnerCourseInfo()
    {
        var id = User.GetUserIdInt();

        return await Courses(id);
    }


    /// <summary>
    ///     Returns a list of course information.
    /// </summary>
    /// <response code="200">Getting course information was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No courses found.</response>
    /// <returns>A JSON-encoded representation of the list of course information.</returns>
    [HttpGet]
    [Route("course/info")]
    public async Task<IActionResult> GetCourseInfo(int courseId)
    {
        var result = await _context
            .Courses
            .AsNoTracking()
            //.Include(e => )
            .Where(e => e.Id == courseId)
            .ProjectTo<CourseFullInfoDto>(_configurationProvider)
            .FirstOrDefaultAsync();
        return Json(result);
    }


    /// <summary>
    ///     Returns a list of users who are enrolled in the course identified by the specified Id.
    /// </summary>
    /// <param name="courseId">Id of the course.</param>
    /// <response code="200">Getting users of course was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No users found.</response>
    /// <returns>A JSON-encoded representation of the list of users.</returns>
    [HttpGet]
    [Route("course/users")]
    public async Task<IActionResult> GetUsersOfCourse(int courseId)
    {
        var result = await _context.UserCourses
            .Where(e => e.CourseId == courseId)
            .Select(e => e.User)
            .ProjectTo<UserFullnameItemDto>(_configurationProvider)
            .ToListAsync();
        return Json(result);
    }

    /// <summary>
    ///     Returns the owner information for a course identified by the specified courseId.
    /// </summary>
    /// <param name="courseId">Id of the course.</param>
    /// <response code="200">Getting owner of course was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No owner found.</response>
    /// <returns>A JSON-encoded representation of the owner information.</returns>
    [HttpGet]
    [Route("course/owner")]
    public async Task<IActionResult> GetOwnerOfCourse(int courseId)
    {
        var result = await _context.Courses
            .Where(e => e.Id == courseId)
            .Select(e => e.Owner)
            .ProjectTo<OwnerInfoDto>(_configurationProvider, new { baseUrl = _amazonClient.GetFileUrl() })
            .FirstOrDefaultAsync();

        return Json(result);
    }


    /// <summary>
    ///     Creates a new course using the provided course data.
    /// </summary>
    /// <param name="courseDto">A data transfer object containing the course information.</param>
    /// <response code="200">Creating of course was successful.</response>
    /// <response code="400">Creating of course was failed.</response>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [HttpPost]
    [Route("course")]
    //TODO: Create owner
    public async Task<IActionResult> CreateCourse(CreateCourseDto courseDto)
    {
        var course = _mapper.Map<CreateCourseDto, Course>(courseDto);
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        if (course.Id == 0) return BadRequest();

        var result = _mapper.Map<Course, CourseInfoDto>(course,
            opts => opts.AfterMap((_, dest) => dest.Language = courseDto.Language));

        return Ok(result);
    }

    /// <summary>
    ///     Updates the specified course with the details provided in the <paramref name="courseDto" /> object.
    /// </summary>
    /// <param name="courseDto">A data transfer object containing the course information.</param>
    /// <response code="200">Editing course is successful.</response>
    /// <response code="400">Editing course was failed.</response>
    /// <response code="404">Not Found response.</response>
    /// <returns>Returns HTTP status response.</returns>
    [HttpPost]
    [Route("course/edit")]
    //TODO: Owner edits his course
    public async Task<IActionResult> EditCourse(EditCourseDto courseDto)
    {
        var course = await _context
            .Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == courseDto.Id);

        if (course == null) return NotFound();

        course.Language = _context.Languages.First(l => l.Code == courseDto.Language);
        course.Name = courseDto.Name;
        course.Note = courseDto.Note;
        course.Description = courseDto.Description;

        _context.Courses.Update(course);

        await _context.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    ///     Deletes a course identified by the specified Id.
    /// </summary>
    /// <param name="courseId">ID of the course to be deleted.</param>
    /// <response code="200">Course was deleted succesfully.</response>
    /// <response code="400">Deleting of course was failed.</response>
    /// <response code="404">Not found course to delete.</response>
    /// <returns>An HTTP response status code.</returns>
    [HttpDelete]
    [Route("course")]
    // TODO: Owner deletes his course
    public async Task<IActionResult> DeleteCourse(int courseId)
    {
        var course = await _context
            .Courses
            .AnyAsync(e => e.Id == courseId);

        if (!course)
            return NotFound();

        _context.Courses.Remove(new Course { Id = courseId });

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost]
    [Route("course/add-users")]
    public async Task<ActionResult> AddUserToCourse(CourseUsersDto userDto)
    {
        if (!await _context
                .Users
                .AnyAsync(e => e.Id == userDto.UserId))
            return NotFound("A user with such an ID does not exist");
        if (!await _context
                .Courses
                .AnyAsync(e => e.Id == userDto.CourseId))
            return NotFound("A course with such an ID does not exist");

        var courseUsers = _mapper.Map<CourseUsersDto, UserCourse>(userDto);
        //TODO: NumberTheme
        courseUsers.LastThemeId = 1;
        //courseUsers.LastTheme.Number = 1;
        await _context.UserCourses.AddAsync(courseUsers);
        await _context.SaveChangesAsync();
        return courseUsers.Id == 0 ? BadRequest() : Ok();
    }
    //public async Task<IActionResult> AddMaterialToTheme(ThemeMaterialCreateDto themeMaterialDto)
    //{
    //    if (await _context
    //            .ThemeMaterials
    //            .AnyAsync(e => e.ThemeId == themeMaterialDto.ThemeId
    //                           && e.MaterialId == themeMaterialDto.MaterialId))
    //        return BadRequest();

    //    var themeMaterial = _mapper.Map<ThemeMaterialCreateDto, ThemeMaterial>(themeMaterialDto);

    //    await _context.ThemeMaterials.AddAsync(themeMaterial);
    //    await _context.SaveChangesAsync();

    //    if (themeMaterial.Id == 0) return BadRequest();

    //    var themeMaterialInfoDto = _mapper.Map<ThemeMaterial, ThemeMaterialInfoDto>(themeMaterial);

    //    return Json(themeMaterialInfoDto);
    //}


    /*
        var result = await _context
        .Courses
        .AsNoTracking()
        .Where(c => c.Id == courseId)
        .Select(c => new
        {
            c.Id,
            c.Name,
            Themes = _context.Themes
                .AsNoTracking()
                .Where(t => t.CourseId == courseId)
                .Select(t => new
                {
                    Theme = t,
                    UserTheme = _context
                        .UserThemes
                        .FirstOrDefault(ut => ut.UserId == userId && ut.ThemeId == t.Id)
                })
                .Select(obj => new
                {
                    Id = obj.Theme.Id,
                    Name = obj.Theme.Name,
                    IsComplited = obj.UserTheme != null && obj.UserTheme.IsCompleted,
                    Grade = obj.UserTheme == null ? 0 : obj.UserTheme.Grade,
                    Materials = _context
                        .ThemeMaterials
                        .Where(tm => tm.ThemeId == obj.Theme.Id)
                        .Select(tm => tm.Material)
                        .Select(m => new
                        {
                            Material = m,
                            UserMaterial = _context
                                .UserMaterials
                                .FirstOrDefault(um => um.UserId == userId && um.MaterialId == m.Id)
                        })
                        .Select(m => new
                        {
                            Id = m.Material.Id,
                            Name = m.Material.Name,
                            IsComplited = m.UserMaterial != null && m.UserMaterial.IsCompleted,
                            Grade = m.UserMaterial == null ? 0 : m.UserMaterial.Grade,
                        })
                        .ToArray()
                }).ToArray()
        })
        .FirstOrDefaultAsync();*/
    /**/

    //return result == null ? BadRequest() : Json(result);
    //}
}