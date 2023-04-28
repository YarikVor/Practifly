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
    [Route("api")]
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


        /// <summary>
        /// Returns a list of courses that can be accessed by an admin, each represented by an ID and a name.
        /// </summary>
        /// <returns>A JSON-encoded representation of the list of courses.</returns>

        //already realized

        //[HttpGet]
        //[Route("courses/all")] 
        //public async Task<IActionResult> GetCoursesForAdmin()
        //{
        //    var courses = await _context.Courses
        //    .Select(c => new CourseItemDto
        //    {
        //        Id = c.Id,
        //        Name = c.Name
        //    })
        //    .ToListAsync();

        //    return Json(courses);
        //}

        /// <summary>
        /// Returns a list of course information.
        /// </summary>
        /// <returns>A JSON-encoded representation of the list of course information.</returns>
        //TODO: Глянути тут, має бути інфа про один курс.
        [HttpGet]
        [Route("course")]
        public async Task<IActionResult> GetCourseInfo(int courseId)
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
             .FirstOrDefaultAsync();

            return Json(result);
        }

        /// <summary>
        /// Returns a list of users who are enrolled in the course identified by the specified Id.
        /// </summary>
        /// <param name="courseId">Id of the course.</param>
        /// <returns>A JSON-encoded representation of the list of users.</returns>
        [HttpGet]
        [Route("course/users")]
        public async Task<IActionResult> GetUsersOfCourse(int courseId)
        {
            var result = await _context.UserCourses
                .Where(e => e.CourseId == courseId)
                .Select(e => e.User.ToUserFullnameItemDto())
                .ToListAsync();

            return Json(result);
        }

        /// <summary>
        /// Returns the owner information for a course identified by the specified courseId.
        /// </summary>
        /// <param name="courseId">Id of the course.</param>
        /// <returns>A JSON-encoded representation of the owner information.</returns>
        [HttpGet]
        [Route("course/owner")]
        //TODO: має бути один вчитель, не список.
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

        /// <summary>
        /// Creates a new course using the provided course data.
        /// </summary>
        /// <param name="courseDto">A data transfer object containing the course information.</param>
        /// <returns>An HTTP response indicating the result of the operation.</returns>
        [HttpPost]
        [Route("course")]
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

        /// <summary>
        /// Updates the specified course with the details provided in the <paramref name="courseDto"/> object.
        /// </summary>
        /// <param name="courseDto">A data transfer object containing the course information.</param>
        /// <response code="200">Operation is successful, an HTTP 200 OK status code is returned.</response>
        /// <response code="404">Otherwise, a HTTP Not Found response.</response>
        /// <returns>Returns HTTP status response.</returns>
        [HttpGet]
        [Route("course/edit")]
        public async Task<IActionResult> EditCourse(CreateCourseDto courseDto)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(e => e.Id == courseDto.CourseId);
            
            if (course == null) { 
                return NotFound(); 
            }

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

        /// <summary>
        /// Deletes a course identified by the specified Id.
        /// </summary>
        /// <param name="courseId">ID of the course to be deleted.</param>
        /// <response code="200">Course was deleted succesfully.</response>
        /// <response code="404">Delete error.</response>
        /// <returns>An HTTP response status code.</returns>
        [HttpDelete]
        [Route("course")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(e => e.Id == courseId);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
