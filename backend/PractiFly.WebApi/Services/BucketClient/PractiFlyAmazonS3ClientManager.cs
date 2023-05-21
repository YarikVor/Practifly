using System.Net;
using Amazon.S3;
using Amazon.S3.Model;

namespace PractiFly.WebApi;

public class PractiFlyAmazonS3ClientManager : IAmazonS3ClientManager
{
    private readonly IAmazonS3 _client;
    private readonly string _baseUrl;
    private readonly string _bucketName;
    public PractiFlyAmazonS3ClientManager(IAmazonS3 client, IBucketConfiguration configuration)
    {
        _client = client;
        _bucketName = configuration.BucketName;
        _baseUrl = configuration.BaseUrl;
    }
    
    public async Task<string?> UploadFileAsync(IFormFile file, string name)
    {
        var request = CreateUpdateRequest(file, name);
        var res = await _client.PutObjectAsync(request);

        if (res.HttpStatusCode != HttpStatusCode.OK)
            return null;
        
        return GetFileUrl(name);
    }
    
    private PutObjectRequest CreateUpdateRequest(IFormFile file, string name)
    {
        return new PutObjectRequest()
        {
            BucketName = _bucketName,
            Key = name,
            InputStream = file.OpenReadStream(),
            ContentType = file.ContentType
        };
    }
    
    public string GetFileUrl(string name)
    {
        return $"{_baseUrl}/{name}";
    }

    public async Task<bool> IsAvaibleFileAsync(string name)
    {
        var request = CreateMetadataRequest(name);
        
        try
        {
            await _client.GetObjectMetadataAsync(request);
        }
        catch (AmazonS3Exception e)
        {
            if (e.StatusCode == HttpStatusCode.NotFound)
                return false;
            throw;
        }
        
        return true;
    }

    private GetObjectMetadataRequest CreateMetadataRequest(string name)
    {
        return new GetObjectMetadataRequest()
        {
            BucketName = _bucketName,
            Key = name
        };
    }
    
    public async Task<bool> DeleteFileAsync(string name)
    {
        var request = CreateDeleteRequest(name);
        var response = await _client.DeleteObjectAsync(request);
        return response.HttpStatusCode.IsSuccess();
    }
    
    private DeleteObjectRequest CreateDeleteRequest(string name)
    {
        return new DeleteObjectRequest()
        {
            BucketName = _bucketName,
            Key = name
        };
    }
}