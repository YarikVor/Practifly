using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PractiFly.DbContextUtility.Context.Users;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Dto.Registration;
using PractiFly.WebApi.Services.TokenGenerator;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : Controller
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserController(IHttpContextAccessor httpContext, ITokenGenerator
        tokenGenerator, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _httpContext = httpContext;
        _tokenGenerator = tokenGenerator;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    [Route("")]
    [AllowAnonymous]
    public async Task<IActionResult> Create(RegistrationDto registrationDto)
    {
        var identityUser = new User
        {
            UserName = registrationDto.Username,
            Birthday = registrationDto.Birthday,
            Email = registrationDto.Email,
            FirstName = registrationDto.Name,
            LastName = registrationDto.Surname,
            PhoneNumber = registrationDto.Phone,
            RegistrationDate = DateOnly.FromDateTime(DateTime.Today),
            FilePhoto = "https://www.nicepng.com/maxp/u2y3a9e6t4o0a9w7/"
        };

        var identityResult = await _userManager.CreateAsync(identityUser, registrationDto.Password);

        if (!identityResult.Succeeded)
            return BadRequest();

        identityResult = await _userManager.AddToRoleAsync(identityUser, UserRoles.User);

        if (!identityResult.Succeeded)
            return BadRequest();

        string token = GenerateToken(identityUser, UserRoles.User);

        return Ok(token);
    }

    
    [HttpPost]
    [Route("")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        var role = (await _userManager.GetRolesAsync(user))[0];
        
        if (result.Succeeded)
            return Ok(GenerateToken(user, role));

        return BadRequest();
    }

    private string GenerateToken(User user, string role)
    {
        IEnumerable<Claim> claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, role),
        };

        return _tokenGenerator.GenerateToken(claims);
    }
    
    [HttpGet]
    [Route("")]
    [Authorize()]
    public async Task<IActionResult> RefreshToken()
    {
        var currentUser = HttpContext.User;
        var id = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

        var user = await _userManager.FindByIdAsync(id);
        
        var role = (await _userManager.GetRolesAsync(user)).First();
        
        return Ok(GenerateToken(user, role));
    }
}