using Microsoft.AspNetCore.Http;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IFileStorageService
    {
        Task<string> SaveImageAndGetUrl(IFormFile imageFile);
        Task<bool> DeleteImage(string imageName);
    }
}
