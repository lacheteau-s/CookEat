namespace CookEat.Api.Configuration
{
    public static class CorsConfiguration
    {
        private const string AllowAll = "AllowAll";

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddCors(options =>
            {
                // TODO: actual config
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}
