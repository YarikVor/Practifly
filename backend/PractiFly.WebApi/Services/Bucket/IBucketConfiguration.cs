namespace PractiFly.WebApi;

public interface IBucketConfiguration
{
    string AccessKey { get; }
    string SecretKey { get; }
    string Region { get; }
    string BucketName { get; }
    string BaseUrl { get; }
    string ProfileName { get; }
}