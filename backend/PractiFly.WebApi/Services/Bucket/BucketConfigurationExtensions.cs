using Amazon;

namespace PractiFly.WebApi;

public static class BucketConfigurationExtensions
{
    public static RegionEndpoint GetRegionEndpoint(this IBucketConfiguration configuration)
    {
        return RegionEndpoint.GetBySystemName(configuration.Region);
    }
}