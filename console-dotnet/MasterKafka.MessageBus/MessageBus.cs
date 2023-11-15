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
            //informando quais servidores iremos publicar a mensagem
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServer,
            };

            //mensagem serializada para enviar
           // var payload = JsonSerializer.Serialize(message);

            //inicializando producer
            var producer = new ProducerBuilder<string, T>(config)
            .SetValueSerializer(new SerializerApp<T>())
            .Build();

            //chamada do método para enviar a mensagem
            var result = await producer.ProduceAsync(topic, new Message<string, T>
            {
                Key = Guid.NewGuid().ToString(),
                Value = message
            });

            await Task.CompletedTask;
        }

        public async Task ConsumerAsync<T>(string topic, Func<T, Task> onMessage, CancellationToken cancellationToken) where T : IntegrationEvent
        {
            _ = Task.Factory.StartNew(async () =>
            {
                var config = new ConsumerConfig
                {
                    GroupId = "grupo-curso",
                    BootstrapServers = _bootstrapServer,
                    EnableAutoCommit = false,
                    EnablePartitionEof = true //notifica o final da partição
                };

                using var consumer = new ConsumerBuilder<string, T>(config)
                .SetValueDeserializer(new DeserializerApp<T>())
                .Build();

                consumer.Subscribe(topic); //se increvendo no tópico.

                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = consumer.Consume();
                    if (result.IsPartitionEOF) //verifica se não está no final da partição
                        continue;

                    //var message = JsonSerializer.Deserialize<T>(result.Message.Value);

                    await onMessage(result.Message.Value);
                    consumer.Commit();

                }
            }, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}