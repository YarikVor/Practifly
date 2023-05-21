namespace PractiFly.WebApi.Dto;

public sealed class UrlResult
{
    public string Url {get;}
    
    public UrlResult(string url)
    {
        Url = url;
    }
}