using System.ComponentModel.DataAnnotations;
using Amazon;
using Amazon.S3;

namespace PractiFly.WebApi;

public sealed class PractiFlyAmazonS3Client: AmazonS3Client
{
    public PractiFlyAmazonS3Client(IConfiguration configuration): base(
        configuration["AWS:AccessKey"],
        configuration["AWS:SecretKey"],
        RegionEndpoint.GetBySystemName(configuration["AWS:Region"])
        )
    {
        var bucketName = configuration["AWS:BucketName"] 
                         ?? throw new KeyNotFoundException("Bucket name is not found.");
        
        CheckBucketExists(bucketName).Wait();
    }
    
    private async Task CheckBucketExists(string bucketName)
    {
        var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(this, bucketName);
        
        if(!bucketExists)
            throw new ValidationException($"Bucket {bucketName} is not found.");
    }
}