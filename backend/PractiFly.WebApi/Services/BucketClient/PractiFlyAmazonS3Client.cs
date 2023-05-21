using System.ComponentModel.DataAnnotations;
using Amazon;
using Amazon.S3;

namespace PractiFly.WebApi;

public sealed class PractiFlyAmazonS3Client: AmazonS3Client
{
    public PractiFlyAmazonS3Client(IBucketConfiguration configuration): base(
        configuration.AccessKey,
        configuration.SecretKey,
        configuration.GetRegionEndpoint()
        )
    {
        var bucketName = configuration.BucketName;
        
        CheckBucketExists(bucketName).Wait();
    }
    
    private async Task CheckBucketExists(string bucketName)
    {
        var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(this, bucketName);
        
        if(!bucketExists)
            throw new KeyNotFoundException($"Bucket {bucketName} is not found.");
    }
}