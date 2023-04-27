using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbContextUtility.Context.Users;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.AutoMapper;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Dto.Admin.UserView;
using PractiFly.WebApi.Dto.Profile;
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
    private readonly RoleManager<Role> _roleManager;
    private readonly IPractiflyContext _context;
    private readonly IMapper _mapper;


    public UserController(IPractiflyContext practiflyContext, IHttpContextAccessor httpContext, ITokenGenerator
        tokenGenerator, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
    {
        _httpContext = httpContext;
        _tokenGenerator = tokenGenerator;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _context = practiflyContext;

    }

    [HttpPost]
    [Route("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Create(RegistrationDto registrationDto)
    {
        var identityUser = registrationDto.ToUser();

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
    [Route("[action]")]
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
    [Route("[action]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> RefreshToken()
    {
        var currentUser = HttpContext.User;
        var id = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

        var user = await _userManager.FindByIdAsync(id);

        var role = (await _userManager.GetRolesAsync(user)).First();
        return Ok(GenerateToken(user, role));
    }


    

    [HttpDelete]
    [Route("[action]")]
    [Authorize]
    public async Task<IActionResult> DeleteCurrentUserAsync()
    {
        // Отримання ідентифікатора поточного користувача з токена.
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            // Якщо ідентифікатор користувача не було знайдено в токені, видається помилка.
            return BadRequest();
        }

        // Знаходження користувача за його ідентифікатором.
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            // Якщо користувача з таким ідентифікатором не знайдено, видається помилка.
            return BadRequest();
        }
        //TODO: Add confirm email
        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            // Якщо виникла помилка при видаленні користувача, видається помилка.
            return BadRequest();
        }

        return Ok();
    }


    

}