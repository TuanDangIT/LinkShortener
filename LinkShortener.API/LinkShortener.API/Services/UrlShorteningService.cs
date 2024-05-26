using LinkShortener.API.DAL;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.API.Services
{
    public class UrlShorteningService : IUrlShorteningService
    {
        public const int Length = 7;
        public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly Random _random = new();
        private readonly LinkShortenerDbContext _dbContext;
        public UrlShorteningService(LinkShortenerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<string> GenerateUniqueCode()
        {
            var codeChars = new char[Length];
            int maxValue = Alphabet.Length;

            while (true)
            {
                for (var i = 0; i < Length; i++)
                {
                    var randomIndex = _random.Next(maxValue);

                    codeChars[i] = Alphabet[randomIndex];
                }
                var code = new string(codeChars);

                if (!await _dbContext.ShortenedUrls.AnyAsync(s => s.Code == code))
                {
                    return code;
                }
            }
        }
    }
}
