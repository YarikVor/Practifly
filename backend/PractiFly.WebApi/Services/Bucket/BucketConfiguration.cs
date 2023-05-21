using PractiFly.WebApi.Services.AuthenticationOptions;

namespace PractiFly.WebApi;

//Bucket
public class BucketConfiguration : ConfigurationAbstraction, IBucketConfiguration
{
    private const string AccessKeyName = "AccessKeyId";
    private const string SecretKeyName = "SecretAccessKey";
    private const string RegionKeyName = "Region";
    private const string BucketKeyName = "BucketName";
    private const string BaseUrlKeyName = "BaseUrl";
    private const string ProfileKeyName = "Profile";

    public BucketConfiguration(IConfiguration configuration) : base(configuration)
    {
    }

    protected override string SectionName => "AWS";

    public string AccessKey => GetValue(AccessKeyName);
    public string SecretKey => GetValue(SecretKeyName);
    public string Region => GetValue(RegionKeyName);
    public string BucketName => GetValue(BucketKeyName);
    public string BaseUrl => GetValue(BaseUrlKeyName);
    public string ProfileName => GetValue(ProfileKeyName);
}