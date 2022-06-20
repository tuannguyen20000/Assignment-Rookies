using eCommerce_SharedViewModels.Utilities.Constants;

namespace eCommerce_Backend.Application.Common
{
    public class FileStorage : IFileStorage
    {
        private readonly string _userContentFolder;

        public FileStorage()
        {
            _userContentFolder = Path.Combine(SystemConstants.RESOURCES, SystemConstants.USER_IMAGES_FOLDER_NAME);
        }
        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public string GetFileUrl(string fileName)
        {
            return $"{SystemConstants.RESOURCES}/{SystemConstants.USER_IMAGES_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }
    }
}
