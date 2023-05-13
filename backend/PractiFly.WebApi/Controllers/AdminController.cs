using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.AutoMapper.Ex;
using PractiFly.WebApi.Dto.Admin.UserView;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.Controllers;

[Route("api/admin/user")]
[ApiController]
//[Authorize(Roles = UserRoles.Admin, AuthenticationSchemes = "Bearer")]
public class AdminController : Controller
{
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IPractiflyContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;


    public AdminController(IPractiflyContext practiflyContext,
        UserManager<User> userManager,
        IConfigurationProvider configurationProvider,
        IMapper mapper
    )
    {
        _userManager = userManager;
        _context = practiflyContext;
        _configurationProvider = configurationProvider;
        _mapper = mapper;
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

        return result == null ? NotFound() : Json(result);
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
            return NotFound();

        var result = await _userManager.DeleteAsync(user);

        return !result.Succeeded ? BadRequest() : Ok();
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
        var user = _mapper.Map<UserProfileForAdminCreateDto, User>(userDto);

        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (!result.Succeeded)
            return BadRequest();

        var roleResult = await _userManager.AddToRoleAsync(user, userDto.Role);
        //TODO: reyurn userId
        return !roleResult.Succeeded ? BadRequest() : Ok();
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

        user.ChangeUserInAdmin(userDto);
        //Add
        //
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var currentRoles = await _userManager.GetRolesAsync(user);

        var roleDeleteResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

        if (!roleDeleteResult.Succeeded)
            return BadRequest(roleDeleteResult.Errors);

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
    public async Task<IActionResult> GetUsers([FromQuery] UserFilteringDto filter)
    {
        var roleId = 0;

        if (!string.IsNullOrEmpty(filter.Role))
            roleId = await _context.Roles.Where(e => e.Name == filter.Role).Select(e => e.Id).FirstAsync();

        var users = _context.Users.AsNoTracking();

        if (roleId != 0)
            users = users.Where(u => _context.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == roleId));

        if (!string.IsNullOrEmpty(filter.FirstName))
            users = users.Where(u => u.FirstName.Contains(filter.FirstName));

        if (!string.IsNullOrEmpty(filter.LastName))
            users = users.Where(u => u.LastName.Contains(filter.LastName));

        if (!string.IsNullOrEmpty(filter.Phone))
            users = users.Where(u => u.PhoneNumber == filter.Phone);

        if (filter.RegistrationDateFrom.HasValue)
            users = users.Where(u => u.RegistrationDate >= filter.RegistrationDateFrom.Value);

        if (filter.RegistrationDateTo.HasValue)
            users = users.Where(u => u.RegistrationDate <= filter.RegistrationDateTo.Value);

        if (!string.IsNullOrEmpty(filter.Email))
            users = users.Where(u => u.Email == filter.Email);

        var result = await users
            .OrderBy(u => u.Id)
            .ProjectTo<UserFullnameItemDto>(_configurationProvider)
            .ToListAsync();

        return Json(result);
    }
}