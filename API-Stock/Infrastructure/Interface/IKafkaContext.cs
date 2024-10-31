using Confluent.Kafka;

namespace Infrastructure.Interface;

public interface IKafkaContext
{
    ConsumerConfig GetConsumerConfig();
    ProducerConfig GetProducerConfig();
}
