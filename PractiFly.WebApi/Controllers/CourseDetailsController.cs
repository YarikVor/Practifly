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

    public async Task<IActionResult> GetMaterialInfo(int themeId, int materialId)
    {
        var material = _context
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
            .FirstOrDefault();

        if (material == null)
            return NotFound();
        
        return Json(material);
    }

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