using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.WebApi.Dto.MyCourse;

namespace PractiFly.WebApi.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        [Route("{userId:int}/usercourse")]
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
