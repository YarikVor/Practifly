using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.WebApi.Dto.MyCourse;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class MyCourseController : Controller
    {
        private readonly IPractiflyContext _context;
        private readonly IConfigurationProvider _configurationProvider;

        public MyCourseController(
            IPractiflyContext context, 
            IConfigurationProvider configurationProvider
            )
        {
            _context = context;
            _configurationProvider = configurationProvider;
        }

        /// <summary>
        /// Returns a list of courses and their statuses associated with a user identified by the specified Id.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <response code="200">List of user courses return successfully.</response>
        /// <response code="400">Operation was failed.</response>
        /// <response code="404">No courses found.</response>
        /// <returns>A JSON-encoded representation of the list of courses and their statuses.</returns>
        [HttpGet]
        [Route("user/courses")]
        public async Task<IActionResult> UserCourse(int userId)
        {
            UserCourseStatusDto[] result = await _context
                .UserCourses
                .AsNoTracking()
                .Where(e => e.UserId == userId)
                .ProjectToArrayAsync<UserCourseStatusDto>(_configurationProvider);

            return Json(result);
        }

    }
}
