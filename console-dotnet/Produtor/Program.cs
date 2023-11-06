using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

/*Implementação funcional utilizando Schema Registry*/
var schemaConfig = new SchemaRegistryConfig
{
    Url = "http://localhost:8081"
};

//api de comunicação com o servidor do schemaRegistry
var schemaRegistry = new CachedSchemaRegistryClient(schemaConfig); //onde armazena os ids dos schemas que estão armazenados no servido do schema registry. Para nn precisar sempre buscar no servidor.

var config = new ProducerConfig { BootstrapServers = "localhost:9092"};

using var producer = new ProducerBuilder<string, desenvolvedor.io.Curso>(config)
.SetValueSerializer(new AvroSerializer<desenvolvedor.io.Curso>(schemaRegistry))
.Build();

var message = new Message<string, desenvolvedor.io.Curso>
{
    Key = Guid.NewGuid().ToString(),
    Value = new desenvolvedor.io.Curso 
    {
        Id = Guid.NewGuid().ToString(),
        Descricao = "Curso de Apache Kafka"
    }
};

var result = await producer.ProduceAsync("cursos", message);
Console.WriteLine($"Partição: {result.Partition} - OffSet: {result.Offset}");




/*Configuração funcional sem utilizar SchemaRegistry*/
// var config = new ProducerConfig { BootstrapServers = "localhost:9092"};

// using var producer = new ProducerBuilder<string, string>(config).Build();

// var message = new Message<string, string>
// {
//     Key = Guid.NewGuid().ToString(),
//     Value = $"Mensagem teste {DateTime.Now.Second}"
// };

// var result = await producer.ProduceAsync("topico-teste", message);

// Console.WriteLine($"Partição: {result.Partition} - OffSet: {result.Offset}");