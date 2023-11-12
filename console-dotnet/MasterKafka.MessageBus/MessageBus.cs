using Confluent.Kafka;
using MasterKafka.MessageBus.Message;
using System.Text.Json;

namespace MasterKafka.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private readonly string _bootstrapServer;

        public MessageBus(string bootstrapServer)
        {
            _bootstrapServer = bootstrapServer;
        }
        public async Task ProducerAsync<T>(string topic, T message) where T : IntegrationEvent
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServer,
            };

            var payload = JsonSerializer.Serialize(message);

            var producer = new ProducerBuilder<string, string>(config).Build();

            var result = await producer.ProduceAsync(topic, new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = payload
            });

            await Task.CompletedTask;
        }

        public Task ConsumerAsync<T>(string topic, Func<T, Task> onMessage, CancellationToken cancellationToken) where T : IntegrationEvent
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
