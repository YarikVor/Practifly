using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.Context;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUsersContext _context;
    private readonly ILogger<UserController> _logger;

    public UserController(
        ILogger<UserController> logger,
        IUsersContext context
    )
    {
        _logger = logger;
        _context = context;
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> User(int id)
    {
        if (id <= 0)
            return BadRequest();

        var headings = await _context.Users.FirstOrDefaultAsync(e => e.Id == id);

        if (headings == null)
            return BadRequest();

        return Json(headings);
    }
}