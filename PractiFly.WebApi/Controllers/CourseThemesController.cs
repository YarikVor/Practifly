using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.WebApi.Dto.CourseThemes;

namespace PractiFly.WebApi.Controllers
{
    [Route("api")]
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
        /// Returns an information of theme associated with a course identified by the special Id.
        /// </summary>
        /// <param name="courseId">ID of the course.</param>
        /// <response code="200">Getting information of theme was successful.</response>
        /// <response code="400">Operation was failed.</response>
        /// <response code="404">No themes found.</response>
        /// <returns>A JSON-encoded representation of the information of course theme.</returns>
        [HttpGet]
        [Route("course/themes/notunderstand")]

        //TODO: Метод повертає інформацію про курс. ПЕРЕПРОВІРИТИ
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
                .FirstAsync();*/

            return Json(1);
        }

        /// <summary>
        /// Returns a list of themes associated with a course identified by the specified Id.
        /// </summary>
        /// <param name="courseId">ID of the course.</param>
        /// <response code="200">Getting list themes from course was successful.</response>
        /// <response code="400">Operation was failed.</response>
        /// <response code="404">No themes found.</response>
        /// <returns>An HTTP response indicating success and a 
        /// JSON-encoded representation of the list of themes, 
        /// or a "Not Found" error if the specified ID does not exist.</returns>
        [HttpGet]
        [Route("course/themes")]
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
        /// <response code="200">Getting materials was successful.</response>
        /// <response code="400">Operation was failed.</response>
        /// <response code="404">No materials found.</response>
        /// <returns>A JSON-encoded representation of the list of materials.</returns>
        //TODO: ViewMaterialsList
        [HttpGet]
        [Route("[action]")]
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
        /// <response code="200">Getting courses list was successful.</response>
        /// <response code="400">Operation was failed.</response>
        /// <response code="404">No courses found.</response>
        /// <returns>A JSON-encoded representation of the list of courses.</returns>
        // TODO: Цей метод поверне лише один курс, а не список курсів. ПЕРЕПРОВІРИТИ
        [HttpGet]
        [Route("[action]")]
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
        /// <response code="200">Getting information of course theme was successful.</response>
        /// <response code="400">Operation was failed.</response>
        /// <response code="404">No theme found.</response>
        /// <returns>An HTTP response indicating success and a JSON-encoded representation 
        /// of the theme information, or a "Not Found" error if the specified theme ID does not exist.</returns>
        [HttpGet]
        [Route("theme")]
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
        /// <response code="200">Update theme was successful.</response>
        /// <response code="400">Update was failed.</response>
        /// <returns>An HTTP response indicating success or failure of the update operation.</returns>
        [HttpGet]
        [Route("theme/edit")]
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
