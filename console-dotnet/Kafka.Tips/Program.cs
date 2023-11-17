using Confluent.Kafka;


#region  EXEMPLO TIPO DE CONSUMO (AutoOffReset: Earliest, Latest)
/*
const string Topico = "desenvolvedor.io";
var i = 1;

for (i = 1; i <= 5; i++)
{
   // await Produzir(i);
}

_ = Task.Run(() => Consumir("grupo1", AutoOffsetReset.Earliest));
_ = Task.Run(() => Consumir("grupo2", AutoOffsetReset.Earliest));
_ = Task.Run(() => Consumir("grupo3", AutoOffsetReset.Earliest));
_ = Task.Run(() => Consumir("grupo4", AutoOffsetReset.Latest));

while (true)
{
    Console.ReadLine();
    Produzir(i).GetAwaiter().GetResult();
    i++;
}

static async Task Produzir(int i)
{
    var configuracao = new ProducerConfig
    {
        Acks = Acks.All,
        BootstrapServers = "localhost:9092"
    };

    var mensagem = $"Mensagem ( {i} ) - *{Guid.NewGuid()} - {DateTime.Now}*";

    Console.WriteLine(">> Enviada:\t " + mensagem);

    try
    {
        using var producer = new ProducerBuilder<Null, string>(configuracao).Build();

        var result = await producer.ProduceAsync(Topico, new Message<Null, string>
        {
            Value = mensagem
        });

        await Task.CompletedTask;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex.ToString());
    }
}

static void Consumir(string grupoId, AutoOffsetReset autoOffsetReset)
{
    var clientId = Guid.NewGuid().ToString()[0..5];

    var conf = new ConsumerConfig 
    {
        ClientId = clientId,
        GroupId = grupoId,
        BootstrapServers = "localhost:9092",
        AutoOffsetReset = autoOffsetReset,
        EnablePartitionEof = true,
        EnableAutoCommit = false
    };

    using var consumer = new ConsumerBuilder<Null, string>(conf).Build();

    consumer.Subscribe(Topico);

    while(true)
    {
        var result = consumer.Consume();
        if(result.IsPartitionEOF)
        {
            continue;
        }

        var message = "<< Recebida \t" + result.Message.Value + $" - {grupoId}-{autoOffsetReset}-{clientId}";
        
        Console.WriteLine(message);

        consumer.Commit(result);
    }
}*/

#endregion

#region APLICANDO EXEMPLOS DE TIPOS DE ACKS: None, Leader, All.
/*
const string Topico = "desenvolvedor.io";

var configuracao = new ProducerConfig
{
    Acks = Acks.All,
    BootstrapServers = "localhost:9092"
};

var mensagem = $"Mensagem {Guid.NewGuid()}";

try
{
    using var producer = new ProducerBuilder<Null, string>(configuracao).Build();
    
    var result = await producer.ProduceAsync(Topico, new Message<Null, string> 
    {
        Value = mensagem
    });
    //colocar break point aqui para ver os tipos de acks no status
}
catch(Exception ex)
{
    Console.Error.WriteLine(ex.ToString());
}
*/
#endregion