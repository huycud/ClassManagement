using System.Net.Http.Headers;
using ClassManagement.Api.Common.Exceptions;
using Utilities.Messages;
using static ClassManagement.Api.Common.FileValidator.FileUploadValidator;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Services.Storage
{
    class StorageService : IStorageService
    {
        private readonly string _contentFolder;

        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly string _folderName;

        public StorageService(IWebHostEnvironment webHostEnvironment, string contentFolder, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;

            _folderName = $"{configuration.GetSection("Url:DataImportDir").Value}/{contentFolder}";

            _contentFolder = Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, _folderName))

                            ? Path.Combine(_webHostEnvironment.WebRootPath, _folderName)

                            : Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, _folderName)).FullName;
        }

        public async Task<bool> DeleteFilePathAsync(string filePath, CancellationToken cancellationToken)
        {
            var fileName = Path.GetFileName(filePath);

            var newPath = Path.Combine(_contentFolder, fileName);

            if (File.Exists(newPath))
            {
                await Task.Run(() => File.Delete(newPath), cancellationToken);

                return true;
            }

            return false;
        }

        public string GetFileUrl(string fileName) => $"/{_folderName}/{fileName}";

        public async Task<string> SaveFileAsync(IFormFile file, string name, FileType type, string? oldPath, CancellationToken cancellationToken)
        {
            var originalFileName = GetOriginalFileName(file);

            var fileName = $"{name}-{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";

            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                var fileContentType = file.ContentType;

                byte[] byteData = reader.ReadBytes((int)file.Length);

                var result = IsValidFile(byteData, fileContentType, type);

                if (result)
                {
                    int fileLenght = 1024 * 1024 * 2;

                    if (file.Length > fileLenght)

                        throw new BadRequestException(string.Format(ErrorMessages.OVER_MAXIMUM_SIZE, "image", $"{fileLenght / 1024} KB"));

                    if (!string.IsNullOrEmpty(oldPath))
                    {
                        bool resultDeleteFile = await DeleteFilePathAsync(oldPath, cancellationToken);

                        if (!resultDeleteFile) throw new BadRequestException(ErrorMessages.HANDLING_FAILURE, "Update image");

                        await SaveFileStreamAsync(file.OpenReadStream(), fileName);
                    }

                    await SaveFileStreamAsync(file.OpenReadStream(), fileName);
                }

                else return default;
            }

            return GetFileUrl(fileName);
        }

        private async Task SaveFileStreamAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_contentFolder, fileName);

            using var output = new FileStream(filePath, FileMode.Create);

            await mediaBinaryStream.CopyToAsync(output);
        }

        public string GetOriginalFileName(IFormFile file)
        {
            return ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        }
    }
}
