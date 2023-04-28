using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.WebApi.Dto.CourseThemes;

namespace PractiFly.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseThemesController : Controller
    {
        private readonly IPractiflyContext _context;
        private readonly IMapper _mapper;

        public CourseThemesController(IPractiflyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Returns a list of themes associated with a course identified by the special Id.
        /// </summary>
        /// <param name="courseId">ID of the course.</param>
        /// <returns>A JSON-encoded representation of the list of themes.</returns>
        [HttpGet]
        [Route("course/{courseId:int}/themes")]
        //метод, повертає список тем в курсі
        public async Task<IActionResult> GetCourseThemes(int courseId)
        {
            var result = await _context
                .Courses
                .AsNoTracking()
                .Where(e => e.Id == courseId)
                .ProjectTo<CourseItemWithThemeDto>(_mapper.ConfigurationProvider)
                .FirstAsync();

            return Json(result);
        }

        /// <summary>
        /// Returns a list of themes associated with a course identified by the specified Id.
        /// </summary>
        /// <param name="courseId">ID of the course.</param>
        /// <returns>An HTTP response indicating success and a 
        /// JSON-encoded representation of the list of themes, 
        /// or a "Not Found" error if the specified ID does not exist.</returns>
        public async Task<IActionResult> GetThemesFromCourses(int courseId)
        {
            var result = await _context.Themes.FindAsync(courseId);

            if(result == null)
            {
                return NotFound();
            }

            var themes = await _context.Themes.Where(t => t.CourseId == courseId).ToListAsync();

            return Ok(themes);
        }

        /// <summary>
        /// Returns a list of materials associated with a material identified by the specified Id.
        /// </summary>
        /// <param name="materialId">ID of the material.</param>
        /// <returns>A JSON-encoded representation of the list of materials.</returns>
        //TODO: ViewMaterialsList
        [HttpGet]
        [Route("course/{courseId:int}/themes")]
        public async Task<IActionResult> GetMaterialsList(int materialId)
        {
            var result = await _context
                .Materials
                .AsNoTracking()
                .Where(e => e.Id == materialId)
                .ProjectTo<MaterialsMenuDto>(_mapper.ConfigurationProvider)
                .OrderBy(e => e.Priority)
                .ToListAsync();

            return Json(result);
        }

        /// <summary>
        /// Returns a list of courses associated with a course identified by the specified Id.
        /// </summary>
        /// <param name="courseId">ID of the course.</param>
        /// <returns>A JSON-encoded representation of the list of courses.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCoursesList(int courseId)
        {
            ThemeItemDto[] result = await _context
                .Courses
                .AsNoTracking()
                .Where(e => e.Id == courseId)
                .ProjectTo<ThemeItemDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();

            return Json(result);
        }

        /// <summary>
        /// Returns information about a specific theme identified by the specified Id.
        /// </summary>
        /// <param name="themeId">Id of the theme.</param>
        /// <returns>An HTTP response indicating success and a JSON-encoded representation 
        /// of the theme information, or a "Not Found" error if the specified theme ID does not exist.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> CourseThemeInfo(int themeId)
        {
            var result = await _context
                .Themes
                .AsNoTracking()
                .Where(e => e.Id == themeId)
                .Select(e => new ThemeDto()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Note = e.Note
                })
                .FirstOrDefaultAsync();

            if(result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        /// <summary>
        /// Updates an existing theme identified by the specified Id with the specified Data Transfer Object.
        /// </summary>
        /// <param name="themeId">Id of the theme.</param>
        /// <param name="themeDto">The updated theme data as a JSON-encoded ThemeDto object.</param>
        /// <returns>An HTTP response indicating success or failure of the update operation.</returns>
        public async Task<IActionResult> UpdateTheme(int themeId, [FromBody] ThemeDto themeDto)
        {
            if (themeDto == null)
            {
                return BadRequest();
            }

            var theme = await _context.Themes.FindAsync(themeId);

            if (theme == null)
            {
                return NotFound();
            }

            theme.Name = themeDto.Name;
            theme.Note = themeDto.Note;

            _context.Themes.Update(theme);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //TODO: метод для перегляду всіх курсів наявний в CourseController.UserCourse
    }
}
