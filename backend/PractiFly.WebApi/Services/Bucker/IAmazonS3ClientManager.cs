namespace PractiFly.WebApi;

public interface IAmazonS3ClientManager
{
    Task<string?> UploadFileAsync(IFormFile file, string name);
}