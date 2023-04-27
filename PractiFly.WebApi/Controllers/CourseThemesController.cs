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

        //метод для відображення тем, що знаходяться в курсі
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

        //TODO: ViewMaterialsList
        [HttpGet]
        [Route("course/{courseId:int}/themes")]

        //метод перегляду списку матеріалів
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

        //метод перегляду інформації щодо певної теми
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
