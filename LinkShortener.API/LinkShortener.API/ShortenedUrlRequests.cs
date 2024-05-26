using LinkShortener.API.DAL;
using LinkShortener.API.Entities;
using LinkShortener.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.API
{
    public static class ShortenedUrlRequests
    {
        public static IEndpointRouteBuilder RegisterShortenUrlRequests(this IEndpointRouteBuilder app)
        {
            app.MapPost("shorten", CreateShortenedUrl);
            app.MapGet("{code}", GetShortenedUrl);
            return app;
        }
        public static async Task<IResult> CreateShortenedUrl(string url, IUrlShorteningService urlShorteningService,
            ILinkShortenerDbContext dbContext, HttpContext httpContext)
        {
            if (!Uri.TryCreate(url , UriKind.Absolute, out _))
            {
                return Results.BadRequest("The specified URL is invalid.");
            }
            var code = await urlShorteningService.GenerateUniqueCode();
            var request = httpContext.Request;
            var shortenedUrl = new ShortenedUrl
            {
                Id = Guid.NewGuid(),
                LongUrl = url,
                Code = code,
                ShortUrl = $"{request.Scheme}://{request.Host}/{code}",
                CreatedOnUtc = DateTime.UtcNow
            };

            dbContext.ShortenedUrls.Add(shortenedUrl);
            await dbContext.SaveChangesAsync();
            return Results.Ok(shortenedUrl.ShortUrl);
        }
        public static async Task<IResult> GetShortenedUrl(string code, ILinkShortenerDbContext dbContext)
        {
            var shortenedUrl = await dbContext
                .ShortenedUrls
                .SingleOrDefaultAsync(s => s.Code == code);
            if (shortenedUrl is null)
            {
                return Results.NotFound();
            }
            return Results.Redirect(shortenedUrl.LongUrl);
        }
    }
}
