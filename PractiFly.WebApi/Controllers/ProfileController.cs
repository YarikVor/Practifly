using System.Security.Claims;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.AutoMapper;
using PractiFly.WebApi.Dto.Profile;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("api/user/profile")]
public class ProfileController : Controller
{
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IPractiflyContext _context;
    private readonly UserManager<User> _userManager;

    public ProfileController(
        IPractiflyContext context,
        IConfigurationProvider configurationProvider,
        UserManager<User> userManager
    )
    {
        _context = context;
        _configurationProvider = configurationProvider;
        _userManager = userManager;
    }

    /// <summary>
    ///     Returns profile information for the user with the specified userId.
    /// </summary>
    /// <param name="userId">Id of the user.</param>
    /// <response code="200">Getting profile information of user was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No user found.</response>
    /// <returns>A JSON-encoded representation of the user's profile information.</returns>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetUserProfileInfo(int userId)
    {
        var result = await _context
            .Users
            .AsNoTracking()
            .Where(e => e.Id == userId)
            .ProjectTo<UserInfoDto>(_configurationProvider)
            .FirstOrDefaultAsync();

        if (result == null)
            return NotFound();

        return Json(result);
    }

    /// <summary>
    ///     Updates the profile information for the currently authenticated user.
    /// </summary>
    /// <param name="userDto">A Data Transfer Object containing the updated user information.</param>
    /// <response code="200">User update was successful.</response>
    /// <response code="400">Update was failed.</response>
    /// <response code="404">No user found.</response>
    /// <returns>An IActionResult representing the result of the update operation.</returns>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("edit")]
    [HttpPost]
    public async Task<IActionResult> UpdateUser(UserProfileInfoCreateDto userDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) return NotFound();

        user.ChangeUser(userDto);

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded) return BadRequest();

        return Ok();
    }
}