using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.WebApi.Dto.CourseThemes;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.Controllers;

[Route("api")]
[ApiController]
public class CourseThemesController : Controller
{
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IPractiflyContext _context;
    private readonly IMapper _mapper;

    public CourseThemesController(
        IPractiflyContext context,
        IMapper mapper,
        IConfigurationProvider configurationProvider
    )
    {
        _context = context;
        _mapper = mapper;
        _configurationProvider = configurationProvider;
    }


    /// <summary>
    /// Returns an information of theme associated with a course identified by the special Id.
    /// </summary>
    /// <param name="courseId">ID of the course.</param>
    /// <response code="200">Getting information of theme was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No themes found.</response>
    /// <returns>A JSON-encoded representation of the information of course theme.</returns>
    /*[HttpGet]
    [Route("course/themes/notunderstand")]

    //метод, повертає список тем в курсі
    public async Task<IActionResult> GetCourseThemes(int courseId)
    {
        var course = await _context
            .Courses
            .Where(e => e.Id == courseId)
            .Select(e => new CourseItemWithThemeDto()
            {
                Id = e.Id,
                Name = e.Name,
            })
            .FirstOrDefaultAsync();

        if (course == null)
            return NotFound();

        course.Themes = _context.Themes
            .AsNoTracking()
            .Where(e => e.CourseId == courseId)
            .ProjectToArray<ThemeItemDto>();
        
        
        /*
        var result = await _context
            .Courses
            .AsNoTracking()
            .Where(e => e.Id == courseId)
            .ProjectTo<CourseItemWithThemeDto>(_mapper.ConfigurationProvider)
            .FirstAsync();#1#

        return Json(1);
    }*/

    /// <summary>
    ///     Returns a list of themes associated with a course identified by the specified Id.
    /// </summary>
    /// <param name="courseId">ID of the course.</param>
    /// <response code="200">Getting list themes from course was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No themes found.</response>
    /// <returns>
    ///     An HTTP response indicating success and a
    ///     JSON-encoded representation of the list of themes,
    ///     or a "Not Found" error if the specified ID does not exist.
    /// </returns>
    [HttpGet]
    [Route("course/themes")]
    public async Task<IActionResult> GetThemesFromCourse(int courseId)
    {
        if (!await _context.Courses.AnyAsync(e => e.Id == courseId)) return NotFound();

        var themes = await _context
            .Themes
            .AsNoTracking()
            .Where(t => t.CourseId == courseId)
            .ProjectTo<ThemeItemDto>(_configurationProvider)
            .ToListAsync();

        return Ok(themes);
    }

    /// <summary>
    ///     Returns a list of materials associated with a material identified by the specified Id.
    /// </summary>
    /// <param name="courseId">ID of the course.</param>
    /// <response code="200">Getting materials was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No materials found.</response>
    /// <returns>A JSON-encoded representation of the list of materials.</returns>
    //TODO: ViewMaterialsList
    [HttpGet]
    [Route("course/materials")]
    public async Task<IActionResult> GetMaterialsFromCourse(int courseId)
    {
        var result = await _context
            .CourseMaterials
            .AsNoTracking()
            .Where(e => e.CourseId == courseId)
            .ProjectTo<MaterialsMenuDto>(_configurationProvider)
            .OrderBy(e => e.PriorityLevel)
            .ToListAsync();

        return Json(result);
    }

    /*/// <summary>
    /// Returns a list of courses associated with a course identified by the specified Id.
    /// </summary>
    /// <param name="courseId">ID of the course.</param>
    /// <response code="200">Getting courses list was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No courses found.</response>
    /// <returns>A JSON-encoded representation of the list of courses.</returns>
    // TODO: Цей метод поверне лише один курс, а не список курсів. ПЕРЕПРОВІРИТИ
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetCoursesList(int courseId)
    {
        var result = await _context
            .Courses
            .AsNoTracking()
            .Where(e => e.Id == courseId)
            .ProjectTo<CourseItemDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return Json(result);
    }*/

    /// <summary>
    ///     Returns information about a specific theme identified by the specified Id.
    /// </summary>
    /// <param name="themeId">Id of the theme.</param>
    /// <response code="200">Getting information of course theme was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No theme found.</response>
    /// <returns>
    ///     An HTTP response indicating success and a JSON-encoded representation
    ///     of the theme information, or a "Not Found" error if the specified theme ID does not exist.
    /// </returns>
    [HttpGet]
    [Route("theme")]
    public async Task<IActionResult> ThemeInfo(int themeId)
    {
        var result = await _context
            .Themes
            .AsNoTracking()
            .Where(e => e.Id == themeId)
            .ProjectTo<ThemeInfoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return result == null ? NotFound() : Json(result);
    }

    /// <summary>
    ///     Updates an existing theme identified by the specified Id with the specified Data Transfer Object.
    /// </summary>
    /// <param name="themeDto">The updated theme data as a JSON-encoded ThemeDto object.</param>
    /// <response code="200">Update theme was successful.</response>
    /// <response code="400">Update was failed.</response>
    /// <returns>An HTTP response indicating success or failure of the update operation.</returns>
    [HttpPost]
    [Route("theme/edit")]
    public async Task<IActionResult> UpdateTheme(ThemeEditDto themeDto)
    {
        var theme = await _context.Themes.FirstOrDefaultAsync(e => e.Id == themeDto.Id);

        if (theme == null) return NotFound();

        theme.Name = themeDto.Name;
        theme.Number = themeDto.Number;
        theme.LevelId = themeDto.LevelId;
        theme.Note = themeDto.Note;
        theme.Description = themeDto.Description;

        _context.Themes.Update(theme);
        await _context.SaveChangesAsync();

        return Ok();
    }

    //TODO: метод для перегляду всіх курсів наявний в CourseController.UserCourse

    /// <summary>
    ///     Method for create themes of course.
    /// </summary>
    /// <param name="themeDto">A data transfer object containing the properties of the theme.</param>
    /// <response code="200">Theme created successfully.</response>
    /// <response code="400">Operation was failed.</response>
    /// <returns></returns>
    [HttpPost]
    [Route("theme")]
    public async Task<IActionResult> CreateTheme(ThemeCreateDto themeDto)
    {
        var theme = _mapper.Map<ThemeCreateDto, Theme>(themeDto);

        _context.Themes.Add(theme);
        await _context.SaveChangesAsync();

        if (theme.Id == 0)
            return BadRequest();

        var themeInfoDto = _mapper.Map<Theme, ThemeInfoDto>(theme);


        return Json(themeInfoDto);
    }

    /// <summary>
    ///     Method for delete themes.
    /// </summary>
    /// <param name="themeId">Id of theme to delete.</param>
    /// <response code="200">Theme deleted successfully.</response>
    /// <response code="404">Theme not found.</response>
    /// <returns></returns>
    [HttpDelete]
    [Route("theme")]
    public async Task<IActionResult> DeleteTheme(int themeId)
    {
        var theme = await _context.Themes.FindAsync(themeId);

        if (theme == null)
            return NotFound();

        _context.Themes.Remove(theme);
        await _context.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    ///     Adds a new material to a theme and saves it to the database.
    /// </summary>
    /// <param name="themeMaterialDto">The DTO containing information about the material and theme.</param>
    /// <response code="200">Operation was successful.</response>
    /// <response code="400">Adding material to theme was failed.</response>
    /// <returns>A JSON-encoded representation of the newly created theme-material relation.</returns>
    [HttpPost]
    [Route("theme_material")]
    public async Task<IActionResult> AddMaterialToTheme(ThemeMaterialCreateDto themeMaterialDto)
    {
        if (await _context
                .ThemeMaterials
                .AnyAsync(e => e.ThemeId == themeMaterialDto.ThemeId
                               && e.MaterialId == themeMaterialDto.MaterialId))
            return BadRequest();

        var themeMaterial = _mapper.Map<ThemeMaterialCreateDto, ThemeMaterial>(themeMaterialDto);

        await _context.ThemeMaterials.AddAsync(themeMaterial);
        await _context.SaveChangesAsync();

        if (themeMaterial.Id == 0) return BadRequest();

        var themeMaterialInfoDto = _mapper.Map<ThemeMaterial, ThemeMaterialInfoDto>(themeMaterial);

        return Json(themeMaterialInfoDto);
    }

    /// <summary>
    ///     Edits the properties of a theme material specified in the ThemeMaterialEditDto.
    /// </summary>
    /// <param name="themeMaterialDto">A DTO containing the properties of the theme material to edit.</param>
    /// <response code="200">Change material was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">Theme Material not found.</response>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpPost]
    [Route("theme_material/edit")]
    public async Task<IActionResult> ChangeMaterialToTheme(ThemeMaterialEditDto themeMaterialDto)
    {
        var themeMaterial = await _context
            .ThemeMaterials
            .FirstOrDefaultAsync(e => e.ThemeId == themeMaterialDto.ThemeId
                                      && e.MaterialId == themeMaterialDto.MaterialId);

        if (themeMaterial == null) return NotFound();

        themeMaterial.ChangeThemeMaterialsForAdmin(themeMaterialDto);

        _context.ThemeMaterials.Update(themeMaterial);
        await _context.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    ///     Method for delete materials in theme.
    /// </summary>
    /// <param name="themeMaterialId">Id of the material in theme.</param>
    /// <param name="themeId">Id of the theme.</param>
    /// <param name="materialId">Id of the material.</param>
    /// <response code="200">Theme material deleted successfully.</response>
    /// <response code="404">Theme material not found.</response>
    /// <returns></returns>
    [HttpDelete]
    [Route("theme_material")]
    public async Task<IActionResult> DeleteThemeMaterial(int? themeMaterialId = null, int? themeId = null,
        int? materialId = null)
    {
        if (themeMaterialId.HasValue)
        {
            if (!await _context.ThemeMaterials.AnyAsync(e => e.Id == themeMaterialId.Value))
                return NotFound();

            _context
                .ThemeMaterials
                .Remove(
                    new ThemeMaterial
                    {
                        Id = themeMaterialId.Value
                    }
                );
        }
        else if (themeId.HasValue && materialId.HasValue)
        {
            var themeMaterial = await _context
                .ThemeMaterials
                .FirstOrDefaultAsync(e => e.ThemeId == themeId.Value
                                          && e.MaterialId == materialId.Value);

            if (themeMaterial == null)
                return NotFound();

            _context.ThemeMaterials.Remove(themeMaterial);
        }
        else
        {
            return BadRequest("Invalid parameters");
        }

        await _context.SaveChangesAsync();

        return Ok();
    }
}

public static class CourseThemesEx
{
    public static void ChangeThemeMaterialsForAdmin(this ThemeMaterial themeMaterial,
        ThemeMaterialEditDto themeMaterialDto)
    {
        themeMaterial.Number = themeMaterialDto.Number;
        themeMaterial.LevelId = themeMaterialDto.LevelId;
        themeMaterial.IsBasic = themeMaterialDto.IsBasic;
        themeMaterial.Note = themeMaterialDto.Note;
        themeMaterial.Description = themeMaterialDto.Description;
        themeMaterial.LevelId = themeMaterialDto.LevelId;
    }
}