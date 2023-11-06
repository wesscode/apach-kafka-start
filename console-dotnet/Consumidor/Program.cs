using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

/*Implementação funcional utilizando Schema Registry*/
var schemaConfig = new SchemaRegistryConfig
{
    Url = "http://localhost:8081"
};

var schemaRegistry = new CachedSchemaRegistryClient(schemaConfig); //onde armazena os ids dos schemas que estão armazenados no servido do schema registry. Para nn precisar sempre buscar no servidor.

var config = new ConsumerConfig
{
    GroupId = "devio",
    BootstrapServers = "localhost:9092" //onde meu broker está rodando
};

using var consumer = new ConsumerBuilder<string, desenvolvedor.io.Curso>(config)
// .SetKeyDeserializer => podendo realizar tbm a deserializao da key
.SetValueDeserializer(new AvroDeserializer<desenvolvedor.io.Curso>(schemaRegistry).AsSyncOverAsync())
.Build();
consumer.Subscribe("cursos");
while (true)
{
    var result = consumer.Consume();
    Console.WriteLine($"Mensagem: {result.Message.Value.Descricao}");
}



/*Configuração funcional sem utilizar SchemaRegistry*/
// var config = new ConsumerConfig
// {
//     GroupId = "devio",
//     BootstrapServers = "localhost:9092" //onde meu broker está rodando
// };

// using var consumer = new ConsumerBuilder<string, string>(config).Build();
// consumer.Subscribe("topico-teste");

// while (true)
// {
//     var result = consumer.Consume();
//     Console.WriteLine($"Mensagem: {result.Message.Key} - {result.Message.Value}");
// }