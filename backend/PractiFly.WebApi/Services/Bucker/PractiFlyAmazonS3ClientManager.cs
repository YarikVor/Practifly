using System.Net;
using Amazon.S3;
using Amazon.S3.Model;

namespace PractiFly.WebApi;

public class PractiFlyAmazonS3ClientManager : IAmazonS3ClientManager
{
    private readonly IAmazonS3 _client;
    private readonly string _baseUrl;
    private readonly string _bucketName;
    public PractiFlyAmazonS3ClientManager(IAmazonS3 client, IConfiguration configuration)
    {
        _client = client;
        _bucketName = configuration["AWS:BucketName"] 
                      ?? throw new KeyNotFoundException("Bucket name is not found.");
        _baseUrl = configuration["AWS:BaseUrl"] 
                   ?? throw new KeyNotFoundException("Base url is not found.");
    }
    
    public async Task<string?> UploadFileAsync(IFormFile file, string name)
    {
        var request = CreateRequest(file, name);
        var res = await _client.PutObjectAsync(request);

        if (res.HttpStatusCode != HttpStatusCode.OK)
            return null;
        
        return $"{_baseUrl}/{name}";
    }
    
    private PutObjectRequest CreateRequest(IFormFile file, string name)
    {
        return new PutObjectRequest()
        {
            BucketName = _bucketName,
            Key = name,
            InputStream = file.OpenReadStream(),
            ContentType = file.ContentType
        };
    }
    
}