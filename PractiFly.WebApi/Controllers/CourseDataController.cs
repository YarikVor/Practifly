using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Courses;
using PractiFly.WebApi.AutoMapper;
using PractiFly.WebApi.Dto.Admin.UserView;
using PractiFly.WebApi.Dto.CourseData;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.Controllers;

[Route("api")]
[ApiController]
public class CourseDataController : Controller
{
    private readonly IPractiflyContext _context;
    private readonly IMapper _mapper;
    private readonly IConfigurationProvider _configurationProvider;

    public CourseDataController(IPractiflyContext context, IMapper mapper, IConfigurationProvider configurationProvider)
    {
        _context = context;
        _mapper = mapper;
        _configurationProvider = configurationProvider;
    }


    /// <summary>
    ///     Retrieves an array of courses associated with a owner (user) identified by the specified Id, or all courses if no
    ///     Id is provided.
    /// </summary>
    /// <param name="ownerId">Id of the owner (user).</param>
    /// <response code="200">Courses representation was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No courses found.</response>
    /// <returns>A JSON-encoded representation of the array of courses.</returns>
    //TODO: Провірити цей метод, бо я хз як (Вадім).
    [HttpGet]
    [Route("course/all")]
    public async Task<IActionResult> Courses(int? ownerId = null)
    {
        CourseItemDto[] result;
        if (!ownerId.HasValue)
            result = await _context.Courses.AsNoTracking()
                .ProjectTo<CourseItemDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        else
            result = await _context.Courses.AsNoTracking()
                .Where(e => e.OwnerId == ownerId)
                .ProjectTo<CourseItemDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();

        return Json(result);
    }

    /// <summary>
    ///     Returns a list of course information.
    /// </summary>
    /// <response code="200">Getting course information was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No courses found.</response>
    /// <returns>A JSON-encoded representation of the list of course information.</returns>
    //TODO: Глянути тут, має бути інфа про один курс. ПЕРЕРОБЛЕНИЙ.
    [HttpGet]
    [Route("course")]
    public async Task<IActionResult> GetCourseInfo(int courseId)
    {
        //TODO: Mapper in mapper? (CourseDataProfile)
        var result = await _context
            .Courses
            .Where(e => e.Id == courseId)
            .Select(e => new CourseFullInfoDto
            {
                OwnerInfoDto = new OwnerInfoDto
                {
                    Id = e.OwnerId,
                    Owner = string.Concat(e.Owner.FirstName, " ", e.Owner.LastName),
                    FilePhoto = e.Owner.FilePhoto
                },
                CourseInfoDto = new CourseInfoDto
                {
                    Id = e.Id,
                    Language = e.Language.Name,
                    CourseName = e.Name,
                    Description = e.Description,
                    Note = e.Note
                },
                UserFullnameItemDto = _context
                    .UserCourses
                    .Where(e => e.CourseId == courseId)
                    .Select(uc => new UserFullnameItemDto
                    {
                        Id = uc.UserId,
                        Fullname = string.Concat(uc.User.FirstName, " ", uc.User.LastName)
                    })
                    .ToArray()
            })
            .FirstOrDefaultAsync();

        return Json(result);
    }


    /// <summary>
    ///     Returns a list of users who are enrolled in the course identified by the specified Id.
    /// </summary>
    /// <param name="courseId">Id of the course.</param>
    /// <response code="200">Getting users of course was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No users found.</response>
    /// <returns>A JSON-encoded representation of the list of users.</returns>
    //TODO: Чи потрібний?, оскільки вище реалізований метод для відображення користувачів курсу. 
    [HttpGet]
    [Route("course/users")]
    public async Task<IActionResult> GetUsersOfCourse(int courseId)
    {
        var result = await _context.UserCourses
            .Where(e => e.CourseId == courseId)
            .ProjectTo<UserFullnameItemDto>(_configurationProvider)
            .ToListAsync();

        return Json(result);
    }

    /// <summary>
    ///     Returns the owner information for a course identified by the specified courseId.
    /// </summary>
    /// <param name="courseId">Id of the course.</param>
    /// <response code="200">Getting owner of course was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No owner found.</response>
    /// <returns>A JSON-encoded representation of the owner information.</returns>
    [HttpGet]
    [Route("course/owner")]
    //TODO: має бути один вчитель, не список.
    //TODO: Чи потрібний?, оскільки вище реалізований метод для відображення власника курсу. 
    public async Task<IActionResult> GetOwnerOfCourse(int courseId)
    {
        var result = await _context.Courses
            .Where(e => e.Id == courseId)
            //.Select(o => new OwnerInfoDto
            //{
            //    Id = o.OwnerId,
            //    Owner = string.Concat(o.Owner.FirstName, " ", o.Owner.LastName),
            //    FilePhoto = o.Owner.FilePhoto
            //})
            .ProjectTo<OwnerInfoDto>(_configurationProvider)
            .FirstOrDefaultAsync();

        return Json(result);
    }


    /// <summary>
    ///     Creates a new course using the provided course data.
    /// </summary>
    /// <param name="courseDto">A data transfer object containing the course information.</param>
    /// <response code="200">Creating of course was successful.</response>
    /// <response code="400">Creating of course was failed.</response>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [HttpPost]
    [Route("course")]
    public async Task<IActionResult> CreateCourse(CreateCourseDto courseDto)
    {
        if (!await
                _context
                    .Users
                    .AnyAsync(e => e.Id == courseDto.OwnerId))
            return BadRequest(new { message = "Course no have owner!" });

        var course = new Course
        {
            Language = _context.Languages.First(l => l.Code == courseDto.Language),
            Name = courseDto.CourseName,
            Note = courseDto.Note,
            Description = courseDto.Description
        };
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        return Ok();
    }

    /// <summary>
    ///     Updates the specified course with the details provided in the <paramref name="courseDto" /> object.
    /// </summary>
    /// <param name="courseDto">A data transfer object containing the course information.</param>
    /// <response code="200">Editing course is successful.</response>
    /// <response code="400">Editing course was failed.</response>
    /// <response code="404">Not Found response.</response>
    /// <returns>Returns HTTP status response.</returns>
    [HttpPost]
    [Route("course/edit")]
    public async Task<IActionResult> EditCourse(EditCourseDto courseDto)
    {
        var course = await _context
            .Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == courseDto.Id);

        if (course == null) return NotFound();

        course.Id = courseDto.Id;
        course.Language = _context.Languages.First(l => l.Code == courseDto.Language);
        course.Name = courseDto.CourseName;
        course.Note = courseDto.Note;
        course.Description = courseDto.Description;

        _context.Courses.Update(course);

        await _context.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    ///     Deletes a course identified by the specified Id.
    /// </summary>
    /// <param name="courseId">ID of the course to be deleted.</param>
    /// <response code="200">Course was deleted succesfully.</response>
    /// <response code="400">Deleting of course was failed.</response>
    /// <response code="404">Not found course to delete.</response>
    /// <returns>An HTTP response status code.</returns>
    [HttpDelete]
    [Route("course")]
    public async Task<IActionResult> DeleteCourse(int courseId)
    {
        var course = await _context
            .Courses
            .AnyAsync(e => e.Id == courseId);

        if (!course)
            return NotFound();

        _context.Courses.Remove(new Course { Id = courseId });

        await _context.SaveChangesAsync();

        return Ok();
    }
}