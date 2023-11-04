﻿using Confluent.Kafka;

var config = new ProducerConfig { BootstrapServers = "localhost:9092"};

using var producer = new ProducerBuilder<string, string>(config).Build();

var message = new Message<string, string>
{
    Key = Guid.NewGuid().ToString(),
    Value = $"Mensagem teste {Guid.NewGuid().ToString()}"
};

var result = await producer.ProduceAsync("topico-teste", message);

Console.WriteLine($"Partição: ${result.Partition} OffSet: {result.Offset}");