namespace ck_pacificdev.Services
{
    public interface IImageService
    {
        Task<string> GetImageUrlAsync(string userIdentifier);
    }
}