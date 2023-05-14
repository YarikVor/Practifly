using System.ComponentModel.DataAnnotations;
using System.Net;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace PractiFly.WebApi;

public sealed class PractiFlyAmazonS3Client: AmazonS3Client
{
    public PractiFlyAmazonS3Client(IConfiguration configuration): base(
        configuration["AWS:AccessKey"],
        configuration["AWS:SecretKey"],
        RegionEndpoint.GetBySystemName(configuration["AWS:Region"])
        )
    {
        var bucketName = configuration["AWS:BucketName"] ?? throw new KeyNotFoundException("Bucket name is not found.");
        
        CheckBucketExists(bucketName).Wait();
    }
    
    private async Task CheckBucketExists(string bucketName)
    {
        var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(this, bucketName);
        
        if(!bucketExists)
            throw new ValidationException($"Bucket {bucketName} is not found.");
    }
}

public interface IPractiFlyAmazonS3ClientManager
{
    Task<string?> UploadFileAsync(IFormFile file, string name);
}

public class PractiFlyAmazonS3ClientManager : IPractiFlyAmazonS3ClientManager
{
    private readonly IAmazonS3 _client;
    private readonly PutObjectRequest _request;
    private readonly string _baseUrl;
    public PractiFlyAmazonS3ClientManager(IAmazonS3 client, IConfiguration configuration)
    {
        _client = client;
        _request = new PutObjectRequest()
        {
            BucketName = configuration["AWS:BucketName"] 
                         ?? throw new KeyNotFoundException("Bucket name is not found.")
        };
        _baseUrl = configuration["AWS:BaseUrl"] 
                   ?? throw new KeyNotFoundException("Base url is not found.");
    }
    
    public async Task<string?> UploadFileAsync(IFormFile file, string name)
    {
        _request.Key = name;
        _request.InputStream = file.OpenReadStream();
        _request.Metadata["Content-Type"] = file.ContentType;
        var res = await _client.PutObjectAsync(_request);

        if (res.HttpStatusCode != HttpStatusCode.OK)
            return null;
        
        return $"{_baseUrl}/{name}";
    }
    
}