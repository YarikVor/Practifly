using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Materials;
using PractiFly.WebApi.Dto.CourseData;
using PractiFly.WebApi.Dto.CourseMaterials;
using PractiFly.WebApi.Dto.CourseThemes;
using PractiFly.WebApi.Dto.Heading;
using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //TODO: CourseMAterialController
    public class CourseMaterialsController : Controller
    {
        private readonly IPractiflyContext _context;
        private readonly IMapper _mapper;

        public CourseMaterialsController(IPractiflyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of courses for an admin user.
        /// </summary>
        /// <returns>A JSON-encoded representation of the list of courses, including only the course ID and name.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCoursesForAdmin()
        {
            var courses = await _context.Courses
            .Select(c => new CourseItemDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();

            return Json(courses);
        }

        /// <summary>
        /// Returns a list of course headings associated with a course identified by the specified Id.
        /// </summary>
        /// <param name="courseId">Id of the course.</param>
        /// <returns>A JSON-encoded representation of the list of course headings.</returns>
        [HttpGet]
        [Route("course/{courseId:int}/heading")]
        public async Task<IActionResult> CourseHeading(int courseId)
        {
            var result = await _context.CourseHeadings
                .AsNoTracking()
                .Where(e => e.CourseId == courseId)
                .Select(e => new CourseHeadingInfoDto()
                {
                    Id = e.Heading.Id,
                    Code = e.Heading.Code,
                    Name = e.Heading.Name,
                })
                .ToListAsync();

            return Json(result);
        }

        //TODO: ?
        [HttpGet]
        [Route("")]
        //public async Task<IActionResult> GetMaterialAndBlocksForInclusion(int materialId)
        public async Task<IActionResult> GetMaterialForInclusion(int headingId, int courseId)
        {
            var result = await _context
            .Materials
                .AsNoTracking()
                .Where(e => e.Id == 2/*TODO:*/)
                .ProjectTo<MaterialBlocksDto>(_mapper.ConfigurationProvider)
                .OrderBy(e => e.Id)
                .ToListAsync();

            return Json(result);
        }

        /// <summary>
        /// Returns a list of course headings associated with a course identified by the specified courseId.
        /// </summary>
        /// <param name="materialId">Id of the material.</param>
        /// <returns>A JSON-encoded representation of the list of course headings.</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetMaterialBlocks(int materialId)
        {
            var result = await _context
            .Materials
                .AsNoTracking()
                .Where(e => e.Id == materialId)
                .ProjectTo<MaterialBlocksDto>(_mapper.ConfigurationProvider)
                .OrderBy(e => e.Id)
                .ToListAsync();

            return Json(result);
        }
    }
}
