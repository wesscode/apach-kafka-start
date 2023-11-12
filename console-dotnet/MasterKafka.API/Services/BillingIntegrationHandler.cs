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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        //private async Task SetSubscribersAsync()
        //{
        //    await _bus.ConsumerAsync<OrderCanceledIntegrationEvent>("OrderCanceled", CancelTransaction);

        //    await _bus.SubscribeAsync<OrderLoweredStockIntegrationEvent>("UpdateStockOrder", CapturePayment);
        //}

        private async Task CancelTransaction(string order)
        {
            using var scope = _serviceProvider.CreateScope();

            var pagamentoService = scope.ServiceProvider.GetRequiredService<IBillingService>();

            var response = await pagamentoService.CancelTransaction(123);

            if (response is not null)
                throw new Exception($"Failed to cancel order payment {order}");
        }
    }
}
