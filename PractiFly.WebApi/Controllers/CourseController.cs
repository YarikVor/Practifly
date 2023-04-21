using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Dto.MyCourse;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CourseController: Controller
{
    private readonly IPractiflyContext _context;
    private readonly IMapper _mapper;
    
    public CourseController(IPractiflyContext context, IMapper mapper)
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