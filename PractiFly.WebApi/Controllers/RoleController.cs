using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PractiFly.DbEntities.Users;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;

    public RoleController(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    [Route("[action]")]
    [HttpGet]
    public IActionResult Index()
    {
        return Json(userManager.Users);
    }
}