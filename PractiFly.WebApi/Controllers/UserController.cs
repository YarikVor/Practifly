using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Dto.Registration;
using PractiFly.WebApi.EntityDb.Users;
using PractiFly.WebApi.Extentions;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUsersContext _usersContext;

    private readonly IHttpContextAccessor _httpContext;

   
    public UserController(IUsersContext usersContext, IHttpContextAccessor httpContext)
    {
        _usersContext = usersContext;
        _httpContext = httpContext;
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
    
    public string GenerateToken(User user)
    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
        var handler = new JwtSecurityTokenHandler();

        IEnumerable<Claim> claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.SerialNumber, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "empty"),
            new Claim(ClaimTypes.Email, user.Email)
           
        };

        var token = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: new SigningCredentials(
                AuthOptions.GetSymmetricSecurityKey(),
                algorithm: SecurityAlgorithms.HmacSha256
            )
        );

        return handler.WriteToken(token);
    }
    
    [HttpGet("test")]
    [Authorize]
    public async Task<IActionResult> Test()
    {
        var currentUser = _httpContext.HttpContext.User;
        return Ok(currentUser.FindFirst(ClaimTypes.Email).Value);
    }
    
    
}