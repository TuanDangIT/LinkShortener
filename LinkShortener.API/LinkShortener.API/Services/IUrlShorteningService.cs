namespace LinkShortener.API.Services
{
    public interface IUrlShorteningService
    {
        Task<string> GenerateUniqueCode();
    }
}
