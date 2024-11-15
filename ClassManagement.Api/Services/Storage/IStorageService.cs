using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Services.Storage
{
    public interface IStorageService
    {
        string GetOriginalFileName(IFormFile file);
        string GetFileUrl(string fileName);
        Task<string> SaveFileAsync(IFormFile file, string name, FileType type, string? oldPath, CancellationToken cancellationToken);
        Task<bool> DeleteFilePathAsync(string filePath, CancellationToken cancellationToken);
    }
}
