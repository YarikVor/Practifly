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
[Route("api/user")]
public class UserController : Controller
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    
    public UserController(
        ITokenGenerator tokenGenerator,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IMapper mapper
    )
    {
        _tokenGenerator = tokenGenerator;
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    /// <summary>
    ///  Creates a new user account based on the provided registration data, 
    ///  adds the user to the 'User' role, 
    ///  and returns an authentication token for the newly-created user.
    /// </summary>
    /// <param name="registrationDto">A Data Transfer Object, containing user registrate information.</param>
    /// <returns>HTTP response status code.</returns>
    /// <response code="200">An HTTP OK result containing a JSON-encoded authentication token if the user account was created successfully</response>     
    /// <response code="400">HTTP BadRequest result</response>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Create(RegistrationDto registrationDto)
    {
        var identityUser = _mapper.Map<RegistrationDto, User>(registrationDto);

        var identityResult = await _userManager.CreateAsync(identityUser, registrationDto.Password);

        if (!identityResult.Succeeded)
            return BadRequest(identityResult.Errors);

        identityResult = await _userManager.AddToRoleAsync(identityUser, UserRoles.User);

        if (!identityResult.Succeeded)
            return BadRequest(identityResult.Errors);

        string token = GenerateToken(identityUser, UserRoles.User);

        return Ok(token);
    }

    /// <summary>
    /// Logs in a user with the specified email and password credentials.
    /// </summary>
    /// <param name="loginDto">The login Data Transfer Object for the user.</param>
    /// <returns></returns>
    /// <response code="200">Returns an access token for the user.</response>     
    /// <response code="400">BadRequest result.</response>
    [HttpPost]
    [Route("login")]
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

    /// <summary>
    /// Generates a JWT (JSON Web Token) for the specified user with the given role.
    /// </summary>
    /// <param name="user">The User object for whom the token is generated.</param>
    /// <param name="role">The role assigned to the user.</param>
    /// <returns>A JWT that contains the specified user's ID and assigned role.</returns>
    private string GenerateToken(User user, string role)
    {
        IEnumerable<Claim> claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, role),
        };

        return _tokenGenerator.GenerateToken(claims);
    }

    /// <summary>
    /// Refreshes the authentication token for the current user.
    /// </summary>
    /// <returns>An HTTP 200 OK response containing a new authentication token.</returns>
    [HttpGet]
    [Route("refresh-token")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> RefreshToken()
    {
        var currentUser = HttpContext.User;
        var id = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

        var user = await _userManager.FindByIdAsync(id);

        var role = (await _userManager.GetRolesAsync(user)).First();
        return Ok(GenerateToken(user, role));
    }

    /// <summary>
    /// Deletes the user associated with the current request's token.
    /// </summary>
    /// <returns>An IActionResult indicating success or failure.</returns>
    [HttpDelete]
    [Route("delete")]
    [Authorize(AuthenticationSchemes = "Bearer")]
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