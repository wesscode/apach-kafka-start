using MasterKafka.API.Services;
using MasterKafka.MessageBus;

namespace MasterKafka.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
               IConfiguration configuration)
        {
            services.AddMessageBus(configuration?.GetSection("MessageKafkaConnection")?["MessageBus"])
                .AddHostedService<BillingIntegrationHandler>();
        }
    }
}
