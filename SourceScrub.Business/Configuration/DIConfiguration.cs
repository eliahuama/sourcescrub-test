using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SourceScrub.Business.Services;
using SourceScrub.Business.Services.Interfaces;
using SourceScrub.Data;

namespace SourceScrub.Business
{
    public static class DIConfiguration
    {
        public static void AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<Initializer>();
            services.AddDataServices(configuration);
        }
    }
}
