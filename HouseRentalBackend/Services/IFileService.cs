namespace HouseRentalBackend.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderPath);
        void DeleteFile(string relativePath);
    }
}
