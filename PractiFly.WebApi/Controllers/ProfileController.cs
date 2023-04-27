using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.Profile;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProfileController: Controller
{
    private readonly IPractiflyContext _context;
    private readonly IMapper _mapper;
    private readonly IConfigurationProvider _configurationProvider;
    private readonly UserManager<User> _userManager;

    public ProfileController(IPractiflyContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _configurationProvider = mapper.ConfigurationProvider;
        _userManager = userManager;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetUserProfileInfo(int userId)
    {
        var result = _context
            .Users
            .Where(e => e.Id == userId)
            .ProjectTo<UserInfoDto>(_configurationProvider)
            .FirstOrDefault();
        
        if(result == null)
            return NotFound();
        
        return Json(result);
    }

    [Authorize]
    [Route("[action]")]
    [HttpGet]
    public async Task<IActionResult> UpdateUser(UserProfileInfoCreateDto userDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        User user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }
        
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.PhoneNumber = userDto.PhoneNumber;
        user.Email = userDto.Email;
        user.Birthday = userDto.Birthday;
        user.FilePhoto = userDto.FilePhoto;
        
        var result = await _userManager.UpdateAsync(user);
        
        if (!result.Succeeded)
        {
            return BadRequest();
        }
        
        return Ok();
    }
}
