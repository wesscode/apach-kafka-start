namespace MasterKafka.MessageBus.Message.Integration
{
    public class OrderCanceledIntegrationEvent : IntegrationEvent
    {
        public OrderCanceledIntegrationEvent(Guid customerId, Guid orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
        }

        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }
    }
}
