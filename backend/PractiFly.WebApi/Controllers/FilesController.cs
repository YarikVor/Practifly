using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;

namespace PractiFly.WebApi.Controllers
{
    [ApiController]
    [Route("api/bucket")]
    public class FilesController : ControllerBase
    {
        private readonly IAmazonS3 _s3Client;

        public FilesController(IAmazonS3 s3Client)
        {
            _s3Client = new AmazonS3Client("AKIAYICM4SLLBL4MZZ5F", "Zi0p4Z0YgjrpNcqZuTSRaOCSEZWN++8FV240899H");
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> UploadFileAsync(IFormFile file, string? prefix)
        {
            const string bucketName = "practiflybucket";
            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} doesnt exist.");
            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}",
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await _s3Client.PutObjectAsync(request);
            return Ok($"File {prefix}/{file.FileName} uploaded to S3 successfully!");
        }
    }
}