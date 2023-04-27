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
    [Route("")]
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
    [Route("")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> DeleteUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return BadRequest();
        }

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete]
    [Route("")]
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


    public async Task<IActionResult> CreateUserInAdmin(UserProfileForAdminCreateDto userDto)
    {
        const string defaultPassword = "Qwerty_1";
        var user = new User()
        {
            UserName = $"{userDto.Name}_{userDto.Surname}".ToLower(),
            FirstName = userDto.Name,
            LastName = userDto.Surname,
            Email = userDto.Email,
            PhoneNumber = userDto.Phone,
            FilePhoto = userDto.FilePhoto
        };
        
        var result = await _userManager.CreateAsync(user, defaultPassword);

        if (!result.Succeeded)
            return BadRequest();

        var roleResult = await _userManager.AddToRoleAsync(user, userDto.Role);
        
        if (!roleResult.Succeeded)
            return BadRequest();

        return Ok();
    }

    public async Task<IActionResult> UpdateUserInAdmin(UserProfileForAdminUpdateDto userDto)
    {
        User user = await _userManager.FindByIdAsync(userDto.Id.ToString());
        
        if (user == null)
        {
            return NotFound();
        }
        
        user.UserName = userDto.Name;
        user.LastName = userDto.Surname;
        user.Email = userDto.Email;
        user.PhoneNumber = userDto.Phone;
        user.FilePhoto = userDto.FilePhoto;

        var result = await _userManager.UpdateAsync(user);
        
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        
        await _userManager.RemoveFromRolesAsync(user, UserRoles.RolesEnumerable);
        var roleResult = await _userManager.AddToRoleAsync(user, userDto.Role);
        
        if (!roleResult.Succeeded)
        {
            return BadRequest(roleResult.Errors);
        }
        
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> FilterUsers(UserFilteringDto filter)
    {
        var users = _userManager.Users.AsNoTracking();

        if (!string.IsNullOrEmpty(filter.Name))
        {
            users = users.Where(u => u.FirstName.Contains(filter.Name));
        }

        if (!string.IsNullOrEmpty(filter.Surname))
        {
            users = users.Where(u => u.LastName.Contains(filter.Surname));
        }

        if (!string.IsNullOrEmpty(filter.Phone))
        {
            users = users.Where(u => u.PhoneNumber == filter.Phone);
        }

        if (filter.RegistrationDateFrom.HasValue)
        {
            users = users.Where(u => u.RegistrationDate >= filter.RegistrationDateFrom.Value);
        }

        if (filter.RegistrationDateTo.HasValue)
        {
            users = users.Where(u => u.RegistrationDate <= filter.RegistrationDateTo.Value);
        }

        if (!string.IsNullOrEmpty(filter.Email))
        {
            users = users.Where(u => u.Email == filter.Email);
        }

        if (!string.IsNullOrEmpty(filter.Role))
        {
            users = users.Where(u => _userManager.IsInRoleAsync(u, filter.Role).Result);
        }
        var result = await users.Select(e => e.ToUserFullnameItemDto()).ToArrayAsync();

        return Json(result);
    }

}