using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.WebApi.Dto.MyCourse;

namespace PractiFly.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class MyCourseController : Controller
    {
        private readonly IPractiflyContext _context;
        private readonly IMapper _mapper;

        public MyCourseController(IPractiflyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of courses and their statuses associated with a user identified by the specified Id.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <returns>A JSON-encoded representation of the list of courses and their statuses.</returns>
        [HttpGet]
        [Route("user/courses")]
        public async Task<IActionResult> UserCourse(int userId)
        {
            UserCourseStatusDto[] result = await _context
                .UserCourses
                .AsNoTracking()
                .Where(e => e.UserId == userId)
                .ProjectTo<UserCourseStatusDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();

            return Json(result);
        }

    }
}
