using CRMBackend.Data.Repositories;

namespace CRMBackend.Data.Interface
{
    public interface IAWSImageService
    {
        Task<string> UploadFileAsync(IFormFile file, AWSDirectory FolderName);
        Task<bool> DeleteImageAsync(string imageName);
        string GetImageBaseUrl();
    }
}
