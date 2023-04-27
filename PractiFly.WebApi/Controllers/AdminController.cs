using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.AutoMapper;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Dto.Admin.UserView;
using PractiFly.WebApi.Services.TokenGenerator;

namespace PractiFly.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPractiflyContext _context;
        private readonly IMapper _mapper;


        public AdminController(IPractiflyContext practiflyContext, IHttpContextAccessor httpContext, ITokenGenerator
            tokenGenerator, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _httpContext = httpContext;
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = practiflyContext;

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetInfoForUsers(int userId)
        {
            var result = await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserProfileForAdminViewDto
                {
                    Id = u.Id,
                    Name = u.FirstName,
                    Surname = u.LastName,
                    Email = u.Email,
                    Phone = u.PhoneNumber,
                    RegistrationDate = u.RegistrationDate,
                    FilePhoto = u.FilePhoto,
                    //TODO: Role?
                    //Role = _userManager.GetRolesAsync(u).Result.First()
                })
                .FirstOrDefaultAsync();

            if(result == null) { return NotFound(); }

            return Json(result);
               
        }

        [HttpDelete]
        [Route("[action]")]
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

        [HttpPost]
        [Route("[action]")]
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
        [HttpPost]
        [Route("[action]")]
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
        [Route("[action]")]
        public async Task<IActionResult> GetUsers(UserFilteringDto filter)
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
}
