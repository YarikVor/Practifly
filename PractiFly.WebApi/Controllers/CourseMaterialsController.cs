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
        public async Task<IActionResult> GetMaterialForInclusion(int headingId,int courseId)
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

        [HttpGet]
        [Route("")]
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
