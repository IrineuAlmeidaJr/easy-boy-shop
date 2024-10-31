using Confluent.Kafka;
using Domain.Exception;
using Infrastructure.Interface;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Configuration.Kafka;

public abstract class KafkaBaseProducer<TKey, TValue>
{
    private readonly IKafkaContext _context;
    private readonly ILogger<KafkaBaseProducer<TKey, TValue>> _logger;
    private readonly string _topic;

    private readonly IProducer<TKey, TValue> _producer;

    protected virtual ISerializer<TKey> GetKeySerializer() => new KafkaJsonSerializer<TKey>();
    protected virtual ISerializer<TValue> GetValueSerializer() => new KafkaJsonSerializer<TValue>();

    protected KafkaBaseProducer(IKafkaContext context, ILogger<KafkaBaseProducer<TKey, TValue>> logger, string topic)
    {
        _context = context;
        _logger = logger;
        _topic = topic;

        _producer = new ProducerBuilder<TKey, TValue>(_context.GetProducerConfig())
            .SetKeySerializer(GetKeySerializer())
            .SetValueSerializer(GetValueSerializer())
            .Build();
    }

    public virtual async Task PublishAsync(TKey key, TValue value)
    {
        try
        {
            if (value == null)
                DomainExecptionValidation.When(true, "Um evento nulo não pode ser publicado");

            var message = new Message<TKey, TValue> { Key = key, Value = value };
            var deliveryResult = await _producer.ProduceAsync(_topic, message);
            _logger.LogInformation($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
        }
        catch (ProduceException<TKey, TValue> e)
        {
            _logger.LogError($"Delivery failed: {e.Error.Reason}");
        }
    }

    public virtual async Task PublishAsync(TValue value)
    {
        // Passamos 'default' para o valor que KEY, pois, para nós a ordenação não é uma prioridade, por essa razão,
        // a mensagem não tem uma chave específica de identificação.

        // Se eu passar uma KEY aqui eu vou garantir que todas as mensagens irão para a mesma partição. 

        await PublishAsync(default, value);
    }
}