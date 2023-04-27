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
            CourseItemWithThemeDto result = await _context
                .Courses
                .AsNoTracking()
                .Where(e => e.Id == courseId)
                .ProjectTo<CourseItemWithThemeDto>(_mapper.ConfigurationProvider)
                .FirstAsync();

            return Json(result);
        }

        //TODO: ViewMaterialsList
        [HttpGet]
        [Route("course/{courseId:int}/themes")]

        //метод перегляду списку матеріалів
        public async Task<IActionResult> GetMaterialsList(int materialId)
        {
            MaterialsMenuDto[] result = await _context
                .Materials
                .AsNoTracking()
                .Where(e => e.Id == materialId)
                .ProjectTo<MaterialsMenuDto>(_mapper.ConfigurationProvider)
                .OrderBy(e => e.Priority)
                .ToArrayAsync();

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
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> CourseThemeInfo(int courseId)
        {
            
        }
    }
}
