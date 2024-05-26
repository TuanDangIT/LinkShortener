using Microsoft.EntityFrameworkCore;

namespace LinkShortener.API.DAL
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LinkShortenerDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Postgres"));
            });
            services.AddScoped<ILinkShortenerDbContext, LinkShortenerDbContext>();
            return services;
        }
    }
}
