namespace PractiFly.WebApi;

public interface IAmazonS3ClientManager
{
    Task<string?> UploadFileAsync(IFormFile file, string name);

    string GetFileUrl(string name = "");

    Task<bool> IsAvaibleFileAsync(string name);

    Task<bool> DeleteFileAsync(string name);
}