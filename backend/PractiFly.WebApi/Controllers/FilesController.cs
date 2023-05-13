using Amazon.Runtime;
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

        public FilesController()
        {
            AmazonS3Config config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.EUNorth1

            };

            BasicAWSCredentials credentials =
                new BasicAWSCredentials("AKIAYICM4SLLBL4MZZ5F", "Zi0p4Z0YgjrpNcqZuTSRaOCSEZWN++8FV240899H");
            
            _s3Client = new AmazonS3Client(credentials, config);
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> UploadFileAsync(IFormFile file)
        {
            const string bucketName = "practiflybucket";
            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} doesnt exist.");
            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = file.Name,
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await _s3Client.PutObjectAsync(request);
            var baseurl = "https://practiflybucket.s3.eu-north-1.amazonaws.com/";
            return Ok($"{baseurl}{file.Name}");
        }
    }
}