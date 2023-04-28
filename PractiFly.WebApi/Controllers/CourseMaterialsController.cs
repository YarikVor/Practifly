using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.WebApi.Dto.CourseData;
using PractiFly.WebApi.Dto.Heading;
using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        //[HttpGet]
        //[Route("course/{courseId:int}/heading")]
        //public async Task<IActionResult> CourseHeading(int courseId, [RegularExpression(@"^(?:\d{2})?(?:\.\d{2}){0,2}$")] string beginRubricCode)
        //{
        //    string patternSubRubricCode = string.IsNullOrEmpty(beginRubricCode)
        //        ? "__"
        //        : $"{beginRubricCode}.__";

        //    var result = await _context.Headings
        //        .AsNoTracking()
        //        .Where(e => EF.Functions.Like(e.Code, patternSubRubricCode))
        //        .Select(e => new HeadingItemInCourseDto()
        //        {
        //            Code = e.Code,
        //            Name = e.Name,
        //            Id = e.Id,
        //            IsIncluded = _context.CourseHeadings.Any(ch => ch.CourseId == courseId)
        //        })
        //        .ToListAsync();

        //    return Json(result);
        //}
    }
}
