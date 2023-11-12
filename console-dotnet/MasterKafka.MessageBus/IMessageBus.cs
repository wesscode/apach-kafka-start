using MasterKafka.MessageBus.Message;

namespace MasterKafka.MessageBus
{
    public interface IMessageBus : IDisposable
    {
        Task ProducerAsync<T>(string topic, T message) where T : IntegrationEvent;
        Task ConsumerAsync<T>(string topic, Func<T, Task> onMessage, CancellationToken cancellationToken) where T : IntegrationEvent;
    }
}
