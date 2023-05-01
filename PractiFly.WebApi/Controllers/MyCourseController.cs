using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.MyCourse;
using System.Security.Claims;

namespace PractiFly.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class MyCourseController : Controller
    {
        private readonly IPractiflyContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public MyCourseController(IPractiflyContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        /// Returns a list of courses and their statuses associated with a user identified by the specified Id.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <response code="200">List of user courses return successfully.</response>
        /// <response code="400">Operation was failed.</response>
        /// <response code="404">No courses found.</response>
        /// <returns>A JSON-encoded representation of the list of courses and their statuses.</returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("user/courses")]
        public async Task<IActionResult> UserCourse()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                // Якщо користувача з таким ідентифікатором не знайдено, видається помилка.
                return BadRequest();
            }
            int userIntId = user.Id;
            UserCourseStatusDto[] result = await _context
                .UserCourses
                .AsNoTracking()
                .Where(e => e.UserId == userIntId)
                .ProjectTo<UserCourseStatusDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();

            return Json(result);
        }

    }
}
