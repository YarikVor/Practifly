using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.AutoMapper;
using PractiFly.WebApi.Dto.CourseDetails;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;


namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("api")]
public class CourseDetailsController : Controller
{
    private readonly IPractiflyContext _context;
    private readonly IConfigurationProvider _configurationProvider;
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
    //TODO: Check Route.
    [HttpGet]
    [Route("user/course/themes")]
    public async Task<IActionResult> GetThemesInUserCourse(int courseId)
    {
        var userId =  User.GetUserIdInt();

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
    [Route("user/course/theme/material")]
    public async Task<IActionResult> GetMaterialsInUserThemes(int themeId)
    {
        var userId =  User.GetUserIdInt();

        //TODO: Mapper
        //?

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
    [Route("theme/material")]
    public async Task<IActionResult> GetMaterialInfo(int themeId, int materialId)
    {
        //TODO: Mapper (foregin parametr)
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
    [Route("user/material/status")]
    public async Task<IActionResult> GetUserInfoInMaterial(int materialId)
    {
        var userId = User.GetUserIdInt();

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
    /// <param name="materialId">Id of the material.</param>
    /// <param name="dto">A Data Transfer Object which containing information about the user's progress on the material.</param>
    /// <returns>
    ///     Returns an IActionResult that represents the result of the operation.
    /// </returns>
    /// <response code="200">Operation is successful.</response>
    /// <response code="404">The specified user material does not exist.</response>
    [HttpPost]
    [Route("user/material/status")]
    public async Task<IActionResult> SetMaterialInfo(UserMaterialSendDto dto)
    {
        var userId = 2;

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
            
            if (createUserMaterial.Id == 0)
            {
                return Problem();
            }

            return Ok();
        }

        userMaterial.EditData(dto);
        await _context.SaveChangesAsync();

        return Ok();
    }
}

public static class ClaimsPrincipalEx
{
    public static string GetUserId(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public static int GetUserIdInt(this ClaimsPrincipal principal)
    {
        return int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}