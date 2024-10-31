using Confluent.Kafka;
using Infrastructure.Interface;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configuration.Kafka;

public class KafkaContext : IKafkaContext
{
    private readonly ConsumerConfig _consumerConfig;
    private readonly ProducerConfig _producerConfig;

    public KafkaContext(IConfiguration configuration)
    {
        _consumerConfig = new ConsumerConfig
        {
            GroupId = configuration.GetValue<string>("Kafka:GroupId"),
            BootstrapServers = configuration.GetValue<string>("Kafka:BootstrapServers"),
            AutoOffsetReset = AutoOffsetReset.Earliest,
            AllowAutoCreateTopics = true,
        };

        _producerConfig = new ProducerConfig
        {
            BootstrapServers = configuration.GetValue<string>("Kafka:BootstrapServers"),
            AllowAutoCreateTopics = true,
        };
    }

    public ConsumerConfig GetConsumerConfig() => _consumerConfig;

    public ProducerConfig GetProducerConfig() => _producerConfig;
}