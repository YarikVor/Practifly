using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.AutoMapper.Ex;
using PractiFly.WebApi.Dto.CourseDetails;
using PractiFly.WebApi.Dto.Heading;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("api")]
public class CourseDetailsController : Controller
{
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IPractiflyContext _context;
    private readonly IMapper _mapper;


    public CourseDetailsController(
        IPractiflyContext context,
        IConfigurationProvider configurationProvider,
        IMapper mapper
    )
    {
        _context = context;
        _configurationProvider = configurationProvider;
        _mapper = mapper;
    }

    /// <summary>
    ///     Retrieves a list of themes associated with a course identified
    ///     by the specified courseId, as well as information about whether
    ///     or not each theme has been completed by the current user.
    /// </summary>
    /// <param name="courseId">Id of the course.</param>
    /// <response code="200">Getting themes in user course was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No themes found.</response>
    /// <returns>A JSON-encoded representation of the list of themes, with completion information included for each theme.</returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("user/course/themes")]
    public async Task<IActionResult> GetThemesInUserCourse(int courseId)
    {
        var userId = User.GetUserIdInt();

        var userCourse = await _context
            .UserCourses
            .AnyAsync(e => e.UserId == userId && e.CourseId == courseId);

        if (!userCourse)
            return NotFound();

        var themes = await _context
            .Themes
            .Where(e => e.CourseId == courseId)
            .OrderBy(e => e.Number)
            .ProjectTo<CourseThemeItemDto>(_configurationProvider)
            .ToListAsync();

        return Json(themes);
    }

    /// <summary>
    ///     Retrieves a list of materials associated with a user and theme identified by the specified Id.
    /// </summary>
    /// <param name="themeId">Id of the theme.</param>
    /// <response code="200">Getting materials in user themes was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No materials found.</response>
    /// <returns>A JSON-encoded representation of the list of materials associated with the user and theme.</returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("user/course/theme/material")]
    public async Task<IActionResult> GetMaterialsInUserThemes(int themeId)
    {
        var userId = User.GetUserIdInt();
        var result = await _context
            .UserThemes
            .Where(ut => ut.UserId == userId && ut.ThemeId == themeId)
            .ProjectTo<ThemeWithMaterialsDto>(_configurationProvider)
            .FirstOrDefaultAsync();

        return result == null ? NotFound() : Json(result);
    }

    /// <summary>
    ///     Returns details about a material identified by the specified Id`s.
    /// </summary>
    /// <param name="themeId">Id of the theme.</param>
    /// <param name="materialId">Id of the material.</param>
    /// <response code="200">Getting information of material was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No materials found.</response>
    /// <returns>A JSON-encoded representation of the material details.</returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("theme/material")]
    public async Task<IActionResult> GetMaterialInfo(int themeId, int materialId)
    {
        var material = await _context
            .ThemeMaterials
            .Where(tm => tm.MaterialId == materialId && tm.ThemeId == themeId)
            .ProjectTo<MaterialDetailsViewDto>(_configurationProvider)
            .FirstOrDefaultAsync();

        if (material == null)
            return NotFound();

        return Json(material);
    }

    /// <summary>
    ///     Returns information about the user's progress in a specific material.
    /// </summary>
    /// <param name="materialId">Id of the material.</param>
    /// <response code="200">Getting user information in material was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No material found.</response>
    /// <returns>A JSON-encoded representation of the user's progress information.</returns>
    //TODO: Можливо матеріал міститься лише в одній темі (1:1)
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("user/material/status")]
    public async Task<IActionResult> GetUserInfoInMaterial(int materialId)
    {
        var userId = User.GetUserIdInt();
        //TODO: return url
        var userMaterial = await _context
            .UserMaterials
            .Where(e => e.UserId == userId && e.MaterialId == materialId)
            .ProjectTo<CourseMaterialItemDto>(_configurationProvider)
            .FirstOrDefaultAsync();

        return userMaterial == null ? NotFound() : Json(userMaterial);
    }

    /// <summary>
    ///     Sets information about a user's progress on a specified material identified by the materialId parameter.
    /// </summary>
    /// <param name="dto">A Data Transfer Object which containing information about the user's progress on the material.</param>
    /// <returns>
    ///     Returns an IActionResult that represents the result of the operation.
    /// </returns>
    /// <response code="200">Operation is successful.</response>
    /// <response code="404">The specified user material does not exist.</response>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("user/material/status")]
    public async Task<IActionResult> SetMaterialInfo(UserMaterialSendDto dto)
    {
        var userId = User.GetUserIdInt();

        var userMaterial = await _context
            .UserMaterials
            .Where(e => e.UserId == userId && e.MaterialId == dto.Id)
            .FirstOrDefaultAsync();

        if (userMaterial == null)
        {
            //TODO: Check if materialId is not included in the themes (or UserThemes or UserCourses)

            var createUserMaterial =
                _mapper.Map<UserMaterialSendDto, UserMaterial>(dto);

            createUserMaterial.UserId = userId;

            await _context.UserMaterials.AddAsync(createUserMaterial);
            await _context.SaveChangesAsync();

            if (createUserMaterial.Id == 0) return Problem();

            return Ok();
        }

        userMaterial.EditData(dto);
        await _context.SaveChangesAsync();

        return Ok();
    }
    /// <summary>
    ///  Returns advanced information about the course, including information. its themes and their materials
    /// </summary>
    /// <param name="courseId">Id of the course about which information is received</param>
    /// <returns></returns>
    /// <response code="200">Operation is successful.</response>
    /// <response code="400">Operation was failed</response>

    [HttpGet]
    [Route("course/themes/full-info")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetUserCourseFullInfo(int courseId)
    {
        var userId = User.GetUserIdInt();

        if (!await _context.UserCourses.AnyAsync(uc => uc.CourseId == courseId && uc.UserId == userId))
            return NotFound("A user with such an ID does not exist");
        var result = await _context
            .Courses
            .AsNoTracking()
            .Where(c => c.Id == courseId)
            .ProjectTo<UserCourseInfoDto>(_configurationProvider, new { userId })

            .FirstOrDefaultAsync();
        return result == null ? BadRequest() : Json(result);
    }
    }
