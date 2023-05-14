using Microsoft.AspNetCore.Mvc;

namespace PractiFly.WebApi.Controllers
{
    [ApiController]
    [Route("api/bucket")]
    public class FilesController : ControllerBase
    {
        private readonly IPractiFlyAmazonS3ClientManager _amazonClient;

        public FilesController(IPractiFlyAmazonS3ClientManager amazonClient)
        {
            _amazonClient = amazonClient;
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> UploadFileAsync(IFormFile file)
        {
            var url = await _amazonClient.UploadFileAsync(file, "1");

            return url == null ? BadRequest() : Ok(new {url});
        }
    }
}