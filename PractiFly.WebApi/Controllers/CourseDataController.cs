using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.AutoMapper;
using PractiFly.WebApi.Dto.Admin.UserView;
using PractiFly.WebApi.Dto.CourseData;
using PractiFly.WebApi.Dto.CourseThemes;

namespace PractiFly.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseDataController : Controller
    {
        private readonly IPractiflyContext _context;
        private readonly IMapper _mapper;

        public CourseDataController(IPractiflyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

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

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCourseInfo()
        {
            var result = await _context.Courses
             .Select(c => new CourseInfoDto
             {
                 Id = c.Id,
                 Language = c.Language.Code,
                 CourseName = c.Name,
                 Description = c.Description,
                 Note = c.Note,
             })
             .ToListAsync();

            return Json(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUsersOffCourse(int courseId)
        {
            var result = await _context.UserCourses
                .Where(e => e.CourseId == courseId)
                .Select(e => e.User.ToUserFullnameItemDto())
                .ToListAsync();

            return Json(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetOwnerOfCourse(int courseId)
        {
            var result = await _context.Courses
                .Where(e => e.Id == courseId)
                .Select(o => new OwnerInfoDto
                {
                    Id = o.Id,
                    Owner = o.Name,
                    FilePhoto = o.Owner.FilePhoto,
                })
                .ToListAsync();

            return Json(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CreateCourse(CreateCourseDto courseDto)
        {
            var course = new Course()
            {
                Language = _context.Languages.First(l => (l.Code == courseDto.Language)),
                OwnerId = courseDto.OwnerId,
                Name = courseDto.CourseName,
                Note = courseDto.Note,
                Description = courseDto.Description
            };
            await _context.Courses.AddAsync(course);
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EditCourse(CreateCourseDto courseDto)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(e => e.Id == courseDto.CourseId);
            if (course == null) { return NotFound(); }

            course.Id = courseDto.CourseId;
            course.Language = _context.Languages.First(l => (l.Code == courseDto.Language));
            course.OwnerId = courseDto.OwnerId;
            course.Name = courseDto.CourseName;
            course.Note = courseDto.Note;
            course.Description = courseDto.Description;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(e => e.Id == courseId);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
