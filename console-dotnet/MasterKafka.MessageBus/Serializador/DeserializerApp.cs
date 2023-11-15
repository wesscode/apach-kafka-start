
using System.IO.Compression;
using System.Text.Json;
using Confluent.Kafka;

namespace MasterKafka.MessageBus
{
    public class DeserializerApp<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            using var memoryStream = new MemoryStream(data.ToArray());
            using var zip = new GZipStream(memoryStream, CompressionMode.Decompress, true);

            return JsonSerializer.Deserialize<T>(zip);
        }
    }
}