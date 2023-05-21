namespace PractiFly.WebApi.Dto;

public sealed class UrlResult
{
    public UrlResult(string url)
    {
        Url = url;
    }

    public string Url { get; }
}