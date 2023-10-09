namespace SourceScrub.API
{
    public static class Initializer
    {
        public static IHost Initialize(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var blInitializer = scope.ServiceProvider.GetRequiredService<Business.Initializer>();
                blInitializer.Initialize();
            }
            return host;
        }
    }
}
