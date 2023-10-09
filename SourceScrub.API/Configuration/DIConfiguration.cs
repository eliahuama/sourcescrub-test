using SourceScrub.Business;

namespace SourceScrub.API
{
    public static class DIConfiguration
    {
        public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBusinessServices(configuration);
        }
    }
}
