using LinkShortener.API.Entities;
using LinkShortener.API.Services;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.API.DAL
{
    public class LinkShortenerDbContext : DbContext, ILinkShortenerDbContext
    {
        public LinkShortenerDbContext(DbContextOptions options)
        : base(options)
        {
        }
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(builder =>
            {
                builder
                    .Property(shortenedUrl => shortenedUrl.Code)
                    .HasMaxLength(UrlShorteningService.Length);

                builder
                    .HasIndex(shortenedUrl => shortenedUrl.Code)
                    .IsUnique();
            });
        }
    }
}
