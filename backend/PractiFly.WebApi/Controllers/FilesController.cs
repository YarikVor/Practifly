using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PractiFly.WebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class FilesController : ControllerBase
    {
        private readonly IAmazonS3ClientManager _amazonClient;

        public FilesController(IAmazonS3ClientManager amazonClient)
        {
            _amazonClient = amazonClient;
        }

        [Route("admin/user/upload_avatar")]
        [HttpPost]
        public async Task<IActionResult> UploadUserAvatarAsync(int userId, IFormFile file)
        {
            var url = await _amazonClient.UploadFileAsync(file, userId.ToString());

            return url == null ? BadRequest() : Ok(new {url});
        }
        
        [Route("user/upload_avatar")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UploadSelfAvatarAsync(IFormFile file)
        {
            int id = User.GetUserIdInt();
            
            return await UploadUserAvatarAsync(id, file);
        }
    }
}