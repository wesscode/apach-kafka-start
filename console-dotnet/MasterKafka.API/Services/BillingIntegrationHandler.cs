using MasterKafka.MessageBus;
using MasterKafka.MessageBus.Message.Integration;

namespace MasterKafka.API.Services
{
    public class BillingIntegrationHandler : BackgroundService
    {
        private IMessageBus _bus;
        private IServiceProvider _serviceProvider;
        public BillingIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           await SetSubscribersAsync(stoppingToken);
        }

        private async Task SetSubscribersAsync(CancellationToken stoppingToken)
        {
           await _bus.ConsumerAsync<OrderCanceledIntegrationEvent>("OrderCanceled", CancelTransaction, stoppingToken);
        }

        private async Task CancelTransaction(OrderCanceledIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();

            var pagamentoService = scope.ServiceProvider.GetRequiredService<IBillingService>();

            var response = await pagamentoService.CancelTransaction(message.OrderId);

            if (response is not null)
                throw new Exception($"Failed to cancel order payment {message.OrderId}");
        }
    }
}
