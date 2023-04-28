using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.WebApi.Dto.CourseDetails;
using PractiFly.WebApi.Dto.CourseThemes;

namespace PractiFly.WebApi.Controllers;

public class CourseDetailsController : Controller
{
    private readonly IPractiflyContext _context;
    
    public CourseDetailsController(IPractiflyContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a list of themes associated with a course identified 
    /// by the specified courseId, as well as information about whether 
    /// or not each theme has been completed by the current user.
    /// </summary>
    /// <param name="courseId">Id of the course.</param>
    /// <returns>A JSON-encoded representation of the list of themes, with completion information included for each theme.</returns>
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetThemesInUserCourse(int courseId)
    {
        var userId = User.GetUserIdInt();
        
        var userCourse = await _context
            .UserCourses
            .AnyAsync(e => e.UserId == userId && e.CourseId == courseId);
        
        if(userCourse == false)
            return NotFound();

        var themes = await _context
            .Themes
            .Where(e => e.CourseId == courseId)
            .Select(e => new CourseThemeItemDto()
            {
                Id = e.Id,
                Name = e.Name,
                IsCompleted = _context
                    .UserThemes
                    .Where(ut => ut.ThemeId == e.Id)
                    .Select(ut => ut.IsCompleted)
                    .FirstOrDefault()
            })
            .ToListAsync();
        
        return Json(themes);
    }

    /// <summary>
    /// Retrieves a list of materials associated with a user and theme identified by the specified Id.
    /// </summary>
    /// <param name="themeId">Id of the theme.</param>
    /// <returns>A JSON-encoded representation of the list of materials associated with the user and theme.</returns>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetMaterialsInUserThemes(int themeId)
    {
        var userId = User.GetUserIdInt();
        
        var userTheme = await _context
            .UserThemes
            .AnyAsync(e => e.UserId == userId && e.ThemeId == themeId);
        
        if(userTheme == false)
            return NotFound();

        var result = new CourseThemeWithMaterialsDto();
        
        
        result.Materials = await _context
            .UserMaterials
            .Where(um => um.UserId == userId)
            .Where(um => _context
                .ThemeMaterials
                .Any(tm => tm.ThemeId == themeId
                           && tm.MaterialId == um.MaterialId)
            )
            .Select(um => new CourseMaterialItemDto()
            {
                Id = um.MaterialId,
                Name = um.Material.Name,
                IsCompleted = um.IsCompleted,
                Grade = um.Grade
            })
            .ToArrayAsync();
        
        return Json(result);
    }

    /// <summary>
    /// Returns details about a material identified by the specified Id`s.
    /// </summary>
    /// <param name="themeId">Id of the theme.</param>
    /// <param name="materialId">Id of the material.</param>
    /// <returns>A JSON-encoded representation of the material details.</returns>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetMaterialInfo(int themeId, int materialId)
    {
        var material = await _context
            .Materials
            .Where(e => e.Id == materialId)
            .Select(e => new MaterialDetailsViewDto()
            {
                Id = e.Id,
                Name = e.Name,
                Url = e.Url,
                Description = _context
                    .ThemeMaterials
                    .Where(e => e.MaterialId == materialId
                                && e.ThemeId == themeId)
                    .Select(e => e.Description)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();

        if (material == null)
            return NotFound();
        
        return Json(material);
    }

    /// <summary>
    /// Returns information about the user's progress in a specific material.
    /// </summary>
    /// <param name="materialId">Id of the material.</param>
    /// <returns>A JSON-encoded representation of the user's progress information.</returns>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetUserInfoInMaterial(int materialId)
    {
        var userId = User.GetUserIdInt();
        
        var userMaterial = await _context
            .UserMaterials
            .Where(e => e.UserId == userId && e.MaterialId == materialId)
            .Select(e => new UserMaterialInfoDto()
            {
                Grade = e.Grade,
                IsCompleted = e.IsCompleted,
                ResultUrl = e.ResultUrl
                
            }
            )
            .FirstOrDefaultAsync();

        return userMaterial == null ? NotFound() : Json(userMaterial);
    }

    /// <summary>
    /// Sets information about a user's progress on a specified material identified by the materialId parameter.
    /// </summary>
    /// <param name="materialId">Id of the material.</param>
    /// <param name="dto">A Data Transfer Object which containing information about the user's progress on the material.</param>
    /// <returns>
    /// Returns an IActionResult that represents the result of the operation. 
    /// </returns>
    /// <response code="200">Operation is successful, HTTP OK status code is returned.</response>
    /// <response code="404">The specified user material does not exist, HTTP 404 Not Found status code is returned.</response>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> SetMaterialInfo(int materialId, UserMaterialInfoDto dto)
    {
        var userId = User.GetUserIdInt();
        
        var userMaterial = await _context
            .UserMaterials
            .Where(e => e.UserId == userId && e.MaterialId == materialId)
            .FirstOrDefaultAsync();

        if (userMaterial == null)
            return NotFound();
        
        userMaterial.IsCompleted = dto.IsCompleted;
        userMaterial.ResultUrl = dto.ResultUrl;

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