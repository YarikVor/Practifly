using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.WebApi.Dto;

namespace PractiFly.WebApi.Controllers;

[ApiController]
[Route("api/")]
public class FilesController : ControllerBase
{
    private readonly IAmazonS3ClientManager _amazonClient;
    private readonly IPractiflyContext _context;

    public FilesController(IAmazonS3ClientManager amazonClient, IPractiflyContext context)
    {
        _amazonClient = amazonClient;
        _context = context;
    }

    [Route("admin/user/avatar")]
    [HttpPost]
    public async Task<IActionResult> UploadUserAvatarAsync(IFormFile file, int userId)
    {
        var url = await _amazonClient.UploadFileAsync(file, userId.ToString());
        var count = await _context
            .Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(calls => calls.SetProperty(u => u.IsDefaultPhoto, true));

        return url == null || count == 0 ? BadRequest() : Ok(new UrlResult(url));
    }

    [Route("user/avatar")]
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UploadSelfAvatarAsync(IFormFile file)
    {
        var id = User.GetUserIdInt();

        return await UploadUserAvatarAsync(file, id);
    }

    [Route("admin/user/avatar")]
    [HttpDelete]
    public async Task<IActionResult> DeleteUserAvatarAsync(int userId)
    {
        var count = await _context
            .Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(calls => calls.SetProperty(u => u.IsDefaultPhoto, false));

        var result = await _amazonClient.DeleteFileAsync(userId.ToString());

        var url = _amazonClient.GetFileUrl("0");

        return result && count != 0 ? Ok(new UrlResult(url)) : BadRequest();
    }

    [Route("user/avatar")]
    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteSelfAvatarAsync()
    {
        var id = User.GetUserIdInt();

        return await DeleteUserAvatarAsync(id);
    }
}