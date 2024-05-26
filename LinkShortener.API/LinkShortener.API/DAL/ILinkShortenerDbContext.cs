using LinkShortener.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.API.DAL
{
    public interface ILinkShortenerDbContext
    {
        DbSet<ShortenedUrl> ShortenedUrls { get; set; }
        Task SaveChangesAsync();
    }
}
