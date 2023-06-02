using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Dto.Registration;
using PractiFly.WebApi.Services.TokenGenerator;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : Controller
{
    private readonly IMapper _mapper;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly UserManager<User> _userManager;
    private readonly IAmazonS3ClientManager _amazonClient;

    public UserController(
        ITokenGenerator tokenGenerator,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IMapper mapper,
        IAmazonS3ClientManager amazonClient
    )
    {
        _tokenGenerator = tokenGenerator;
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _amazonClient = amazonClient;
        
    }

    /// <summary>
    ///     Creates a new user account based on the provided registration data,
    ///     adds the user to the 'User' role,
    ///     and returns an authentication token for the newly-created user.
    /// </summary>
    /// <param name="registrationDto">A Data Transfer Object, containing user registrate information.</param>
    /// <returns>HTTP response status code.</returns>
    /// <response code="200">
    ///     An HTTP OK result containing a JSON-encoded authentication token if the user account was created
    ///     successfully
    /// </response>
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

        var resultDto = _mapper.Map<User, UserTokenInfoDto>(identityUser, opt => opt.Items["baseUrl"] = _amazonClient.GetFileUrl());
        
        resultDto.Token = GenerateToken(identityUser.Id, UserRoles.User);
        return Ok(resultDto);
    }

    /// <summary>
    ///     Logs in a user with the specified email and password credentials.
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

        if (user == null)
            return NotFound();

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        var role = (await _userManager.GetRolesAsync(user))[0];

        if (!result.Succeeded) return BadRequest();
        var resultDto = _mapper.Map<User, UserTokenInfoDto>(user, opt => opt.Items["baseUrl"] = _amazonClient.GetFileUrl());
        resultDto.Token = GenerateToken(user.Id, role);
        return Ok(resultDto);
    }

    //public async Task<IActionResult> Login(LoginDto loginDto)
    //{
    //    var user = await _userManager.FindByEmailAsync(loginDto.Email);

    //    if (user == null)
    //    {
    //        return BadRequest("Invalid email or password.");
    //    }

    //    var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

    //    if (result.Succeeded)
    //    {
    //        var role = (await _userManager.GetRolesAsync(user))[0];
    //        return Ok(GenerateToken(user, role));
    //    }

    //    return BadRequest("Invalid email or password.");
    //}

    /// <summary>
    ///     Generates a JWT (JSON Web Token) for the specified user with the given role.
    /// </summary>
    /// <param name="user">The User object for whom the token is generated.</param>
    /// <param name="role">The role assigned to the user.</param>
    /// <response code="200">Token generate was successful.</response>
    /// <response code="400">Token generate was failed.</response>
    /// <returns>A JWT that contains the specified user's ID and assigned role.</returns>
    private string GenerateToken(int userId, string role)
    {
        IEnumerable<Claim> claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, role)
        };
        return _tokenGenerator.GenerateToken(claims);
    }

    /// <summary>
    ///     Refreshes the authentication token for the current user.
    /// </summary>
    /// <response code="200">Token refresh was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <returns>An HTTP 200 OK response containing a new authentication token.</returns>
    [HttpGet]
    [Route("refresh-token")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> RefreshToken()
    {
        var id = User.GetUserId();
        var user = await _userManager.FindByIdAsync(id);

        var role = (await _userManager.GetRolesAsync(user)).First();
        return Ok(GenerateToken(user.Id, role));
    }

    /// <summary>
    ///     Deletes the user associated with the current request's token.
    /// </summary>
    /// <response code="200">Delete current user was successful.</response>
    /// <response code="400">Delete of user was failed.</response>
    /// <response code="404">No users found.</response>
    /// <returns>An IActionResult indicating success or failure.</returns>
    [HttpDelete]
    [Route("delete")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteCurrentUserAsync()
    {
        // Отримання ідентифікатора поточного користувача з токена.
        var userId = User.GetUserId();
        
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            // Якщо користувача з таким ідентифікатором не знайдено, видається помилка.
            return BadRequest();

        //TODO: Add confirm email
        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
            // Якщо виникла помилка при видаленні користувача, видається помилка.
            return BadRequest();

        return Ok();
    }
}