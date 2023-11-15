using Confluent.Kafka;

const string Topico = "desenvolvedor.io";

var configuracao = new ProducerConfig
{
    Acks = Acks.All,
    BootstrapServers = "localhost:9094"
};

var mensagem = $"Mensagem {Guid.NewGuid()}";

try
{
    using var producer = new ProducerBuilder<Null, string>(configuracao).Build();
    
    var result = await producer.ProduceAsync(Topico, new Message<Null, string> 
    {
        Value = mensagem
    });
}
catch(Exception ex)
{
    Console.Error.WriteLine(ex.ToString());
}