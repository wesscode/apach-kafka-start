using MasterKafka.API.Services;

namespace MasterKafka.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IBillingService, BillingService>();
        }
    }
}
