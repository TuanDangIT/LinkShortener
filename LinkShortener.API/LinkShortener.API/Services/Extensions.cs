using LinkShortener.API.DAL;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.API.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUrlShorteningService, UrlShorteningService>();
            return services;
        }
    }
}
