using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.AutoMapper;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Dto.Admin.UserView;
using PractiFly.WebApi.Services.TokenGenerator;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.Controllers;

[Route("api/admin/user")]
[ApiController]
//[Authorize(Roles = UserRoles.Admin, AuthenticationSchemes = "Bearer")]
public class AdminController : Controller
{
    private readonly IPractiflyContext _context;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IMapper _mapper;
    private readonly RoleManager<Role> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly UserManager<User> _userManager;
    private readonly IConfigurationProvider _configurationProvider;


    public AdminController(IPractiflyContext practiflyContext, IHttpContextAccessor httpContext, ITokenGenerator
        tokenGenerator, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager,
        IConfigurationProvider configurationProvider)
    {
        _httpContext = httpContext;
        _tokenGenerator = tokenGenerator;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _context = practiflyContext;
        _configurationProvider = configurationProvider;
    }

    /// <summary>
    ///     Method for obtaining information about the selected user from the list
    /// </summary>
    /// <param name="userId">ID for receiving information about the selected user from the list</param>
    /// <returns></returns>
    /// <response code="200">Received information about the user by his ID</response>
    /// <response code="404">No user found with this ID</response>
    [HttpGet]
    [Route("")]
    //[Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetInfoForUsers(int userId)
    {
        var result = await _context
            .Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .ProjectTo<UserProfileForAdminViewDto>(_configurationProvider)
            .FirstOrDefaultAsync();
        
        /*Role = _context
                    .UserRoles
                    .Where(e => e.UserId == u.Id)
                    .Select(e => e.Role.Name)
                    .FirstOrDefault()*/

        if (result == null) return NotFound();

        return Json(result);
    }

    /// <summary>
    ///     Method for deleting a user by his id
    /// </summary>
    /// <param name="userId">The ID of the user we want to delete</param>
    /// <returns></returns>
    /// <response code="200">Received information about the user by his ID</response>
    /// <response code="404">No users found</response>
    /// <response code="400">The user delete operation failed</response>
    [HttpDelete]
    [Route("")]
    //[Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> DeleteUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            //return BadRequest();
            return NotFound();

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded) return BadRequest();

        return Ok();
    }

    /// <summary>
    ///     A method for the admin to create a new user with a default password
    /// </summary>
    /// <param name="userDto">DTO that has the necessary properties to create a user</param>
    /// <returns></returns>
    /// <response code="400">The operation to create a user or add its role failed</response>
    /// <response code="200">User creation was successful</response>
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateUserInAdmin(UserProfileForAdminCreateDto userDto)
    {
        const string defaultPassword = "Qwerty_1";
        var user = new User
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

    /// <summary>
    ///     Method to update information about the selected user
    /// </summary>
    /// <param name="userDto">DTO that has the necessary properties to update information about the selected user</param>
    /// <returns></returns>
    /// <response code="404">No users found</response>
    /// <response code="400">The operation to create a user or add its role failed</response>
    /// <response code="200">User update was successful</response>
    [HttpPost]
    [Route("edit")]
    public async Task<IActionResult> UpdateUserInAdmin(UserProfileForAdminUpdateDto userDto)
    {
        var user = await _userManager.FindByIdAsync(userDto.Id.ToString());

        if (user == null) return NotFound();

        user.UserName = userDto.Name;
        user.LastName = userDto.Surname;
        user.Email = userDto.Email;
        user.PhoneNumber = userDto.Phone;
        user.FilePhoto = userDto.FilePhoto;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded) return BadRequest(result.Errors);

        await _userManager.RemoveFromRolesAsync(user, UserRoles.RolesEnumerable);
        var roleResult = await _userManager.AddToRoleAsync(user, userDto.Role);

        if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

        return Ok();
    }

    /// <summary>
    ///     A method for filtering users, for a more convenient search for them
    /// </summary>
    /// <param name="filter">DTO which has the necessary properties for user filtering</param>
    /// <returns></returns>
    /// <response code="200">Filtration was successful</response>
    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> GetUsers( [FromQuery] UserFilteringDto filter)
    {
        var users = _userManager.Users.Include(e => e.UserRoles).AsNoTracking();

        if (!string.IsNullOrEmpty(filter.Name)) 
            users = users.Where(u => u.FirstName.Contains(filter.Name));

        if (!string.IsNullOrEmpty(filter.Surname)) 
            users = users.Where(u => u.LastName.Contains(filter.Surname));

        if (!string.IsNullOrEmpty(filter.Phone)) 
            users = users.Where(u => u.PhoneNumber == filter.Phone);

        if (filter.RegistrationDateFrom.HasValue)
            users = users.Where(u => u.RegistrationDate >= filter.RegistrationDateFrom.Value);

        if (filter.RegistrationDateTo.HasValue)
            users = users.Where(u => u.RegistrationDate <= filter.RegistrationDateTo.Value);

        if (!string.IsNullOrEmpty(filter.Email)) 
            users = users.Where(u => u.Email == filter.Email);
        // TODO: Role Name Not Working
        if (!string.IsNullOrEmpty(filter.Role))
            users = users
                .Where(u => u.UserRoles.Any(r => r.Role.Name == filter.Role));
        
        var result = await users.Select(e => e.ToUserFullnameItemDto()).ToArrayAsync();

        return Json(result);
    }
}