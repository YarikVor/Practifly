using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.Users;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.Registration;
using PractiFly.WebApi.Extentions;
using PractiFly.WebApi.Services.TokenGenerator;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUsersContext _usersContext;

    public UserController(IUsersContext usersContext, IHttpContextAccessor httpContext, ITokenGenerator tokenGenerator)
    {
        _usersContext = usersContext;
        _httpContext = httpContext;
        _tokenGenerator = tokenGenerator;
    }

    // TODO: SESSION
    [HttpPost]
    [Route("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Create(RegistrationDto registrationDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!DateEx.TryToConvertToDateOnly(
                registrationDto.YearBirth!.Value,
                registrationDto.MonthBirth!.Value,
                registrationDto.DayBirth!.Value,
                out var birthday)
           )
            ModelState.AddModelError("Birthday", "Birthday is not valid");

        if (birthday >= DateOnly.FromDateTime(DateTime.Today))
            ModelState.AddModelError("Birthday", "Birthday is not valid");

        if (registrationDto.Password != registrationDto.PasswordConfirm)
            ModelState.AddModelError("Password", "Passwords are not equal");

        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = new User
        {
            Id = 0,
            Birthday = birthday,
            Email = registrationDto.Email,
            FirstName = registrationDto.Name,
            LastName = registrationDto.Surname,
            Phone = registrationDto.Phone,
            RegistrationDate = DateOnly.FromDateTime(DateTime.Today),
            // TODO: Hash password
            PasswordHash = registrationDto.Password,
            FilePhoto = "https://www.nicepng.com/maxp/u2y3a9e6t4o0a9w7/"
        };

        await _usersContext.Users.AddAsync(user);
        await _usersContext.SaveChangesAsync();

        return user.Id != 0 ? Ok(user.Id) : BadRequest();
    }

    [HttpPost]
    [Route("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = await _usersContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(
                u => u.Email == loginDto.Email && u.PasswordHash == loginDto.Password
            );

        return user != null
            ? Ok(GenerateToken(user))
            : BadRequest();
    }

    private string GenerateToken(User user)
    {
        IEnumerable<Claim> claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, "user"),
        };

        return _tokenGenerator.GenerateToken(claims);
    }

    [HttpGet("test")]
    [Authorize]
    public async Task<IActionResult> Test()
    {
        _usersContext.Users.Where(e => e.Is)
    
        var currentUser = HttpContext.User;
        return Ok(currentUser.FindFirst(ClaimTypes.Email).Value);
    }
}